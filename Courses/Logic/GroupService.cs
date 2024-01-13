using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dto;
using Backend.Chat.Dal.Interfaces;
using Backend.Chat.Dal.Models;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;
using Backend.Courses.Dto;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Courses.Logic;

public class GroupService(
    IGroupRepo groupRepo,
    IStudentGroupsRepo studentGroupsRepo,
    IStudentRepo studentRepo,
    ITeacherRepo teacherRepo,
    IAccountRepo accountRepo,
    IGroupCoursesRepo groupCoursesRepo,
    IChatRepo chatRepo,
    ICourseRepo courseRepo,
    CourseService courseService,
    IHttpContextAccessor httpContextAccessor)
{
    private IGroupRepo _groupRepo = groupRepo;
    private IStudentGroupsRepo _studentGroupsRepo = studentGroupsRepo;
    private IStudentRepo _studentRepo = studentRepo;
    private ITeacherRepo _teacherRepo = teacherRepo;
    private IAccountRepo _accountRepo = accountRepo;
    private ICourseRepo _courseRepo = courseRepo;
    private IGroupCoursesRepo _groupCoursesRepo = groupCoursesRepo;
    private IChatRepo _chatRepo = chatRepo;

    private CourseService _courseService = courseService;

    private IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<GroupGetDto> GetGroupByIdAsync(int id)
    {
        var group = await _groupRepo.GetEntityByIdAsync(id);
        return MapGroupToGetDto(group);
    }

    public async Task<List<GroupGetDto>> GetAllGroupsAsync()
    {
        var groups = await _groupRepo.GetAllEntitiesAsync();
        return groups.Select(MapGroupToGetDto).ToList();
    }

    public async Task<List<GroupGetDto>> GetAllGroupsByTeacherUserIdAsync(int userId)
    {
        var teacherId = await GetTeacherIdByUserIdAsync(userId);
        var allGroups = await _groupRepo.GetAllTeacherGroupsAsync(teacherId);
        return allGroups.Select(MapGroupToGetDto).ToList();
    }

    public async Task<GroupStudentsGetAllResponseDto> GetAllStudentsByGroupIdAsync(int groupId)
    {
        var studentIds = await _studentGroupsRepo.GetAllStudentIdsByGroupIdAsync(groupId);
        var studentToUserIdDict = await GetStudentIdToUserIdDictionary(studentIds);

        var studentsList = new List<GroupStudentsGetAllResponseDto.StudentDto>();

        var context = _httpContextAccessor.HttpContext;
        var protocolString = context.Request.IsHttps ? "https" : "http";

        foreach (var studentToUserIdDictPair in studentToUserIdDict)
        {
            var studentId = studentToUserIdDictPair.Key;
            var userId = studentToUserIdDictPair.Value;

            var account = await _accountRepo.GetAccountByUserIdAsync(userId);
            var studentDto = new GroupStudentsGetAllResponseDto.StudentDto() {
                UserId = userId,
                StudentId = studentId,
                StudentName = account.Name,
                StudentSurname = account.Surname,
                StudentPatronymic = account.Patronymic,
                StudentPhoto = $"{protocolString}://{context.Request.Host}/api/v1/accounts/{account.Id}/photo"
            };
            studentsList.Add(studentDto);
        }

        return new GroupStudentsGetAllResponseDto() { Students = studentsList };
    }

    public async Task<Dictionary<int, int>> GetStudentIdToUserIdDictionary(List<int> studentIds)
    {
        var matchDict = new Dictionary<int, int>();
        foreach(int studentId in studentIds)
            matchDict.Add(studentId, await _studentRepo.GetUserIdByStudentId(studentId));

        return matchDict;
    }

    public async Task<int> CreateGroupAsync(GroupCreateDto createGroupDto)
    {
        var newGroup = new GroupModel()
        {
            GroupName = createGroupDto.GroupName,
            AddGuid = Guid.NewGuid().ToString(),
            TeacherId = await GetTeacherIdByUserIdAsync(createGroupDto.UserId),
        };

        var id = await _groupRepo.CreateEntityAsync(newGroup);
        return id;
    }

    public async Task AddCourseToGroupAsync(int groupId, GroupAddCourseToGroupDto groupAddCourseToGroupDto)
    {
        var courseId = groupAddCourseToGroupDto.CourseId;

        var groupCourseModel = new GroupCoursesModel()
        {
            CourseId = courseId,
            GroupId = groupId,
        };

        var id = await _groupCoursesRepo.CreateEntityAsync(groupCourseModel);
    }

    public async Task AddManyCoursesToGroupAsync(int groupId, GroupAddManyCoursesToGroupDto groupAddManyCoursesToGroupDto)
    {
        foreach (var course in groupAddManyCoursesToGroupDto.Courses)
            await AddCourseToGroupAsync(groupId, course);
    }

    public async Task DeleteGroupByIdAsync(int id)
    {
        await _groupRepo.DeleteEntityByIdAsync(id);
    }

    public async Task UpdateGroupAsync(int id, GroupUpdateDto updateGroupDto)
    {
        await _groupRepo.UpdateEntityAsync(id, updateGroupDto);
    }

    public async Task ConnectToGroupAsync(UserAuthInfo authInfo, GroupConnectDto connectToGroupDto)
    {
        var accountType = authInfo.UserType;
        if (accountType is not Auth.Enums.UserType.Student)
            throw new BadHttpRequestException(statusCode: 400, message: "Добавляться в группы могут только ученики");

        var userId = authInfo.Id;
        var groupId = connectToGroupDto.GroupId;
        await AddStudentToGroupAsync(userId, groupId);

        var group = await _groupRepo.GetEntityByIdAsync(groupId);
        var teacherUserId = (await _teacherRepo.GetEntityByIdAsync(group.TeacherId)).UserId;
        await CreateChatAfterConnectionToGroup(teacherUserId, userId);
    }

    private async Task CreateChatAfterConnectionToGroup(int teacherUserId, int studentUserId)
    {
        var newChat = new ChatModel() { User1Id = teacherUserId, User2Id = studentUserId };
        await _chatRepo.CreateEntityAsync(newChat);
    }

    public async Task DeleteCourseFromGroup(int groupId, GroupAddCourseToGroupDto groupAddCourseToGroupDto)
    {
        var conn = (await _groupCoursesRepo.GetAllEntitiesAsync()).Where(x => x.GroupId == groupId && x.CourseId == groupAddCourseToGroupDto.CourseId).FirstOrDefault();
        if (conn is not null)
            await _groupCoursesRepo.DeleteEntityByIdAsync(conn.Id);
    }

    public async Task<CourseGetAllDto> GetGroupCourses(int groupId)
    {
        var courses = await GetGroupCoursesModels(groupId);
        return _courseService.MapCoursesToGetAllDto(courses);
    }

    private async Task<List<CourseModel>> GetGroupCoursesModels(int groupId)
    {
        var courseIds = await _groupCoursesRepo.GetCourseIdsByGroupIdAsync(groupId);
        var courses = new List<CourseModel>();
        foreach (var courseId in courseIds)
            courses.Add(await _courseRepo.GetEntityByIdAsync(courseId));

        return courses;
    }

    public async Task<CourseGetAllDto> GetAvailableGroupCourses(int groupId)
    {
        var allCourses = await _courseRepo.GetAllEntitiesAsync();
        var takenGroupCourses = await GetGroupCoursesModels(groupId);

        var availableCourses = new List<CourseModel>();
        foreach (var course in allCourses)
            if (!takenGroupCourses.Contains(course))
                availableCourses.Add(course);

        return _courseService.MapCoursesToGetAllDto(availableCourses);
    }

    public async Task AddStudentToGroupAsync(int userId, int groupId)
    {
        if (await IsStudentAlreadyInGroupAsync(userId, groupId))
            throw new BadHttpRequestException(statusCode: 400, message: "Вы уже есть в этой группе");

        var newConnectionModel = new StudentGroupsModel()
        {
            GroupId = groupId,
            StudentId = await GetStudentIdByUserIdAsync(userId),
        };

        await _studentGroupsRepo.CreateEntityAsync(newConnectionModel);
    }

    public async Task<int> GetStudentIdByUserIdAsync(int userId)
    {
        var student = await _studentRepo.GetStudentByUserId(userId);
        if (student is null)
            throw new BadHttpRequestException(statusCode: 400, message: "Такого ученика не существует");

        return student.Id;
    }

    public async Task<int> GetTeacherIdByUserIdAsync(int userId)
    {
        var teacher = await _teacherRepo.GetTeacherByUserId(userId);
        if (teacher is null)
            throw new BadHttpRequestException(statusCode: 400, message: "Такого учителя не существует");

        return teacher.Id;
    }

    public async Task<bool> IsStudentAlreadyInGroupAsync(int userId, int groupId)
    {
        var studentId = await GetStudentIdByUserIdAsync(userId);
        var studentGroupConnection = await _studentGroupsRepo.GetStudentGroupByUserAndGroupIdAsync(studentId, groupId);
        return studentGroupConnection is not null;
    }

    // TODO: Make deleting student from gorup

    private GroupGetDto MapGroupToGetDto(GroupModel group)
    {
        return new GroupGetDto()
        {
            Id = group.Id,
            GroupName = group.GroupName,
            TeacherId = group.TeacherId,
        };
    }
}

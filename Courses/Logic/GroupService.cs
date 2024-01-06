using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dto;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;
using Backend.Courses.Dto;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Courses.Logic;

public class GroupService
{
    private IGroupRepo _groupRepo;
    private IStudentGroupsRepo _studentGroupsRepo;
    private IStudentRepo _studentRepo;
    private ITeacherRepo _teacherRepo;
    private IAccountRepo _accountRepo;
    private IGroupCoursesRepo _groupCoursesRepo;

    private IHttpContextAccessor _httpContextAccessor;

    public GroupService(
        IGroupRepo groupRepo,
        IStudentGroupsRepo studentGroupsRepo,
        IStudentRepo studentRepo,
        ITeacherRepo teacherRepo,
        IAccountRepo accountRepo,
        IGroupCoursesRepo groupCoursesRepo,
        IHttpContextAccessor httpContextAccessor)
    {
        _groupRepo = groupRepo;
        _studentGroupsRepo = studentGroupsRepo;
        _studentRepo = studentRepo;
        _teacherRepo = teacherRepo;
        _accountRepo = accountRepo;
        _groupCoursesRepo = groupCoursesRepo;

        _httpContextAccessor = httpContextAccessor;
    }

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

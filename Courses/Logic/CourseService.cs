using Backend.Courses.Dto;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;
using Backend.Base.Services.Interfaces;
using Backend.Base.Services;
using Backend.Auth.Dto;
using Backend.Auth.Dal.Interfaces;

namespace Backend.Courses.Logic;

public class CourseService
{
    private ICourseRepo _courseRepo;
    private ICourseChaptersRepo _courseChaptersRepo;
    private IStudentRepo _studentRepo;
    private IStudentGroupsRepo _studentGroupsRepo;
    private IGroupRepo _groupRepo;
    private IGroupCoursesRepo _groupCoursesRepo;

    private IFileManager _fileManager;
    private IHttpContextAccessor _httpContextAccessor;

    public CourseService(
        ICourseRepo courseRepo,
        ICourseChaptersRepo courseChaptersRepo,
        IStudentRepo studentRepo,
        IStudentGroupsRepo studentGroupsRepo,
        IGroupRepo groupRepo,
        IGroupCoursesRepo groupCoursesRepo,
        IFileManager fileManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _courseRepo = courseRepo;
        _courseChaptersRepo = courseChaptersRepo;
        _studentRepo = studentRepo;
        _studentGroupsRepo = studentGroupsRepo;
        _groupRepo = groupRepo;
        _groupCoursesRepo = groupCoursesRepo;

        _fileManager = fileManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CourseGetOneDto> GetCourseByIdAsync(int id)
    {
        var course = await _courseRepo.GetEntityByIdAsync(id);
        var getOneCourseDto = await MapCourseToGetOneDto(courseModel: course);
        return getOneCourseDto;
    }

    public async Task<CourseGetAllDto> GetAllCoursesAsync()
    {
        var courses = await _courseRepo.GetAllEntitiesAsync();
        return MapCoursesToGetAllDto(courses);
    }

    public async Task<CourseGetAllDto> GetAllUserCoursesByUserIdAsync(UserAuthInfo authInfo)
    {
        var courses = authInfo.UserType == Auth.Enums.UserType.Teacher
            ? await GetAllTeacherCoursesByUserIdAsync(authInfo.Id)
            : await GetCoursesOfAllGroups(await GetAllStudentGroupsByUserIdAsync(authInfo.Id));

        return MapCoursesToGetAllDto(courses);
    }

    public async Task<int> CreateCourseAsync(CourseCreateDto courseCreateDto)
    {
        var newCourse = new CourseModel()
        {
            Name = courseCreateDto.Name,
            Description = courseCreateDto.Description,
            CoursePhoto = StaticFilesManager.StandardPhotoPath, // Some time ago there was Заглушка
            CreatorId = courseCreateDto.CreatorId
        };

        return await _courseRepo.CreateEntityAsync(newCourse);
    }

    public async Task DeleteCourseByIdAsync(int id)
    {
        await _courseRepo.DeleteEntityByIdAsync(id);
    }

    public async Task UpdateCourseAsync(int id, CourseUpdateDto courseUpdateDto)
    {
        await _courseRepo.UpdateEntityAsync(id, courseUpdateDto);
    }

    private async Task<List<GroupModel>> GetAllStudentGroupsByUserIdAsync(int userId)
    {
        var student = await _studentRepo.GetStudentByUserId(userId);
        var studentGroupsIds = await _studentGroupsRepo.GetGroupIdsByStudentIdAsync(student.Id);

        var groups = new List<GroupModel>();
        foreach (var id in studentGroupsIds)
        {
            var group = await _groupRepo.GetEntityByIdAsync(id);
            groups.Add(group);
        }
        return groups;
    }

    private async Task<List<CourseModel>> GetAllTeacherCoursesByUserIdAsync(int userId)
    {
        return await _courseRepo.GetAllTeacherCoursesByCreatorId(userId);
    }

    private async Task<List<CourseModel>> GetCoursesOfAllGroups(List<GroupModel> groups)
    {
        var courses = new List<CourseModel>();
        foreach (var group in groups)
        {
            var groupId = group.Id;
            var courseIds = await _groupCoursesRepo.GetCourseIdsByGroupIdAsync(groupId);

            foreach (int courseId in courseIds)
                courses.Add(await _courseRepo.GetEntityByIdAsync(courseId));
        }

        return courses;
    }

    private async Task<CourseGetOneDto> MapCourseToGetOneDto(CourseModel courseModel)
    {
        //var courseChapters = await GetChaptersByCourseId(courseModel.Id);

        return new CourseGetOneDto()
        {
            Id = courseModel.Id,
            Name = courseModel.Name,
            Description = courseModel.Description,
        };
    }

    //private async Task<List<CourseChaptersModel>> GetChaptersByCourseId(int courseId)
    //{
    //    return await _courseChaptersRepo.GetChaptersByCourseIdAsync(courseId);
    //}

    //private List<CourseGetOneDto.ChapterDto> MapChaptersToDto(List<CourseChaptersModel> courseChaptersModels)
    //{
    //    return courseChaptersModels
    //        .Select(chapter => new CourseGetOneDto.ChapterDto() { Id = chapter.Id, Name = chapter.Name })
    //        .ToList();
    //}

    private CourseGetAllDto MapCoursesToGetAllDto(List<CourseModel> courseModels)
    {
        return new CourseGetAllDto()
        {
            CourseList = courseModels
                            .Select(MapCourseToCourseDtoForGetAllDto)
                            .ToList()
        };
    }

    private CourseGetAllDto.CourseDto MapCourseToCourseDtoForGetAllDto(CourseModel courseModel)
    {
        var context = _httpContextAccessor.HttpContext;
        var protocolString = context.Request.IsHttps ? "https" : "http";

        return new CourseGetAllDto.CourseDto() { 
            Id = courseModel.Id,
            Name = courseModel.Name,
            CoursePhoto = $"{protocolString}://{context.Request.Host}/api/v1/courses/{courseModel.Id}/photo",
            Description = courseModel.Description,
            CreatorId = courseModel.CreatorId
        };
    }

    public async Task SaveCoursePhotoOnServerAsync(int courseId, CourseUploadPhotoDto courseUploadPhotoDto)
    {
        var file = courseUploadPhotoDto.Photo;
        var filePath = await _fileManager.UploadFileAsync(file: file, fileType: Base.Enums.FileType.Photo);

        await _courseRepo.UpdatePhotoPathAsync(courseId, filePath);
    }

    public async Task<byte[]> GetCoursePhotoFromServerAsync(int courseId)
    {
        var account = await _courseRepo.GetEntityByIdAsync(courseId);
        var pathToFile = account.CoursePhoto;

        return await _fileManager.GetFileBytesAsync(path: pathToFile, fileType: Base.Enums.FileType.Photo);
    }
}

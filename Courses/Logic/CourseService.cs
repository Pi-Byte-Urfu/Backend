using Backend.Courses.Dto;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Logic;

public class CourseService
{
    private ICourseRepo _courseRepo;
    private ICourseChaptersRepo _courseChaptersRepo;

    public CourseService(ICourseRepo courseRepo, ICourseChaptersRepo courseChaptersRepo)
    {
        _courseRepo = courseRepo;
        _courseChaptersRepo = courseChaptersRepo;
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

    public async Task<int> CreateCourseAsync(CourseCreateDto courseCreateDto)
    {
        var newCourse = new CourseModel()
        {
            Name = courseCreateDto.Name,
            Description = courseCreateDto.Description,
            CoursePhoto = "Заглушка", // Fix later to normal photos
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
        return new CourseGetAllDto.CourseDto() { 
            Id = courseModel.Id,
            Name = courseModel.Name,
            CoursePhoto = courseModel.CoursePhoto,
            Description = courseModel.Description,
            CreatorId = courseModel.CreatorId
        };
    }
}

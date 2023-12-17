using Backend.Base.Dto;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;

using static Backend.Base.Dto.CourseGetOneDto;

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
        var getOneCourseDto = MapCourseToGetOneDto(courseModel: course);
        return getOneCourseDto;
    }

    public async Task<CourseGetAllDto> GetAllGroupsAsync()
    {
        var courses = await _courseRepo.GetAllEntitiesAsync();
        return MapCoursesToGetAllDto(courses);
    }

    public async Task<int> CreateCourseAsync(CourseCreateDto courseCreateDto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCourseByIdAsync(int id)
    {
        await _courseRepo.DeleteEntityByIdAsync(id);
    }

    public async Task UpdateGroupAsync(int id, CourseUpdateDto courseUpdateDto)
    {
        await _courseRepo.UpdateEntityAsync(id, courseUpdateDto);
    }

    private CourseGetOneDto MapCourseToGetOneDto(CourseModel courseModel)
    {
        return new CourseGetOneDto()
        {
            Id = courseModel.Id,
            Name = courseModel.Name,
            Description = courseModel.Description,
            CoursePhoto = courseModel.CoursePhoto,
            CreatorId = courseModel.CreatorId,
            Chapters = MapChaptersToDto(GetChaptersByCourseId(courseModel.Id))
        };
    }

    private List<CourseChaptersModel> GetChaptersByCourseId(int courseId)
    {
        throw new NotImplementedException();
    }

    private List<ChapterDto> MapChaptersToDto(List<CourseChaptersModel> courseChaptersModels)
    {
        throw new NotImplementedException();
    }

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
        throw new NotImplementedException();
    }
}

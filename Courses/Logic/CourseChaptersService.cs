using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;
using Backend.Courses.Dto;

namespace Backend.Courses.Logic;

public class CourseChaptersService
{
    private ICourseChaptersRepo _courseChaptersRepo;

    public CourseChaptersService(ICourseRepo courseRepo, ICourseChaptersRepo courseChaptersRepo)
    {
        _courseChaptersRepo = courseChaptersRepo;
    }

    public async Task<CourseChaptersGetByCourseIdResponse> GetCourseChaptersByCourseId(int courseId)
    {
        var chapters = await _courseChaptersRepo.GetChaptersByCourseIdAsync(courseId);
        return new CourseChaptersGetByCourseIdResponse()
        {
            CourseChapters = chapters.Select(MapChapterModelToGetOneDto).ToList(),
        };
    }

    public async Task<int> CreateChapterAsync(CourseChaptersCreateDto courseChapterCreateDto)
    {
        var newChapter = new CourseChaptersModel()
        {
            Name = courseChapterCreateDto.Name is not null ? courseChapterCreateDto.Name : string.Empty,
            CourseId = courseChapterCreateDto.CourseId,
        };
        var id = await _courseChaptersRepo.CreateEntityAsync(newChapter);
        return id;
    }

    public async Task DeleteChapter(int chapterId)
    {
        await _courseChaptersRepo.DeleteEntityByIdAsync(chapterId);
    }

    public async Task UpdateChapter(int chapterId, CourseChaptersUpdateDto courseChaptersUpdateDto)
    {
        await _courseChaptersRepo.UpdateEntityAsync(chapterId, courseChaptersUpdateDto);
    }

    public CourseChaptersGetOneDto MapChapterModelToGetOneDto(CourseChaptersModel courseChaptersModel)
    {
        return new CourseChaptersGetOneDto()
        {
            Id = courseChaptersModel.Id,
            Name = courseChaptersModel.Name,
        };
    }
}

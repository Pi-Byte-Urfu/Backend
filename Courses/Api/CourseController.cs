using Backend.Courses.Dto;
using Backend.Courses.Logic;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Courses.Api;


[Route("api/v1/courses")]
[ApiController]
public class CourseController
{
    private CourseService _courseService;

    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<CourseGetOneDto> GetCourseByIdAsync([FromRoute] int id)
    {
        return await _courseService.GetCourseByIdAsync(id);
    }

    [HttpGet]
    public async Task<CourseGetAllDto> GetAllCourses()
    {
        return await _courseService.GetAllCoursesAsync();
    }

    [HttpPost]
    public async Task<int> CreateCourse([FromBody] CourseCreateDto createCourseDto)
    {
        return await _courseService.CreateCourseAsync(createCourseDto);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task DeleteCourseById([FromRoute] int id)
    {
        await _courseService.DeleteCourseByIdAsync(id);
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task UpdateCourse([FromRoute] int id, [FromBody] CourseUpdateDto updateCourseDto)
    {
        await _courseService.UpdateCourseAsync(id, updateCourseDto);
    }
}

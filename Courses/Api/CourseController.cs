using Backend.Base.Dto;
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
    public async Task<CourseGetAllDto> GetAllCourses()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<CourseGetOneDto> GetOneCourse([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
}

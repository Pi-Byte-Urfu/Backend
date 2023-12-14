using Backend.Logic.Courses;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Courses;


[Route("api/v1/courses")]
[ApiController]
public class CourseController
{
    private CourseService _courseService;

    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }


}

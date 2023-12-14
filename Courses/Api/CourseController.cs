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


}

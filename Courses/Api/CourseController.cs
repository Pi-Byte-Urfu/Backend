using Backend.Auth.Dto;
using Backend.Base.Dto;
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

    [HttpGet]
    [Route(template: "users")]
    public async Task<CourseGetAllDto> GetAllUserCoursesByUserId([FromHeader] UserAuthInfo authInfo)
    {
        return await _courseService.GetAllUserCoursesByUserIdAsync(authInfo);
    }

    [HttpPost]
    public async Task<BaseIdDto> CreateCourse([FromBody] CourseCreateDto createCourseDto)
    {
        var id = await _courseService.CreateCourseAsync(createCourseDto);
        return new BaseIdDto { Id = id };
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

    [HttpPost]
    [Route("{id}/photo")]
    public async Task<IResult> UploadCoursePhoto([FromRoute] int id, [FromForm] CourseUploadPhotoDto courseUploadPhoto)
    {
        await _courseService.SaveCoursePhotoOnServerAsync(id, courseUploadPhoto);
        return Results.Ok();
    }

    [HttpGet]
    [Route("{id}/photo")]
    public async Task<IResult> GetCoursePhoto([FromRoute] int id)
    {
        var photoBytes = await _courseService.GetCoursePhotoFromServerAsync(id);
        return Results.File(fileContents: photoBytes, contentType: "image/png");
    }
}
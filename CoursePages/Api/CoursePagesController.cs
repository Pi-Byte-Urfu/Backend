using Backend.Base.Dto;
using Backend.CoursePages.Dto;
using Backend.CoursePages.Enums;
using Backend.CoursePages.Logic;

using Microsoft.AspNetCore.Mvc;

namespace Backend.CoursePages.Api;

[ApiController]
[Route(template: "/api/v1/pages")]
public class CoursePagesController
{
    private readonly CoursePagesService _coursePagesService;

    public CoursePagesController(CoursePagesService coursePagesService)
    {
        _coursePagesService = coursePagesService;
    }

    [HttpGet]
    [Route(template: "{chapterId}")]
    public async Task<CoursePageGetAllDto> GetAllCoursePagesByChapterId(int chapterId)
    {
        var coursePages = await _coursePagesService.GetAllCoursePagesByChapterId(chapterId);
        return coursePages;
    }

    [HttpPost]
    public async Task<BaseIdDto> CreateCoursePage(CoursePageCreateDto coursePageCreateDto)
    {
        var idDto = await _coursePagesService.CoursePageCreateDto(coursePageCreateDto);
        return idDto;
    }

    [HttpGet]
    [Route(template: "0/{pageId}")]
    public async Task<CourseTheoryPageGetOneDto> GetCourseTheoryPage(int pageId)
    {
        var theoryPageDto = await _coursePagesService.GetTheoryPage(pageId);
        return theoryPageDto;
    }

    [HttpGet]
    [Route(template: "1/{pageId}")]
    public async Task<CourseTestPageGetOneDto> GetCourseTestPage(int pageId)
    {
        var testPageDto = await _coursePagesService.GetTestPage(pageId);
        return testPageDto;
    }

    [HttpGet]
    [Route(template: "2/{pageId}")]
    public async Task<CourseTaskPageGetOneDto> GetCourseTaskPage(int pageId)
    {
        var taskPageDto = await _coursePagesService.GetTaskPage(pageId);
        return taskPageDto;
    }

    [HttpPatch]
    [Route(template: "0/{pageId}")]
    public async Task<IResult> UpdateTheoryPage(int pageId, CourseTheoryPageUpdateDto courseTheoryPageUpdateDto)
    {
        await _coursePagesService.UpdateTheoryPage(pageId, courseTheoryPageUpdateDto);
        return Results.Ok();
    }

    [HttpPatch]
    [Route(template: "1/{pageId}")]
    public async Task<IResult> UpdateTestPage(int pageId, CourseTestPageUpdateDto courseTestPageUpdateDto)
    {
        await _coursePagesService.UpdateTestPage(pageId, courseTestPageUpdateDto);
        return Results.Ok();
    }

    [HttpPatch]
    [Route(template: "2/{pageId}")]
    public async Task<IResult> UpdateTaskPage(int pageId, CourseTaskPageUpdateDto courseTaskPageUpdateDto)
    {
        await _coursePagesService.UpdateTaskPage(pageId, courseTaskPageUpdateDto);
        return Results.Ok();
    }

    [HttpDelete]
    [Route("{pageId}")]
    public async Task<IResult> DeletePage([FromRoute] int pageId)
    {
        await _coursePagesService.DeletePage(pageId);
        return Results.Ok();
    }
}

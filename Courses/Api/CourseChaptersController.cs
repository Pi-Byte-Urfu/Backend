﻿using Backend.Courses.Dto;
using Backend.Courses.Logic;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Courses.Api;

[Route("api/v1/chapters")]
[ApiController]
public class CourseChaptersController
{
    private CourseChaptersService _courseChaptersService;

    public CourseChaptersController(CourseChaptersService courseChaptersService)
    {
        _courseChaptersService = courseChaptersService;
    }

    [HttpGet]
    [Route(template: "course/{courseId}")]
    public async Task<CourseChaptersGetByCourseIdResponse> GetAllChaptersByCourseId(int courseId)
    {
        return await _courseChaptersService.GetCourseChaptersByCourseId(courseId);
    }

    [HttpPost]
    public async Task<int> CreateChapter([FromBody] CourseChaptersCreateDto courseChaptersCreateDto)
    {
        return await _courseChaptersService.CreateChapterAsync(courseChaptersCreateDto);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task DeleteChapterById([FromRoute] int id)
    {
        await _courseChaptersService.DeleteChapter(id);
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task UpdateCourse([FromRoute] int id, [FromBody] CourseChaptersUpdateDto courseChaptersUpdateDto)
    {
        await _courseChaptersService.UpdateChapter(id, courseChaptersUpdateDto);
    }
}
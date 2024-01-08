using Backend.Base.Services;
using Backend.Base.Services.Interfaces;
using Backend.CoursePages.Dto;
using Backend.CoursePages.Logic;

using Microsoft.AspNetCore.Mvc;

namespace Backend.CoursePages.Api;

[ApiController]
[Route("/api/v1/editor")]
public class Md5EditorFilesController
{
    private Md5EditorFilesService _md5EditorFilesService;

    public Md5EditorFilesController(Md5EditorFilesService md5EditorFilesService)
    {
        _md5EditorFilesService = md5EditorFilesService;    
    }

    [HttpGet]
    [Route("photos/{photoGuid}")]
    public async Task<IResult> GetPhoto([FromRoute] string photoGuid)
    {
        var fileBytes = await _md5EditorFilesService.GetPhotoBytes(photoGuid);
        return Results.File(fileContents: fileBytes, contentType: "image/png");
    }

    [HttpGet]
    [Route("documents/{fileGuid}")]
    public async Task<IResult> GetFile([FromRoute] string fileGuid)
    {
        var fileBytes = await _md5EditorFilesService.GetFileBytes(fileGuid);
        return Results.File(fileContents: fileBytes, contentType: "application/octet-stream");
    }

    [HttpPost]
    [Route("photos")]
    public async Task<UrlToGetFileDto> UploadPhoto([FromForm] Md5EditorUploadFileDto fileDto)
    {
        return await _md5EditorFilesService.UploadPhoto(fileDto);
    }

    [HttpPost]
    [Route("documents")]
    public async Task<UrlToGetFileDto> UploadFile([FromForm] Md5EditorUploadFileDto fileDto)
    {
        return await _md5EditorFilesService.UploadFile(fileDto);
    }
}

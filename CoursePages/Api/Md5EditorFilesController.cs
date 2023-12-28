using Backend.Base.Services;
using Backend.Base.Services.Interfaces;
using Backend.CoursePages.Dto;

using Microsoft.AspNetCore.Mvc;

namespace Backend.CoursePages.Api;

[ApiController]
[Route("/api/v1/editor")]
public class Md5EditorFilesController
{
    private IFileManager _fileManager;
    private IHttpContextAccessor _httpContextAccessor;

    public Md5EditorFilesController(IFileManager fileManager, IHttpContextAccessor httpContextAccessor)
    {
        _fileManager = fileManager;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    [Route("photos/{photoGuid}")]
    public async Task<IResult> GetPhoto([FromRoute] string photoGuid)
    {
        var directoryToFile = StaticFilesManager.GetFileDirectoryPath(Base.Enums.FileType.Photo);
        var pathToFile = Path.Combine(directoryToFile, photoGuid);

        var fileBytes = await _fileManager.GetFileBytesAsync(pathToFile, Base.Enums.FileType.Photo);
        return Results.File(fileContents: fileBytes, contentType: "image/png");
    }

    [HttpGet]
    [Route("documents/{fileGuid}")]
    public async Task<IResult> GetFile([FromRoute] string fileGuid)
    {
        var directoryToFile = StaticFilesManager.GetFileDirectoryPath(Base.Enums.FileType.Document);
        var pathToFile = Path.Combine(directoryToFile, fileGuid);

        var fileBytes = await _fileManager.GetFileBytesAsync(pathToFile, Base.Enums.FileType.Document);
        return Results.File(fileContents: fileBytes, contentType: "application/octet-stream");
    }

    [HttpPost]
    [Route("photos")]
    public async Task<UrlToGetFileDto> UploadPhoto([FromForm] Md5EditorUploadFileDto fileDto)
    {
        var file = fileDto.FormFile;
        var pathWhereSaved = await _fileManager.UploadFileAsync(file, Base.Enums.FileType.Photo);
        var fileGuid = GetGuidFromFilePath(pathWhereSaved);

        var context = _httpContextAccessor.HttpContext;
        var protocolString = context.Request.IsHttps ? "https" : "http";
        var urlToGetFile = $"{protocolString}://{context.Request.Host}/api/v1/editor/photos/{fileGuid}";

        return new UrlToGetFileDto { UrlToGet =  urlToGetFile };
    }

    [HttpPost]
    [Route("documents")]
    public async Task<UrlToGetFileDto> UploadFile([FromForm] Md5EditorUploadFileDto fileDto)
    {
        var file = fileDto.FormFile;
        var pathWhereSaved = await _fileManager.UploadFileAsync(file, Base.Enums.FileType.Document);
        var fileGuid = GetGuidFromFilePath(pathWhereSaved);

        var context = _httpContextAccessor.HttpContext;
        var protocolString = context.Request.IsHttps ? "https" : "http";
        var urlToGetFile = $"{protocolString}://{context.Request.Host}/api/v1/editor/documents/{fileGuid}";

        return new UrlToGetFileDto { UrlToGet = urlToGetFile };
    }

    private string GetGuidFromFilePath(string filePath) => filePath.Split(['/', '\\']).Last();
}

using Backend.Base.Services.Interfaces;
using Backend.Base.Services;
using Backend.CoursePages.Dto;

namespace Backend.CoursePages.Logic;

public class Md5EditorFilesService
{
    private IFileManager _fileManager;
    private IHttpContextAccessor _httpContextAccessor;

    public Md5EditorFilesService(IFileManager fileManager, IHttpContextAccessor httpContextAccessor)
    {
        _fileManager = fileManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<byte[]> GetPhotoBytes(string photoGuid)
    {
        var directoryToFile = StaticFilesManager.GetFileDirectoryPath(Base.Enums.FileType.Photo);
        var pathToFile = Path.Combine(directoryToFile, photoGuid);

        var fileBytes = await _fileManager.GetFileBytesAsync(pathToFile, Base.Enums.FileType.Photo);
        return fileBytes;
    }

    public async Task<byte[]> GetFileBytes(string fileGuid)
    {
        var directoryToFile = StaticFilesManager.GetFileDirectoryPath(Base.Enums.FileType.Document);
        var pathToFile = Path.Combine(directoryToFile, fileGuid);

        var fileBytes = await _fileManager.GetFileBytesAsync(pathToFile, Base.Enums.FileType.Document);
        return fileBytes;
    }

    public async Task<UrlToGetFileDto> UploadPhoto(Md5EditorUploadFileDto fileDto)
    {
        var file = fileDto.FormFile;
        var pathWhereSaved = await _fileManager.UploadFileAsync(file, Base.Enums.FileType.Photo);
        var fileGuid = GetGuidFromFilePath(pathWhereSaved);

        var context = _httpContextAccessor.HttpContext;
        var protocolString = context.Request.IsHttps ? "https" : "http";
        var urlToGetFile = $"{protocolString}://{context.Request.Host}/api/v1/editor/photos/{fileGuid}";

        return new UrlToGetFileDto { UrlToGet = urlToGetFile };
    }

    public async Task<UrlToGetFileDto> UploadFile(Md5EditorUploadFileDto fileDto)
    {
        var file = fileDto.FormFile;
        var pathWhereSaved = await _fileManager.UploadFileAsync(file, Base.Enums.FileType.Document);
        var fileGuid = GetGuidFromFilePath(pathWhereSaved);

        var context = _httpContextAccessor.HttpContext;
        var protocolString = context.Request.IsHttps ? "https" : "http";
        var urlToGetFile = $"{protocolString}://{context.Request.Host}/api/v1/editor/documents/{fileGuid}";

        return new UrlToGetFileDto { UrlToGet = urlToGetFile };
    }

    public string GetApiUrlToFile(string filePath)
    {
        var context = _httpContextAccessor.HttpContext;
        var protocolString = context.Request.IsHttps ? "https" : "http";

        var guid = GetGuidFromFilePath(filePath);
        return $"{protocolString}://{context.Request.Host}/api/v1/editor/documents/{guid}";
    }

    public string GetApiUrlToPhoto(string filePath)
    {
        var context = _httpContextAccessor.HttpContext;
        var protocolString = context.Request.IsHttps ? "https" : "http";

        var guid = GetGuidFromFilePath(filePath);
        return $"{protocolString}://{context.Request.Host}/api/v1/editor/photos/{guid}";
    }

    public string GetGuidFromFilePath(string filePath) => filePath.Split(['/', '\\']).Last();
}

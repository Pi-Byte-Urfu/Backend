using Backend.Base.Enums;

namespace Backend.Base.Services.Interfaces;

public interface IFileManager
{
    public Task<string> UploadFileAsync(IFormFile file, FileType fileType);
    public Task<byte[]> GetFileBytesAsync(string path, FileType fileType);
}
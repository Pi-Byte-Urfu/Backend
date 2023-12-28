using Backend.Base.Enums;
using Backend.Base.Services.Interfaces;

namespace Backend.Base.Services;

public class StaticFilesManager : IFileManager
{
    public static readonly string StandardPhotoPath = $"{AppDomain.CurrentDomain.BaseDirectory}/static_files/photos/standard.jpg";

    public async Task<byte[]> GetFileBytesAsync(string path, FileType fileType)
    {
        using (var fileStream = new FileStream(path: path, mode: FileMode.Open))
        {
            var bytesAmount = fileStream.Length;
            var photoBytes = new byte[bytesAmount];
            await fileStream.ReadAsync(photoBytes);

            return photoBytes;
        }
    }

    public async Task<string> UploadFileAsync(IFormFile file, FileType fileType)
    {
        var fileDirectoryPath = GetFileDirectoryPath(fileType);
        CreateDirectoryIfNotExists(fileDirectoryPath);
        var guid = GetRandomGuid();
        var extenstion = Path.GetExtension(file.FileName);
        
        var filePath = GeneratePath(fileDirectoryPath, guid, extenstion);

        using (var fileStream = new FileStream(path: filePath, mode: FileMode.CreateNew))
        {
            await file.CopyToAsync(fileStream);
        }

        return filePath;
    }

    public static string GetFileDirectoryPath(FileType fileType)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        return fileType switch
        {
            FileType.Photo => $"{baseDirectory}/static_files/photos",
            FileType.Document => $"{baseDirectory}/static_files/documents",
            _ => throw new Exception("Don't know this file type"),
        };
    }

    private static string GeneratePath(string directory, string name, string? extension)
    {
        if (extension is not null)
            return Path.Combine(directory, name) + extension;

        return Path.Combine(directory, name);
    }

    private static string GetRandomGuid()
    {
        return Guid.NewGuid().ToString("N");
    }

    private static void CreateDirectoryIfNotExists(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}
namespace Backend.CoursePages.Dto;

public class Md5EditorUploadFileDto
{
    public IFormFile FormFile {  get; set; }
}

public class UrlToGetFileDto
{
    public string UrlToGet { get; set; }
}

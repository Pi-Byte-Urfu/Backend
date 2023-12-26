using Backend.Base.Dto;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Dto;

public class CourseChaptersGetByCourseIdResponse
{
    public List<CourseChaptersGetOneDto> CourseChapters { get; set; }
}

public class CourseChaptersGetOneDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CourseChaptersCreateDto
{
    public string? Name { get; set; }
    public int CourseId { get; set; }
}

public class CourseChaptersUpdateDto : BaseUpdateDto<CourseChaptersModel>
{
    public string? Name { get; set; }

    public override CourseChaptersModel UpdateEntity(CourseChaptersModel entityToUpdate)
    {
        if (Name is not null)
            entityToUpdate.Name = Name;

        return entityToUpdate;
    }
}
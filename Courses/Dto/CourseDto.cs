using Backend.Base.Dto;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Dto;

public class CourseGetOneDto : BaseDto
{
    public class ChapterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CoursePhoto { get; set; }
    public int CreatorId { get; set; }
    public List<ChapterDto> Chapters { get; set; }
}

public class CourseGetAllDto : BaseDto
{
    public class CourseDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public string CoursePhoto { get; set; }
        public int CreatorId { get; set; }
    }

    public required List<CourseDto> CourseList { get; set; }
}

public class CourseCreateDto : BaseDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int CreatorId { get; set; }
}

public class CourseUpdateDto : BaseUpdateDto<CourseModel>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public override CourseModel UpdateEntity(CourseModel entityToUpdate)
    {
        if (Name is not null)
            entityToUpdate.Name = Name;
        if (Description is not null)
            entityToUpdate.Description = Description;

        return entityToUpdate;
    }
}
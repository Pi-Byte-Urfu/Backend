using Backend.Courses.Dal.Models;

namespace Backend.Base.Dto;

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

}

public class CourseUpdateDto : BaseUpdateDto<CourseModel>
{
    public override CourseModel UpdateEntity(CourseModel entityToUpdate)
    {
        throw new NotImplementedException();
    }
}
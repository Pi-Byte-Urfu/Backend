﻿using Backend.Base.Dto;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Dto;

public class GroupGetDto : BaseDto
{
    public int Id { get; set; }
    public string GroupName { get; set; }
    public int TeacherId { get; set; }
}

public class GroupCreateDto : BaseDto
{
    public string GroupName { get; set; }
    public int UserId { get; set; }
}

public class GroupConnectDto : BaseDto
{
    public int GroupId { get; set; }
}

public class GroupStudentsGetAllResponseDto : BaseDto
{
    public class StudentDto
    {
        public int UserId { get; set; }
        public int StudentId { get; set; }

        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string StudentPatronymic { get; set; }

        public string StudentPhoto { get; set; }
    }

    public List<StudentDto> Students { get; set; }
}

public class GroupUpdateDto : BaseUpdateDto<GroupModel>
{
    public string GroupName { get; set; }

    public override GroupModel UpdateEntity(GroupModel entityToUpdate)
    {
        if (GroupName is not null)
            entityToUpdate.GroupName = entityToUpdate.GroupName;

        return entityToUpdate;
    }
}

public class GroupAddCourseToGroupDto
{
    public int CourseId { get; set; }
}

public class GroupAddManyCoursesToGroupDto
{
    public List<GroupAddCourseToGroupDto> Courses { get; set; }
}
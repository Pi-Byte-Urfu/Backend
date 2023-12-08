using Backend.Controllers.Base;
using Backend.DataTransferObjects;
using Backend.Models;
using Backend.Repositories.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace Backend.Controllers;

[ApiController]
[Route("api/v1/groups")]
public class GroupController : BaseCrudController<GroupModel>
{
    public GroupController(IGroupRepo repo) : base(repo)
    {

    }

    //[HttpGet]
    //public async Task<IResult> GetGroups()
    //{
    //    return Results.Ok(await _database.Groups.ToListAsync());
    //}

    //[HttpPost]
    //public async Task<IResult> CreateGroup(CreateGroupDto createGroupDto)
    //{
    //    var groupModel = new GroupModel()
    //    {
    //        GroupName = createGroupDto.GroupName,
    //        AddGuid = Guid.NewGuid().ToString(),
    //        StudentIds = "",
    //        TeacherId = createGroupDto.TeacherId,
    //    };

    //    var addingGroup = await _database.Groups.AddAsync(groupModel);
    //    var gettingValuesFromDatabase = await addingGroup.GetDatabaseValuesAsync(); // We need make teacher endpoints earlier
    //    var id = gettingValuesFromDatabase["Id"];

    //    await _database.SaveChangesAsync();

    //    return Results.Ok(new Dictionary<string , int>{ ["id"] = (int)id});
    //}

    //[HttpPost]
    //[Route("connect/{guid}")]
    //public async Task<IResult> ConnectToGroup([FromRoute] string guid,[FromBody] ConnectToGroupDto connectToGroupDto)
    //{
    //    var group = await _database.Groups.FirstOrDefaultAsync(group => group.AddGuid == guid);
    //    AddStudentToGroup(group, connectToGroupDto.StudentId);
    //    return Results.Ok();
    //}

    //[NonAction]
    //private void AddStudentToGroup(GroupModel groupModel, int studentId)
    //{
    //    var studentsIdsList = groupModel.StudentIds.Split(',').ToList();
    //    studentsIdsList.Append(studentId.ToString());
    //    groupModel.StudentIds = string.Join(",", studentsIdsList);
    //}
}

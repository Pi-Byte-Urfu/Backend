using Backend.Logic.ControllerLogicServices;

using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/v1/groups")]
public class GroupController
{
    private GroupService _groupService;

    public GroupController(GroupService groupService)
    {
        _groupService = groupService;
    }

    //[HttpPost]
    //[Route("connect/{guid}")]
    //public async Task<IActionResult> ConnectToGroup([FromRoute] string guid, [FromBody] ConnectToGroupDto connectToGroupDto)
    //{
    //    var group = await _repo.GetGroupByGuidAsync(guid);

    //    AddStudentToGroup(group, connectToGroupDto.StudentId);
    //    return Ok();
    //}

    //public override Task<IActionResult> Create<AccountCreatingDto>([FromBody] AccountCreatingDto entity)
    //{
    //    var newAccount = new AccountModel() { Name = entity.Name,
    //        Surname = entity.Surname,
    //        UserId = entity.UserId,
    //    };
    //}

    //[NonAction]
    //private void AddStudentToGroup(GroupModel groupModel, int studentId)
    //{
    //    var studentsIdsList = groupModel.StudentIds.Split(',').ToList();
    //    studentsIdsList.Append(studentId.ToString());
    //    groupModel.StudentIds = string.Join(",", studentsIdsList);
    //}
}

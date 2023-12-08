using Backend.Controllers.Base;
using Backend.DataTransferObjects;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Backend.Utils;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : BaseCrudController<UserModel>
{
    private IUserService _userService;
    private IUserRepo _userRepo;

    public UserController(IUserService userService, IUserRepo userRepo) : base(userRepo)
    {
        _userService = userService;
        _userRepo = userRepo;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IResult> Login(UserLoginDto userLoginInfo)
    {
        var id = await _userService.Login(userLoginInfo);
        return Results.Ok(id);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IResult> Register(UserRegistrationDto user)
    {
        var id = await _userService.Register(user);
        await _userService.AddToNeeededUserTypeEntityAsync(await _userRepo.GetEntityByIdAsync(id));
        return Results.Ok(id);
    }

    [NonAction]
    public async override Task<IActionResult> Create([FromBody] UserModel entity)
    {
        throw new NotImplementedException();
    }

    [NonAction]
    public override Task<IActionResult> Update([FromRoute] int id, [FromBody] JsonPatchDocument jsonPatchObject)
    {
        throw new NotImplementedException();
    }

    [NonAction]
    public override Task<IActionResult> Delete([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
}
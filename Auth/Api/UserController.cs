using Backend.Auth.Dto;
using Backend.Auth.Logic;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Auth.Api;

[ApiController]
[Route("api/v1/users")]
public class UserController
{
    private UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IResult> Login(UserLoginDto userLoginInfo)
    {
        var authResponseDto = await _userService.Login(userLoginInfo);
        return Results.Ok(authResponseDto);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IResult> Register(UserRegistrationDto user)
    {
        var authResponseDto = await _userService.Register(user);
        return Results.Ok(authResponseDto);
    }

    [HttpPost]
    [Route("change-password")]
    public async Task<IResult> ChangePassword([FromHeader] UserAuthInfo authInfo, string oldPassword, string newPassword)
    {
        await _userService.ChangePassword(authInfo.Id, oldPassword, newPassword);
        return Results.Ok();
    }
}
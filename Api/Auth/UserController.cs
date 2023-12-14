using Backend.DataTransferObjects.Auth;
using Backend.Logic.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Auth;

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
}
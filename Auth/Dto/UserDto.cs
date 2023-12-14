using Backend.Auth.Enums;
using Backend.Base.Dto;

namespace Backend.Auth.Dto;

public class UserRegistrationDto : BaseDto
{
    public string Email { get; init; }
    public string Password { get; init; }
    public UserType UserType { get; init; }
}

public class UserAuthResponseDto
{
    public int Id { get; init; }
    public UserType UserType { get; init; }
}

public class UserLoginDto : BaseDto
{
    public string Email { get; init; }
    public string Password { get; init; }
}
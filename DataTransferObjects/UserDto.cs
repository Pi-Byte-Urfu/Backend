using Backend.Dal.Enums;

namespace Backend.DataTransferObjects;

public class UserRegistrationDto : Dto
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

public class UserLoginDto : Dto
{
    public string Email { get; init; }
    public string Password { get; init; }
}
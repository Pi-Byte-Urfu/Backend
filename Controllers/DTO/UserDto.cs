﻿using Backend.Enums;

namespace Backend.Controllers.DTO;

public class UserRegistrationDto
{ 
    public string Email { get; init; }
    public string Password { get; init; }
}

public class UserLoginDto
{
    public string Email { get; init; }
    public string Password { get; init; }
}
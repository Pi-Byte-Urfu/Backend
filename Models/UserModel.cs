﻿namespace Backend.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public int HashedPassword { get; set; } 
}

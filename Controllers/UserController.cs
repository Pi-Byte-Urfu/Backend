using Backend.Controllers.DTO;
using Backend.Models;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/v1/user")]
public class UserController : Controller
{
    private AppDatabase _database;

    public UserController(AppDatabase database)
    {
        _database = database;
    }


    [HttpPost]
    [Route("register")]
    public List<UserModel> Register(UserRegistrationDto user)
    {
        return _database.Users.ToList();

    }
}
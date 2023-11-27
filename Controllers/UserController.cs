using System.Security.Cryptography;
using System.Text;

using Backend.Controllers.DTO;
using Backend.Models;
using Backend.Utils;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/v1/user")]
public class UserController : Controller
{
    private AppDatabase _database;
    private IConfiguration _configuration;

    public UserController(AppDatabase database, IConfiguration configuration)
    {
        _database = database;
        _configuration = configuration;
    }


    [HttpPost]
    [Route("register")]
    public int Register(UserRegistrationDto user)
    {
        var hashedPassword = user.Password.GetHashCode();
        var newUser = new UserModel() { Email = user.Email, HashedPassword = hashedPassword };
        
        var passwordModel = GetPasswordModel(user.Password);

        var id = _database.Users.Add(newUser).Entity.Id;
        _database.Passwords.Add(passwordModel);

        _database.SaveChanges();
        return id;
    }

    private PasswordModel GetPasswordModel(string password)
    {
        var hashedPassword = password.GetHashCode();

        var aesSettings = AESEncryption.GetAesSettingsFromConfig(_configuration);
        var cryptedPassword = AESEncryption.EncryptString(aesSettings, password);

        return new PasswordModel() { HashedPassword = hashedPassword, CryptedPassword = cryptedPassword };
    }
}
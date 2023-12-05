using Backend.DataTransferObjects;
using Backend.Models;
using Backend.Utils;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/v1/users")]
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
    [Route("login")]
    public async Task<IResult> Login(UserLoginDto userLoginInfo)
    {
        var user = await _database.Users.FirstOrDefaultAsync(user => user.Email == userLoginInfo.Email);
        if (user is null)
            throw new BadHttpRequestException("There are no user with this email", 422);

        var isCorrectLoginInfo = await IsPasswordCorrect(user, userLoginInfo);
        if (isCorrectLoginInfo)
            return Results.Ok(new Dictionary<string, int>() { ["id"] = user.Id });

        throw new BadHttpRequestException("Incorrect password", 400);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IResult> Register(UserRegistrationDto user)
    {
        if (user.Email == "string")
            throw new BadHttpRequestException("This email is string - test error", 402); // TestThing

        var hashedPassword = user.Password.GetHashCode();
        var newUser = new UserModel() { Email = user.Email, HashedPassword = hashedPassword };

        var passwordModel = GetPasswordModel(user.Password);

        var entityEntry = await _database.Users.AddAsync(newUser);
        await _database.Passwords.AddAsync(passwordModel);
        await _database.SaveChangesAsync();

        return Results.Ok(new Dictionary<string, int>(){["id"] = entityEntry.Entity.Id});
    }

    [NonAction]
    private PasswordModel GetPasswordModel(string password)
    {
        var hashedPassword = password.GetHashCode();

        var aesSettings = AESEncryption.GetAesSettingsFromConfig(_configuration);
        var cryptedPassword = AESEncryption.EncryptString(aesSettings, password);

        return new PasswordModel() { HashedPassword = hashedPassword, CryptedPassword = cryptedPassword };
    }   

    [NonAction]
    private async Task<bool> IsPasswordCorrect(UserModel user, UserLoginDto userLoginInfo)
    {
        var passwordFromDatabaseObject = await _database.Passwords.FirstOrDefaultAsync(password => password.HashedPassword == user.HashedPassword);
        if (passwordFromDatabaseObject is null)
            throw new BadHttpRequestException("Can't get this password from db - (no idea why btw)", statusCode: 422);

        var aesSettings = AESEncryption.GetAesSettingsFromConfig(_configuration);
        return userLoginInfo.Password == AESEncryption.DecryptString(aesSettings, passwordFromDatabaseObject.CryptedPassword);
    }
}
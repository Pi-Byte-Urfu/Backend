using Backend.DataTransferObjects;
using Backend.Models;

namespace Backend.Services.Interfaces;

public interface IPasswordHelperService
{
    public int GetPasswordHash(string password);
    public Task<int> AddHashedPasswordToDatabaseAsync(string password);
    public Task<bool> IsPasswordCorrectAsync(UserModel user, UserLoginDto userLoginInfo);
}

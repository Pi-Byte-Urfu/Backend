using Backend.Dal.Auth.Models;
using Backend.DataTransferObjects.Auth;

namespace Backend.Logic.Auth.Interfaces;

public interface IPasswordHelperService
{
    public int GetPasswordHash(string password);
    public Task<int> AddHashedPasswordToDatabaseAsync(string password);
    public Task<bool> IsPasswordCorrectAsync(UserModel user, UserLoginDto userLoginInfo);
}

using Backend.Dal.Models;
using Backend.DataTransferObjects;

namespace Backend.Services.Interfaces;

public interface IPasswordHelperService
{
    public int GetPasswordHash(string password);
    public Task<int> AddHashedPasswordToDatabaseAsync(string password);
    public Task<bool> IsPasswordCorrectAsync(UserModel user, UserLoginDto userLoginInfo);
}

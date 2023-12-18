using Backend.Auth.Dal.Models;
using Backend.Auth.Dto;

namespace Backend.Auth.Logic.Interfaces;

public interface IPasswordHelperService
{
    public int GetPasswordHash(string password);
    public Task<int> AddHashedPasswordToDatabaseAsync(string password);
    public Task<bool> IsPasswordCorrectAsync(UserModel user, UserLoginDto userLoginInfo);
}

using Backend.DataTransferObjects;
using Backend.Models;

namespace Backend.Services.Interfaces;

public interface IUserService
{
    public Task<int> Register(UserRegistrationDto user);
    public Task<int> Login(UserLoginDto userLoginInfo);
    public Task AddToNeeededUserTypeEntityAsync(UserModel user);
}

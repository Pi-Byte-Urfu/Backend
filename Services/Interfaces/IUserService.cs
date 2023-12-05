using Backend.DataTransferObjects;

namespace Backend.Services.Interfaces;

public interface IUserService
{
    public Task<int> Register(UserRegistrationDto user);
    public Task<int> Login(UserLoginDto userLoginInfo);
}

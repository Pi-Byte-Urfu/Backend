using Backend.DataTransferObjects;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class UserService : IUserService
{
    public Task<int> Login(UserLoginDto userLoginInfo)
    {
        throw new NotImplementedException();
    }

    public Task<int> Register(UserRegistrationDto user)
    {
        throw new NotImplementedException();
    }
}
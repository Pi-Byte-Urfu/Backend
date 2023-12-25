using Backend.Auth.Dto;

namespace Backend.Base.Services.Interfaces;

public interface IUserIdGetter
{
    public UserAuthInfo GetUserAuthInfo();
}

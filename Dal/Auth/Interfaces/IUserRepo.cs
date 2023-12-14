using Backend.Dal.Auth.Models;
using Backend.Dal.Base.Interfaces;

namespace Backend.Dal.Auth.Interfaces;

public interface IUserRepo : IBaseRepo<UserModel>
{
    public Task<UserModel> GetUserByEmailAsync(string email);
}
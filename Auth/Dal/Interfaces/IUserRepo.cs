using Backend.Auth.Dal.Models;
using Backend.Base.Dal.Interfaces;

namespace Backend.Auth.Dal.Interfaces;

public interface IUserRepo : IBaseRepo<UserModel>
{
    public Task<UserModel> GetUserByEmailAsync(string email);
    public Task UpdatePasswordHash(int userId, int newHash);
}
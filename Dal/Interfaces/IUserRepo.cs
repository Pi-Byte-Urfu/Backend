using Backend.Dal.Models;

namespace Backend.Repositories.Interfaces;

public interface IUserRepo : IBaseRepo<UserModel>
{
    public Task<UserModel> GetUserByEmailAsync(string email);
}
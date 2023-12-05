using Backend.Models;

namespace Backend.Repositories.Interfaces;

public interface IUserRepo : IBaseRepo<UserModel>
{
    public Task<UserModel> GetUserByEmail(string email);
}
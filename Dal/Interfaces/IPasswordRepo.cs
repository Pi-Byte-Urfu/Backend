using Backend.Dal.Models;

namespace Backend.Repositories.Interfaces;

public interface IPasswordRepo : IBaseRepo<PasswordModel>
{
    public Task<PasswordModel> GetPasswordByHash(int hash);
}

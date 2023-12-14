using Backend.Dal.Auth.Models;
using Backend.Dal.Base.Interfaces;

namespace Backend.Dal.Auth.Interfaces;

public interface IPasswordRepo : IBaseRepo<PasswordModel>
{
    public Task<PasswordModel> GetPasswordByHash(int hash);
}

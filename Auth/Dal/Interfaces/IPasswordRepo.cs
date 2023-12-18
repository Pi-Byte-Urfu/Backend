using Backend.Auth.Dal.Models;
using Backend.Base.Dal.Interfaces;

namespace Backend.Auth.Dal.Interfaces;

public interface IPasswordRepo : IBaseRepo<PasswordModel>
{
    public Task<PasswordModel> GetPasswordByHash(int hash);
}

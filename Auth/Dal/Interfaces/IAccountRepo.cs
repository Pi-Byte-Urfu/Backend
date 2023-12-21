using Backend.Auth.Dal.Models;
using Backend.Base.Dal.Interfaces;

namespace Backend.Auth.Dal.Interfaces;

public interface IAccountRepo : IBaseRepo<AccountModel>
{
    public Task UpdatePhotoPath(int accountId, string photoPath);
}
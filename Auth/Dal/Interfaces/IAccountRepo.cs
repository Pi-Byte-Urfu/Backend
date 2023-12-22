using Backend.Auth.Dal.Models;
using Backend.Auth.Dto;
using Backend.Base.Dal.Interfaces;

namespace Backend.Auth.Dal.Interfaces;

public interface IAccountRepo : IBaseRepo<AccountModel>
{
    public Task UpdatePhotoPathAsync(int accountId, string photoPath);
    public Task<AccountModel> GetAccountByUserIdAsync(int userId);
    public Task<int> UpdateAccountByUserIdAsync(int userId, AccountUpdateDto accountUpdateDto);
}
using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Auth.Dto;
using Backend.Base.Dal;

using Microsoft.EntityFrameworkCore;

namespace Backend.Auth.Dal;

public class AccountRepo : BaseRepo<AccountModel>, IAccountRepo
{
    public AccountRepo(AppDatabase database) : base(database)
    {

    }

    public async override Task<int> CreateEntityAsync(AccountModel entity)
    {
        var allAccounts = await base.GetAllEntitiesAsync();
        var accountNumberWithThisUser = allAccounts.Where(account => account.UserId == entity.UserId).Count();
        if (accountNumberWithThisUser != 0)
            throw new BadHttpRequestException(message: "There are already account for this user", statusCode: 400);

        return await base.CreateEntityAsync(entity);
    }

    public async Task<AccountModel> GetAccountByUserIdAsync(int userId)
    {
        var account = await table.Where(account => account.UserId == userId).FirstOrDefaultAsync();
        return account;
    }

    public async Task<int> UpdateAccountByUserIdAsync(int userId, AccountUpdateDto accountUpdateDto)
    {
        var account = await table.Where(account => account.UserId == userId).FirstOrDefaultAsync();
        accountUpdateDto.UpdateEntity(account);

        await _database.SaveChangesAsync();
        return account.Id;
    }

    public async Task UpdatePhotoPathAsync(int accountId, string photoPath)
    {
        var account = await GetEntityByIdAsync(accountId);
        account.PhotoUrl = photoPath;
        await _database.SaveChangesAsync();
    }
}
using Backend.Dal.Auth.Interfaces;
using Backend.Dal.Auth.Models;
using Backend.Dal.Base;

namespace Backend.Dal.Auth;

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
}
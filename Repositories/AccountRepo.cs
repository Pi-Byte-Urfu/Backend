using Backend.Models;
using Backend.Repositories.Interfaces;

namespace Backend.Repositories;

public class AccountRepo : BaseRepo<AccountModel>, IAccountRepo
{
    public AccountRepo(AppDatabase database) : base(database)
    {

    }
}
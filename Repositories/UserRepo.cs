using Backend.Models;
using Backend.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class UserRepo : BaseRepo<UserModel>, IUserRepo
{
    public UserRepo(AppDatabase database) : base(database)
    {

    }

    public async Task<UserModel> GetUserByEmail(string email)
    {
        return await table.FirstOrDefaultAsync(user => user.Email == email);
    }
}
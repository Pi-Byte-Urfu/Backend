using Backend.Dal.Auth.Interfaces;
using Backend.Dal.Auth.Models;
using Backend.Dal.Base;
using Microsoft.EntityFrameworkCore;

namespace Backend.Dal.Auth;

public class UserRepo : BaseRepo<UserModel>, IUserRepo
{
    public UserRepo(AppDatabase database) : base(database)
    {

    }

    public async Task<UserModel> GetUserByEmailAsync(string email)
    {
        return await table.FirstOrDefaultAsync(user => user.Email == email);
    }
}
using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Base.Dal;
using Microsoft.EntityFrameworkCore;

namespace Backend.Auth.Dal;

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
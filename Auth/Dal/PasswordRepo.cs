using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Base.Dal;
using Microsoft.EntityFrameworkCore;

namespace Backend.Auth.Dal
{
    public class PasswordRepo : BaseRepo<PasswordModel>, IPasswordRepo
    {
        public PasswordRepo(AppDatabase database) : base(database)
        {
        }

        public async Task<PasswordModel> GetPasswordByHash(int hash)
        {
            return await table.FirstOrDefaultAsync(password => password.HashedPassword == hash);
        }
    }
}

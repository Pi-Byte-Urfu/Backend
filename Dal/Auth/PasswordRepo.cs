using Backend.Dal.Auth.Interfaces;
using Backend.Dal.Auth.Models;
using Backend.Dal.Base;
using Microsoft.EntityFrameworkCore;

namespace Backend.Dal.Auth
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

using Backend.Models;
using Backend.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
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

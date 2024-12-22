using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly HRSystemDbContext _context;

        public UserRepository(HRSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}

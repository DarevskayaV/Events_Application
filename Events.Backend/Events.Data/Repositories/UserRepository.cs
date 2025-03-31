using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Events.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly EventDbContext _context;

        public UserRepository(EventDbContext context) : base(context)
        {
            _context = context;
        }

        // получение пользователя по имени
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Username == username);
        }

    }
}


using Events.Core.Entity;
using Events.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Events.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly EventDbContext _context;

        public RefreshTokenRepository(EventDbContext context)
        {
            _context = context;
        }

        public async Task SaveRefreshTokenAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string username, string refreshToken)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Username == username && rt.Token == refreshToken && !rt.IsRevoked);
        }
    }
}


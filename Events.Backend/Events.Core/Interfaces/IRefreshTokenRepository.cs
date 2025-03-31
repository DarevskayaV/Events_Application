using Events.Core.Entity;

namespace Events.Core.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task SaveRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken> GetRefreshTokenAsync(string username, string refreshToken);
    }
}


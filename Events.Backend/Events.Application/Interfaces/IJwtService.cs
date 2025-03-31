using Events.Application.DTO.Request;
using System.Security.Claims;

namespace Events.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
        Task<(string newAccessToken, string newRefreshToken)> GetRefreshToken(TokenRequestDTO tokenRequest);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<bool> ValidateRefreshToken(string username, string refreshToken);
        Task SaveRefreshToken(string username, string refreshToken);
        string GenerateRefreshToken();
    }
}
using UserManagement.Core.Models;

namespace UserManagement.Application.Services.AuthService;

public interface ITokenService
{
    string CreateAccessToken(User user);
    Task<RefreshToken> CreateRefreshToken(User user);
    Task<User?> ValidateRefreshToken(Guid userId, string refreshToken);
}
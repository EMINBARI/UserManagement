using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;

namespace UserManagement.Application.Services.AuthService;

public class TokenService(
    IConfiguration configuration, 
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository): ITokenService
{
    public string CreateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username.First),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:SecureKey")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddHours(configuration.GetValue<int>("AppSettings:ExpiresInHours")),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public async Task<RefreshToken> CreateRefreshToken(User user)
    {
        var tokenValue = GenerateRefreshToken();
        var refreshTokenEntity = new RefreshToken(
            userId: user.Id,
            token: tokenValue,
            expiresAt: DateTimeOffset.UtcNow.AddDays(configuration.GetValue<int>("AppSettings:RefreshExpiresInDays"))
        );
        await refreshTokenRepository.AddAsync(refreshTokenEntity, CancellationToken.None);
        
        return refreshTokenEntity;
    }

    public async Task<User?> ValidateRefreshToken(Guid userId, string refreshToken)
    {
        var user = await userRepository.GetAsync(userId, CancellationToken.None);
        var refreshTokensList = await refreshTokenRepository
            .GetUserTokensAsync(userId, CancellationToken.None);

        var refreshTokenEntry = refreshTokensList
            .SingleOrDefault(t => t.Token == refreshToken);
            
        if (user is null || 
            refreshTokenEntry is null || 
            refreshTokenEntry.ExpiresAt <= DateTimeOffset.UtcNow)
        {
            return null;
        }
        
        return user;
    }
    
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
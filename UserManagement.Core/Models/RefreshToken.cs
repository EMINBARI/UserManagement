using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Models;

public class RefreshToken: IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
    public DateTimeOffset ExpiresAt { get; init; }
    public DateTimeOffset RevokedAt { get; init; }
    
    public User User { get; init; }

    public RefreshToken(Guid userId, string token, DateTimeOffset expiresAt)
    {
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
    }
}
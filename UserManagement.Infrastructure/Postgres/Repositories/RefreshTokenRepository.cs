using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;
using UserManagement.Infrastructure.Abstractions;

namespace UserManagement.Infrastructure.Postgres.Repositories;

public class RefreshTokenRepository: GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    private readonly DbContext _context;

    public RefreshTokenRepository(PostgresContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<RefreshToken>> GetUserTokensAsync(
        Guid userId, 
        CancellationToken cancellationToken)
    {
        return await _context.Set<RefreshToken>()
            .Where(u => u.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Result> RevokeAsync(
        Guid userId, 
        string refreshToken, 
        CancellationToken cancellationToken)
    {
        var refreshTokensList = await GetUserTokensAsync(userId, cancellationToken);

        var token = refreshTokensList.SingleOrDefault(t => t.Token == refreshToken);
        
        if (token == null)
            return Result.Failure("RefreshToken not found");
        
        token.RevokedAt = DateTimeOffset.UtcNow;
        token.IsRevoked = true;
        
        await UpdateAsync(token, cancellationToken);
        
        return Result.Success();
    }
}
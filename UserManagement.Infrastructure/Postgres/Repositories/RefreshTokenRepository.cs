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
}
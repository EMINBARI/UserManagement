using UserManagement.Core.Models;

namespace UserManagement.Core.Repositories;

public interface IRefreshTokenRepository: IGenericRepository<RefreshToken>
{
    public Task<IEnumerable<RefreshToken>> GetUserTokensAsync(Guid userId, CancellationToken cancellationToken); 
}
using UserManagement.Core.Models;
using CSharpFunctionalExtensions;

namespace UserManagement.Core.Repositories;

public interface IRefreshTokenRepository: IGenericRepository<RefreshToken>
{
    public Task<IEnumerable<RefreshToken>> GetUserTokensAsync(Guid userId, CancellationToken cancellationToken);
    public Task<Result> RevokeAsync(Guid userId, string refreshToken, CancellationToken cancellationToken);
}
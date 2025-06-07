using System.Linq.Expressions;
using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Repositories;

public interface IGenericRepository <TEntity> where TEntity : class, IEntity
{
    public Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    public Task<TEntity> GetAsync(Guid entityId, CancellationToken cancellationToken);
    public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
}
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserManagement.Core.Interfaces;
using UserManagement.Core.Repositories;

namespace UserManagement.Infrastructure.Abstractions;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity: class, IEntity
{
    private readonly DbContext _context;

    public GenericRepository(DbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(Guid entityId, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
    }

}
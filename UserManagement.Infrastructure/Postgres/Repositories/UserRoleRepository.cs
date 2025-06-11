using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;

namespace UserManagement.Infrastructure.Postgres.Repositories;

public class UserRoleRepository(PostgresContext context): IUserRoleRepository
{
    public async Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
    {
        var roles = await context.Set<UserRole>()
            .Include(ur => ur.User)
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role)
            .ToListAsync(cancellationToken);
        
        return roles;   
    }

    public async Task AddRoleUserAsync(UserRole userRole, CancellationToken cancellationToken)
    {   
        context.Set<UserRole>().Add(userRole);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRoleUserAsync(UserRole userRole, CancellationToken cancellationToken)
    {
        context.Set<UserRole>().Remove(userRole);
        await context.SaveChangesAsync(cancellationToken);
    }
}
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;

namespace UserManagement.Infrastructure.Postgres.Repositories;

public class UserRoleRepository(PostgresContext context): IUserRoleRepository
{
    public async Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId)
    {
        var roles = await context.Set<UserRole>()
            .Include(ur => ur.User)
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role)
            .ToListAsync();
        
        return roles;   
    }
}
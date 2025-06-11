using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;

namespace UserManagement.Infrastructure.Postgres.Repositories;

public class RolePermissionRepository(PostgresContext context): IRolePermissionRepository
{
    public async Task<IEnumerable<Permission>> GetAsync(int roleId)
    {
        var permissions = await context.Set<RolePermission>()
            .Include(rp => rp.Role)
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission)
            .ToListAsync();
        
        return permissions;
    }
}
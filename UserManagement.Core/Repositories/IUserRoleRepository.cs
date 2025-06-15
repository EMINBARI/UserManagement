using UserManagement.Core.Models;

namespace UserManagement.Core.Repositories;

public interface IUserRoleRepository
{
    public Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken);
    public Task AddRoleUserAsync(UserRole userRole, CancellationToken cancellationToken);
    public Task DeleteRoleUserAsync(UserRole userRole, CancellationToken cancellationToken);
}
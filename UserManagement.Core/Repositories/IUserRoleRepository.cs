using UserManagement.Core.Models;

namespace UserManagement.Core.Repositories;

public interface IUserRoleRepository
{
    public Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId);
}
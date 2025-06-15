using UserManagement.Core.Models;

namespace UserManagement.Core.Repositories;

public interface IRolePermissionRepository
{
    public Task<IEnumerable<Permission>> GetAsync(int roleId);
    public Task<IEnumerable<Permission>> GetAsync(List<int> roleIds);
}
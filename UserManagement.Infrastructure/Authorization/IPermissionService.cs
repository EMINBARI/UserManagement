using UserManagement.Infrastructure.Authorization.Enums;

namespace UserManagement.Infrastructure.Authorization;

public interface IPermissionService
{
    public Task<HashSet<PermissionCategory>> GetPermissionsAsync(Guid userId);
}
using UserManagement.Core.Repositories;
using UserManagement.Infrastructure.Authorization.Enums;

namespace UserManagement.Infrastructure.Authorization;

public class PermissionService : IPermissionService
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    
    public PermissionService(
        IUserRoleRepository userRoleRepository, 
        IRolePermissionRepository rolePermissionRepository)
    {
        _userRoleRepository = userRoleRepository;
        _rolePermissionRepository = rolePermissionRepository;
    }

    public async Task<HashSet<PermissionCategory>> GetPermissionsAsync(Guid userId)
    {
        var userRoles = await _userRoleRepository.GetUserRolesAsync(userId);
        var permissions = new HashSet<PermissionCategory>();
 
        var res = await _rolePermissionRepository.GetAsync(userRoles.Select(u => u.Id).ToList());
        permissions.UnionWith(res.Select(p => (PermissionCategory)p.Id).ToHashSet());
        
        return permissions;
    }
}
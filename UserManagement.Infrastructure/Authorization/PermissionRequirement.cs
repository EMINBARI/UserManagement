using Microsoft.AspNetCore.Authorization;
using UserManagement.Infrastructure.Authorization.Enums;

namespace UserManagement.Infrastructure.Authorization;

public class PermissionRequirement(PermissionCategory[] permissions) : IAuthorizationRequirement
{
    public PermissionCategory[] Permissions { get; set; } = permissions;
}
using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Models;

public class RolePermission
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
    
    public Role Role { get; set; }
    public Permission Permission { get; set; }
}
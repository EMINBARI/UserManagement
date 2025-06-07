using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Models;

public class RolePermissions: IEntity
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    
    public Role Role { get; set; }
    public Permission Permission { get; set; }
}
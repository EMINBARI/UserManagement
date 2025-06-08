using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Models;

public class UserPermission: IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PermissionId { get; set; }
    
    public User User { get; set; }
    public Permission Permission { get; set; }
}
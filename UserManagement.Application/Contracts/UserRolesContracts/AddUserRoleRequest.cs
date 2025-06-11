namespace UserManagement.Application.Contracts.UserRolesContracts;

public class AddUserRoleRequest
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
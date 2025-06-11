namespace UserManagement.Infrastructure.Authorization.Configuration;

public class RolePermissionsMapping
{
    public string Role { get; set; } = String.Empty;
    public string[] Rermissions { get; set; } = [];
}
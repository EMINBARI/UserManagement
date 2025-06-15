using UserManagement.Core.Models;

namespace UserManagement.Application.Contracts.UserRolesContracts;

public record UserRoleResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public UserRoleResponse(Role role)
    {
        Id = role.Id;
        Name = role.Name;
        Description = role.Description;
    }
}
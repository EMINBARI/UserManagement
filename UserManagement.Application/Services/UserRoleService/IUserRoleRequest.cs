using UserManagement.Application.Contracts.UserRolesContracts;
using UserManagement.Core.Models;

namespace UserManagement.Application.Services.UserRoleService;

public interface IUserRoleAssignmentService
{
    public Task AddUserRoleAsync(AddUserRoleRequest request);
    public Task<IEnumerable<UserRoleResponse>> GetUserRolesAsync(Guid userId);
    public Task DeleteUserRoleAsync(AddUserRoleRequest request);
}
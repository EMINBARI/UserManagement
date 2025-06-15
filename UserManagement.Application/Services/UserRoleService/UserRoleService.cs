using UserManagement.Application.Contracts.UserRolesContracts;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;

namespace UserManagement.Application.Services.UserRoleService;

public class UserRoleService(IUserRoleRepository userRoleRepository): IUserRoleAssignmentService
{
    public async Task AddUserRoleAsync(AddUserRoleRequest request)
    {
        var userRole = new UserRole(
            request.UserId, request.RoleId
            );
        
        await userRoleRepository.AddRoleUserAsync(userRole, CancellationToken.None);
    }

    public async Task<IEnumerable<UserRoleResponse>> GetUserRolesAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUserRoleAsync(AddUserRoleRequest request)
    {
        throw new NotImplementedException();
    }
}
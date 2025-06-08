using UserManagement.Application.Contracts.AuthContracts.Requests;
using UserManagement.Application.Contracts.UserContracts.Responses;

namespace UserManagement.Application.Services.UserService;

public interface IUserService
{
    public Task<UserResponse> GetUserAsync(Guid userId);
    public Task<IEnumerable<UserResponse>> GetUsersAsync();
    public Task DeleteUserAsync(Guid userId);
}
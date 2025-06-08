using UserManagement.Application.Contracts.UserContracts.Responses;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;
using UserManagement.Core.ValueObjects;
using UserManagement.Infrastructure;

namespace UserManagement.Application.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<UserResponse> GetUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserResponse>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}
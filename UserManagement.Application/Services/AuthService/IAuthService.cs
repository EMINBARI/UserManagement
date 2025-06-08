using UserManagement.Application.Contracts.AuthContracts.Requests;
using UserManagement.Application.Contracts.AuthContracts.Responses;


namespace UserManagement.Application.Services.AuthService;

public interface IAuthService
{
    public Task<RegisterResponse> RegisterAsync(RegisterUserRequest request);
    public Task<AuthResponse?> LoginAsync(LoginUserRequest request);
}
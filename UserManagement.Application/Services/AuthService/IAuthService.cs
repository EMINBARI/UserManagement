using CSharpFunctionalExtensions;
using UserManagement.Application.Contracts.AuthContracts.Requests;
using UserManagement.Application.Contracts.AuthContracts.Responses;


namespace UserManagement.Application.Services.AuthService;

public interface IAuthService
{
    public Task<RegisterResponse> RegisterAsync(RegisterUserRequest request);
    public Task<Result<AuthResponse>> LoginAsync(LoginUserRequest request);
    public Task<Result<AuthResponse>> RefreshAsync(RefreshTokensRequest request);
}
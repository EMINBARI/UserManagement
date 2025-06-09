using CSharpFunctionalExtensions;
using UserManagement.Application.Contracts.AuthContracts.Requests;
using UserManagement.Application.Contracts.AuthContracts.Responses;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;
using UserManagement.Core.ValueObjects;
using UserManagement.Infrastructure;

namespace UserManagement.Application.Services.AuthService;

public class AuthService(
    ITokenService tokenService,
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IPasswordHasher passwordHasher)
    : IAuthService
{
    public async Task<RegisterResponse> RegisterAsync(RegisterUserRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user is not null)
        {
            return new RegisterResponse
            {
                IsSuccessful = false,
                Message = "User already exists."
            };
        }
        
        var passwordHash = passwordHasher.Generate(request.Password);
        
        user = new User(
            username: new Username
            {
                First = request.FirstName, 
                Last = request.LastName
            },
            email: request.Email,
            passwordHash: passwordHash);

        await userRepository.AddAsync(user, CancellationToken.None);

        return new RegisterResponse
        {
            IsSuccessful = true,
            Message = "User created."
        };
    }
    
    public async Task<Result<AuthResponse>> LoginAsync(LoginUserRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);
        
        if(user == null || !passwordHasher.Verify(request.Password, user.PasswordHash))
            return Result.Failure<AuthResponse>("Invalid credentials.");

        return await CreateAuthResponse(user);
    }
    
    public async Task<Result<AuthResponse>> RefreshAsync(RefreshTokensRequest request)
    {
        var user = await tokenService.ValidateRefreshToken(request.UserId, request.RefreshToken);

        if (user == null) 
            return Result.Failure<AuthResponse>("Invalid refresh token.");
        
        var response = await refreshTokenRepository.RevokeAsync(
            userId: request.UserId,
            refreshToken: request.RefreshToken, 
            CancellationToken.None);
        
        if (response.IsFailure) 
            return Result.Failure<AuthResponse>(response.Error);
        
        return await CreateAuthResponse(user);
    }
    
    private async Task<AuthResponse> CreateAuthResponse(User user)
    {
        return new AuthResponse
        {
            AccessToken = tokenService.CreateAccessToken(user),
            RefreshToken = (await tokenService.CreateRefreshToken(user)).Token
        };
    }
}
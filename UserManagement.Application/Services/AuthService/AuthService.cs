using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Application.Contracts.AuthContracts.Requests;
using UserManagement.Application.Contracts.AuthContracts.Responses;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;
using UserManagement.Core.ValueObjects;
using UserManagement.Infrastructure;

namespace UserManagement.Application.Services.AuthService;

public class AuthService: IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordHasher _passwordHasher;
    

    public AuthService(
        IConfiguration configuration,
        IUserRepository userRepository, 
        IRefreshTokenRepository refreshTokenRepository,
        IPasswordHasher passwordHasher)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordHasher = passwordHasher;
        
    }
    
    public async Task<RegisterResponse> RegisterAsync(RegisterUserRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is not null)
        {
            return new RegisterResponse
            {
                IsSuccessful = false,
                Message = "User already exists."
            };
        }
        
        var passwordHash = _passwordHasher.Generate(request.Password);
        
        var username = new Username
        {
            First = request.FirstName, 
            Last = request.LastName
        };
        
        user = new User(
            username: username,
            email: request.Email,
            passwordHash: passwordHash);

        await _userRepository.AddAsync(user, CancellationToken.None);

        return new RegisterResponse
        {
            IsSuccessful = true,
            Message = "User created."
        };
    }
    
    
    public async Task<Result<AuthResponse>> LoginAsync(LoginUserRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user != null && _passwordHasher.Verify(
                providedPassword: request.Password, 
                hashedPassword: user.PasswordHash))
            
            return await CreateAuthResponse(user);

        return Result.Failure<AuthResponse>("Invalid credentials.");
    }
    
    public async Task<Result<AuthResponse>> RefreshAsync(RefreshTokensRequest request)
    {
        var user = await ValidateRefreshToken(request.UserId, request.RefreshToken);

        if (user == null) return Result.Failure<AuthResponse>("Invalid refresh token.");
        
        var response = await _refreshTokenRepository.RevokeAsync(
            userId: request.UserId,
            refreshToken: request.RefreshToken, 
            CancellationToken.None);
        
        if (response.IsFailure) return Result.Failure<AuthResponse>(response.Error);
        
        return await CreateAuthResponse(user);
    }
    
    
    //Impl
    private async Task<User?> ValidateRefreshToken(Guid userId, string refreshToken)
    {
        var user = await _userRepository.GetAsync(userId, CancellationToken.None);
        var refreshTokensList = await _refreshTokenRepository
            .GetUserTokensAsync(userId, CancellationToken.None);

        var refreshTokenEntry = refreshTokensList
            .SingleOrDefault(t => t.Token == refreshToken);
            
        if (user is null || 
            refreshTokenEntry is null ||
            refreshTokenEntry.IsRevoked ||
            refreshTokenEntry.ExpiresAt <= DateTimeOffset.UtcNow 
            )
        {
            return null;
        }
        
        return user;
    }
    
    
    private async Task<AuthResponse> CreateAuthResponse(User user)
    {
        return new AuthResponse
        {
            AccessToken = CreateAccessToken(user),
            RefreshToken = (await GenerateAndSaveRefreshTokenAsync(user)).Token
        };
    }

    
    //IMpl
    private async Task<RefreshToken> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var tokenValue = GenerateRefreshToken();
        var refreshTokenEntity = new RefreshToken(
            userId: user.Id,
            token: tokenValue,
            expiresAt: DateTimeOffset.UtcNow.AddDays(7)
        );
        await _refreshTokenRepository.AddAsync(refreshTokenEntity, CancellationToken.None);
        
        return refreshTokenEntity;
    }
    
    //Impl
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    //Implemented 
    private string CreateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username.First),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
            audience: _configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
    
    
}
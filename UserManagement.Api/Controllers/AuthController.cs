using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Contracts.AuthContracts.Requests;
using UserManagement.Application.Contracts.AuthContracts.Responses;
using UserManagement.Application.Services.AuthService;

namespace UserManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUserRequest request)
    {
        var response = await _authService.LoginAsync(request);

        if (response.IsFailure)
            return BadRequest(response.Error);
        
        return Ok(response.Value);
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterUserRequest request)
    {
        var response = await _authService.RegisterAsync(request);
        
        if (!response.IsSuccessful)
            return BadRequest(response.Message);
        
        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] RefreshTokensRequest request)
    {
        var response = await _authService.RefreshAsync(request);

        if (response.IsFailure)
            return BadRequest(response.Error);
        
        return Ok(response.Value);
    }
}
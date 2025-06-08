namespace UserManagement.Application.Contracts.AuthContracts.Responses;

public record AuthResponse
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
    
}
namespace UserManagement.Application.Contracts.AuthContracts.Requests;

public record RefreshTokensRequest
{ 
    public Guid UserId { get; init; }
    public string RefreshToken { get; init; }
}
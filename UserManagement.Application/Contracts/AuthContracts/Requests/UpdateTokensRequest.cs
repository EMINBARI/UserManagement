namespace UserManagement.Application.Contracts.AuthContracts.Requests;

public record UpdateTokensRequest
{ 
    public Guid UserId { get; init; }
    public string RefreshToken { get; init; }
}
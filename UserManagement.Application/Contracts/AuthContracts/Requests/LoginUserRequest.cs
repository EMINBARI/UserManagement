namespace UserManagement.Application.Contracts.AuthContracts.Requests;

public record LoginUserRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
}
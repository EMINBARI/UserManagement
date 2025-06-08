namespace UserManagement.Application.Contracts.AuthContracts.Responses;

public record RegisterResponse
{
    public string Message { get; set; }
    public bool IsSuccessful { get; set; }
}
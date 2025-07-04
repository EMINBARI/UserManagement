namespace UserManagement.Application.Contracts.AuthContracts.Requests;

public record RegisterUserRequest
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
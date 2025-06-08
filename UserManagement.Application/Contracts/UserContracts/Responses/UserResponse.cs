using UserManagement.Core.Models;

namespace UserManagement.Application.Contracts.UserContracts.Responses;

public record UserResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public UserResponse(User user)
    {
        Id = user.Id;
        FirstName = user.Username.First;
        LastName = user.Username.Last;
        Email = user.Email;
    }
}
namespace UserManagement.Application.Contracts.ActivityLogContracts;

public record AddActivityLogRequest
{
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public string IPAddress { get; set; }
}
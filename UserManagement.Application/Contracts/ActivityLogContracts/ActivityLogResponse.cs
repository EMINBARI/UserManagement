using UserManagement.Core.Models;

namespace UserManagement.Application.Contracts.ActivityLogContracts;

public record ActivityLogResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public string IPAddress { get; set; }

    public ActivityLogResponse(ActivityLog activityLog)
    {
        Id = activityLog.Id;
        UserId = activityLog.UserId;
        Description = activityLog.Description;
        IPAddress = activityLog.IPAddress;
    }
}
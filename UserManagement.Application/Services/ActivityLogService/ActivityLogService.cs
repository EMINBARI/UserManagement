using UserManagement.Application.Contracts.ActivityLogContracts;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;

namespace UserManagement.Application.Services.ActivityLogService;

public class ActivityLogService(IActivityLogRepository activityLogRepository) : IActivityLogService
{
    public async Task<ActivityLogResponse> AddLogAsync(AddActivityLogRequest request)
    {
        var activityLog = new ActivityLog(
            request.UserId,
            request.Description,
            request.IPAddress);
        
        await activityLogRepository.AddAsync(activityLog, CancellationToken.None);

        return new ActivityLogResponse(activityLog);
    }

    public async Task<ActivityLogResponse> GetLogAsync(Guid logId)
    {
        var activityLog = await activityLogRepository.GetAsync(logId, CancellationToken.None);

        if (activityLog == null)
            throw new Exception("Activity log not found");
        
        return new ActivityLogResponse(activityLog);
    }

    public async Task<IEnumerable<ActivityLogResponse>> GetAsync()
    {
        var projectTasks = await activityLogRepository.GetAsync(c => true, CancellationToken.None);
        return projectTasks.Select(a => new ActivityLogResponse(a));
    }

    public async Task DeleteTaskStateAsync(Guid logId)
    {
        var activityLog = await activityLogRepository.GetAsync(logId, CancellationToken.None);
        
        if (activityLog == null)
            throw new Exception("Activity log not found");
        
        await activityLogRepository.DeleteAsync(activityLog, CancellationToken.None);
    }
}
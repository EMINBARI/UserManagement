using UserManagement.Application.Contracts.ActivityLogContracts;

namespace UserManagement.Application.Services.ActivityLogService;

public interface IActivityLogService
{
    public Task<ActivityLogResponse> AddLogAsync(AddActivityLogRequest request);
    public Task<ActivityLogResponse> GetLogAsync(Guid logId);
    public Task<IEnumerable<ActivityLogResponse>> GetAsync();
    public Task DeleteTaskStateAsync(Guid logId);
}
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;
using UserManagement.Infrastructure.Abstractions;

namespace UserManagement.Infrastructure.Postgres.Repositories;

public class ActivityLogRepository: GenericRepository<ActivityLog>, IActivityLogRepository
{
    public ActivityLogRepository(PostgresContext context) : base(context)
    {
    }
}
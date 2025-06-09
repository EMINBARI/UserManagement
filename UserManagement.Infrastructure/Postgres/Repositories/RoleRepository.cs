using UserManagement.Core.Models;
using UserManagement.Core.Repositories;
using UserManagement.Infrastructure.Abstractions;

namespace UserManagement.Infrastructure.Postgres.Repositories;

public class RoleRepository: IRoleRepository
{
    public RoleRepository(PostgresContext context)
    {
        
    }
}
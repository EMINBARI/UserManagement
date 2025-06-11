using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UserManagement.Infrastructure.Authorization;
using UserManagement.Infrastructure.Authorization.Configuration;
using UserManagement.Infrastructure.Config;

namespace UserManagement.Infrastructure.Postgres
{
    public class PostgresContext(
        DbContextOptions<PostgresContext> options,
        IOptions<RolePermissionsOptions> authorizationOptions
        ) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("usermanagement");
            builder.ApplyConfigurationsFromAssembly(typeof(PostgresContext).Assembly);
            builder.ApplyConfiguration(new RolePermissionConfig(authorizationOptions.Value));
        }

    }
}

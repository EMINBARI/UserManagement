using Microsoft.EntityFrameworkCore;

namespace UserManagement.Infrastructure.Postgres
{
    public class PostgresContext(DbContextOptions<PostgresContext> options) 
        : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("usermanagement");
            builder.ApplyConfigurationsFromAssembly(typeof(PostgresContext).Assembly);
        }
    }
}
 
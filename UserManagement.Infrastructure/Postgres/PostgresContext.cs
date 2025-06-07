using Microsoft.EntityFrameworkCore;


namespace UserManagement.Infrastructure.Postgres
{
    public class PostgresContext: DbContext
    {
        public PostgresContext(DbContextOptions<PostgresContext> options): base(options) { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("usermanagement");
            builder.ApplyConfigurationsFromAssembly(typeof(PostgresContext).Assembly);

        }

    }
}

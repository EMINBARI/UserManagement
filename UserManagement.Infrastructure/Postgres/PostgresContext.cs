using Microsoft.EntityFrameworkCore;


namespace UserManagement.Infrastructure.Postgres
{
    internal class PostgresContext: DbContext
    {
        public PostgresContext(DbContextOptions<PostgresContext> options): base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresContext).Assembly);

        }

    }
}

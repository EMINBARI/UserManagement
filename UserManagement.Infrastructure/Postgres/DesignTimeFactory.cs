using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UserManagement.Infrastructure.Authorization.Configuration;

namespace UserManagement.Infrastructure.Postgres;

public class DesignTimeFactory: IDesignTimeDbContextFactory<PostgresContext>
{
    public PostgresContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        
        var builder = new DbContextOptionsBuilder<PostgresContext>();
        
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.Development.json", optional: true)
            .Build();
        
        var connectionString = configurationBuilder.GetConnectionString("PostgresDatabase");
        builder.UseNpgsql(connectionString);

        var authorizationOptions = new RolePermissionsOptions();
        configurationBuilder
            .GetSection(nameof(RolePermissionsOptions))
            .Bind(authorizationOptions);
        var optionsWrapper = Options.Create(authorizationOptions);
        
        return new PostgresContext(builder.Options, optionsWrapper);
    }
}
using UserManagement.Infrastructure.Authorization;
using UserManagement.Infrastructure.Authorization.Enums;

namespace UserManagement.Api.Extensions;

public static class ConfigureAuthorizationServiceExtension
{
    public static IServiceCollection ConfigureAuthorizationService(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Read", 
                policy => policy.AddRequirements(new PermissionRequirement(
                    [PermissionCategory.Read])));
            options.AddPolicy("Write",
                policy => policy.AddRequirements(new PermissionRequirement(
                    [PermissionCategory.Write])));
            options.AddPolicy("Update",
                policy => policy.AddRequirements(new PermissionRequirement(
                    [PermissionCategory.Update])));
            options.AddPolicy("Delete",
                policy => policy.AddRequirements(new PermissionRequirement(
                    [PermissionCategory.Delete])));
        });

        return services;

    }
}
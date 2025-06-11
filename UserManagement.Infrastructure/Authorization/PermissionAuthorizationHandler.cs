using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace UserManagement.Infrastructure.Authorization;

public class PermissionAuthorizationHandler(IServiceScopeFactory scopeFactory)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
         var userId = context.User.Claims.FirstOrDefault(
             c => c.Type == ClaimTypes.NameIdentifier)?.Value;

         if (userId is null || !Guid.TryParse(userId, out var id))
         {
             return;
         }
         
         using var scope = scopeFactory.CreateScope();
         
         var permissionService = scope.ServiceProvider
             .GetRequiredService<IPermissionService>();

         var permissions = await permissionService.GetPermissionsAsync(id);

         if (permissions.Intersect(requirement.Permissions).Any())
         {
             context.Succeed(requirement);
         }
    }
    
}
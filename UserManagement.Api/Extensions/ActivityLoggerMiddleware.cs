using System.Security.Claims;
using UserManagement.Application.Contracts.ActivityLogContracts;
using UserManagement.Application.Services.ActivityLogService;
using UserManagement.Core.Models;
using UserManagement.Core.Repositories;
using UserManagement.Infrastructure.Postgres.Repositories;

namespace UserManagement.Api.Extensions;

public class ActivityLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ActivityLoggerMiddleware> _logger;

    public ActivityLoggerMiddleware(
        RequestDelegate next, 
        ILogger<ActivityLoggerMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation(
            "[{ActivityLoggerMiddlewareName}] Request received: {RequestMethod} {RequestPath} {ConnectionRemoteIpAddress}",
            nameof(ActivityLoggerMiddleware), context.Request.Method, context.Request.Path,
            context.Connection.RemoteIpAddress);
        await _next(context);
        _logger.LogInformation("[{ActivityLoggerMiddlewareName}] Request handled: {ResponseStatusCode}",
            nameof(ActivityLoggerMiddleware), context.Response.StatusCode);

        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        var activityLogService = context.RequestServices.GetService<IActivityLogService>();

        if (context.User.Identity is { IsAuthenticated: true } && Guid.TryParse(userIdClaim?.Value, out var userId))
        {
            await activityLogService.AddLogAsync(
                new AddActivityLogRequest
                {
                    Description = $"{context.Request.Method} {context.Request.Path}",
                    UserId = userId,
                    IPAddress = context.Connection.RemoteIpAddress?.ToString() ?? "N/A"
                });
        }
    }
}
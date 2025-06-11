using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace UserManagement.Api.Extensions;

public static class ConfigureAuthentificationServiceExtension
{
    public static IServiceCollection ConfigureAuthentificationService(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["AppSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["AppSettings:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["AppSettings:SecureKey"]!)),
                    ValidateIssuerSigningKey = true
                };
            });

        return services;
    }
}
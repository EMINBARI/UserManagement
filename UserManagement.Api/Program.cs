using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using UserManagement.Application.Services.AuthService;
using UserManagement.Core.Repositories;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.Authorization;
using UserManagement.Infrastructure.Authorization.Enums;
using UserManagement.Infrastructure.Postgres;
using UserManagement.Infrastructure.Postgres.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PostgresContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDatabase")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:SecureKey"]!)),
            ValidateIssuerSigningKey = true
        };
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Read", 
        policy => policy.AddRequirements(new PermissionRequirement(
            [PermissionCategory.Read])));
    options.AddPolicy("Write",
        policy => policy.AddRequirements(new PermissionRequirement(
            [PermissionCategory.Write])));
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();

builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


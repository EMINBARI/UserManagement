using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Core.Enums;
using UserManagement.Core.Models;
using UserManagement.Infrastructure.Authorization;
using UserManagement.Infrastructure.Authorization.Configuration;

namespace UserManagement.Infrastructure.Config;

public class RolePermissionConfig: IEntityTypeConfiguration<RolePermission>
{
    private readonly RolePermissionsOptions _rolePermissionsOptions;
 
    public RolePermissionConfig(RolePermissionsOptions rolePermissionsOptions)
    {
        _rolePermissionsOptions = rolePermissionsOptions;
    }
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(rp => new { rp.PermissionId, rp.RoleId });
        
        builder.HasOne(rp => rp.Role)
            .WithMany()
            .HasForeignKey(rp => rp.RoleId);
        
        builder.HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);

        builder.HasData(ParseRolePermissions());
    }

    private RolePermission[] ParseRolePermissions()
    {
        return _rolePermissionsOptions.Mappings
            .SelectMany(rp => rp.Rermissions
                .Select(p => new RolePermission{
                    RoleId = (int)Enum.Parse<RoleType>(rp.Role),
                    PermissionId = (int)Enum.Parse<PermissionCategory>(p)
                })).ToArray();
    }
}
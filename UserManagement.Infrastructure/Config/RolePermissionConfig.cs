using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Core.Models;

namespace UserManagement.Infrastructure.Config;

public class RolePermissionConfig: IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(rp => new { rp.PermissionId, rp.RoleId });
        
        builder.HasOne(rp => rp.Role)
            .WithMany()
            .HasForeignKey(rp => rp.RoleId);
        
        builder.HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);
    }
}
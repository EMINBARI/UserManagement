using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Core.Models;

namespace UserManagement.Infrastructure.Config;

public class UserPermissionConfig: IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.HasKey(up => up.Id);
        
        builder.HasOne(up => up.User)
            .WithMany()
            .HasForeignKey(up => up.UserId);
        
        builder.HasOne(up => up.Permission)
            .WithMany()
            .HasForeignKey(up => up.PermissionId);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Core.Enums;
using UserManagement.Core.Models;

namespace UserManagement.Infrastructure.Config;

public class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasMaxLength(Permission.MAX_NAME_LENGTH)
            .IsRequired();
        
        builder.Property(p => p.Description)
            .HasMaxLength(Permission.MAX_DESCRIPTION_LENGTH);

        
        var permissions = Enum
            .GetValues<PermissionCategory>()
            .Select(p => new Permission
            {
                Id = (int)p,
                Name = p.ToString(),
                Description = $"Some {p.ToString()} description"
            });
        
        builder.HasData(permissions);
    }
}
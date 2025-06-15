using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Infrastructure.Authorization.Enums;

namespace UserManagement.Infrastructure.Config
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(Role.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(r => r.Description) 
                .HasMaxLength(Role.MAX_DESCRIPTION_LENGTH);

            var roles = Enum
                .GetValues<RoleType>()
                .Select(r => new Role
                {
                    Id = (int)r,
                    Name = r.ToString(),
                    Description = $"Some {r.ToString()} description"
                    
                });
            
            builder.HasData(roles);
        }
        
    }
}

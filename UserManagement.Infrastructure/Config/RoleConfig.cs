using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Models;

namespace UserManagement.Infrastructure.Config
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(Role.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(r => r.Description) 
                .HasMaxLength(Role.MAX_DESCRIPTION_LENGTH)
                .IsRequired();
        }
    }
}

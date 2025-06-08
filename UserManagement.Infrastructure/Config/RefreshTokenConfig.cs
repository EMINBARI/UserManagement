using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Core.Models;

namespace UserManagement.Infrastructure.Config;

public class RefreshTokenConfig: IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId);

        builder.Property(t=>t.Token)
            .IsRequired();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Core.Models;

namespace UserManagement.Infrastructure.Config
{
    public class ActivityLogConfig : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Description)
                .HasMaxLength(ActivityLog.MAX_DESCRIPTION_LENGTH)
                .IsRequired();
            
            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);

            builder.Property(a => a.IPAddress)
                .IsRequired();

        }
    }
}

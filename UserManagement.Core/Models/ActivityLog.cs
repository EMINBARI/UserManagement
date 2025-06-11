using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Models
{
    public class ActivityLog: IEntity
    {
        public const int MAX_DESCRIPTION_LENGTH = 256;

        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
        public string IPAddress { get; set; }

        public User User { get; set; }

        public ActivityLog() { }

        public ActivityLog(Guid userId, string description, string ipAddress)
        {
            UserId = userId;
            Description = description;
            IPAddress = ipAddress;
        }
        
    }
}

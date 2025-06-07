using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.Models
{
    public class ActivityLog
    {
        public const int MAX_DESCRIPTION_LENGTH = 256;

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
        public string IPAddress { get; set; }

        public User User { get; set; }

        public ActivityLog() { }
    }
}

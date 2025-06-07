using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.ValueObjects;

namespace UserManagement.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public Username Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; private set; }

        public User() { }

    }
}

using UserManagement.Core.Interfaces;
using UserManagement.Core.ValueObjects;

namespace UserManagement.Core.Models
{
    public class User: IEntity
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

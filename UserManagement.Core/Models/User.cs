using System.ComponentModel.DataAnnotations;
using UserManagement.Core.Interfaces;
using UserManagement.Core.ValueObjects;

namespace UserManagement.Core.Models
{
    public class User: IEntity
    {
        public Guid Id { get; set; }
        public Username Username { get; set; }
        public Email Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; }
        
        public User () {}
        public User(Username username, string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = Email.Create(email);
            PasswordHash = passwordHash;
            UpdatedAt = DateTimeOffset.UtcNow;
        }
        
    }
}

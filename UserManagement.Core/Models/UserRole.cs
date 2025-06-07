using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Models
{
    public class UserRole: IEntity
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }

        public UserRole() { }
    }
}

using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Models
{
    public class UserRole: IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int RoleId { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }

        public UserRole() { }

        public UserRole(Guid userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

    }
}

using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Models
{
    public class Role
    {
        public const int MAX_NAME_LENGTH = 36;
        public const int MAX_DESCRIPTION_LENGTH = 256;
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Role () { }
    }
}

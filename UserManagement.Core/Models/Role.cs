using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.Models
{
    public class Role
    {
        public const int MAX_NAME_LENGTH = 36;
        public const int MAX_DESCRIPTION_LENGTH = 256;


        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Role () { }
    }
}

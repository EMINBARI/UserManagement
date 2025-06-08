using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.ValueObjects
{
    public record Username
    {
        public string First { get; set; }
        public string Last { get; set; }
    }
}

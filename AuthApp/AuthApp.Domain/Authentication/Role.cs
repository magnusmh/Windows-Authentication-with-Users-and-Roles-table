using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApp.Domain.Authentication
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApp.Common
{
    public class UserAccount {

        public string UserName { get; set; }
        public List<ApplicationPermission> ApplicationPermissions { get; set; }



    }
}

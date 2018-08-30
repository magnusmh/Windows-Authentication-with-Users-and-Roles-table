using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace AuthApp.Contract {
    public interface IPermissionProvider {

        bool IsUserAuthorized(IPrincipal principal, string permission);
    }
}

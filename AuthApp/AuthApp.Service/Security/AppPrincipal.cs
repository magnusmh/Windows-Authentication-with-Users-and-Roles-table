using AuthApp.Contract;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace AuthApp.Security
{
    public class AppPrincipal : ClaimsPrincipal {

        private readonly IPermissionProvider _PermissionProvider;

        public AppPrincipal(IPermissionProvider permissionProvider, IIdentity ntIdentity) :
        base((ClaimsIdentity) ntIdentity)
        {
            _PermissionProvider = permissionProvider;
        }


        
        public override bool IsInRole(string role) {
            return _PermissionProvider.IsUserAuthorized(this, role);
        }


    }
}

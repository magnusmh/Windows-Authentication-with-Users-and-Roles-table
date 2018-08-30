using AuthApp.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace AuthApp.Security
{
    public class PermissionProvider : IPermissionProvider {
        private readonly IServerConfiguration _ServerConfiguration;
        private readonly IUserCache _UserCache;
        private readonly string _AdministratorPermission;

       
        public PermissionProvider(
            IServerConfiguration serverConfiguration
            , IUserCache userCache) {

            _ServerConfiguration = serverConfiguration;
            _UserCache = userCache;

            _AdministratorPermission = _ServerConfiguration.AdministratorPermissionName;
        }

        public bool IsUserAuthorized(IPrincipal principal, string permission) {
            if (string.IsNullOrWhiteSpace(permission)) return true;
            var user = _UserCache.GetUserFromPrincipal(principal);
            if (user == null) return false;
            //Has Permission
            if (user.ApplicationPermissions.Any(i => i.IsValid && i.Permission.Equals(permission, StringComparison.OrdinalIgnoreCase))) {
                return true;
            }
            //Is Administrator
            if (user.ApplicationPermissions.Any(i => i.IsValid && i.Permission.Equals(_AdministratorPermission, StringComparison.OrdinalIgnoreCase))) {
                return true;
            }
            return false;
        }
    }
}
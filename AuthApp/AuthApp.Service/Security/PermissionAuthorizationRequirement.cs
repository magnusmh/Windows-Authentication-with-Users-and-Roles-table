using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApp.Security
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement {
        public IEnumerable<string> RequiredPermissions { get; }

        public PermissionAuthorizationRequirement(IEnumerable<string> requiredPermissions) {
            RequiredPermissions = requiredPermissions;
        }
    }
}

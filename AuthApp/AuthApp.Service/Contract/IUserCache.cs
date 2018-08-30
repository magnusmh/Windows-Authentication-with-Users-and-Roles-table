using AuthApp.Common;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace AuthApp.Contract
{
    public interface IUserCache {

        /// <summary>
        /// Get UserAccount from IPrincipal (UserName).
        /// </summary>
        /// <param name="principal">IPrincipal which contains the Identity.Name of the UserLogin</param>
        /// <param name="doRefresh">Refresh the UserCache first, before the User will be searched</param>
        /// <returns></returns>
        UserAccount GetUserFromPrincipal(IPrincipal principal, bool doRefresh = false);

    }
}

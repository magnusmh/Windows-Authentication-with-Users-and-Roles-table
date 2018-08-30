using AuthApp.Common;
using AuthApp.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace AuthApp.Implementation {
    public class UserCache : IUserCache {

        private readonly object _Lock = new object();
        private readonly List<CacheItem<UserAccount>> _UserAccountCache;
        private readonly IServerConfiguration _ServerConfiguration;


        public UserCache(
            IServerConfiguration serverConfiguration) {

            _ServerConfiguration = serverConfiguration;

            _UserAccountCache = new List<CacheItem<UserAccount>>();
        }

        public UserAccount GetUserFromPrincipal(IPrincipal principal, bool doRefresh = false) {
            string userName = principal.Identity.Name;
            //Remove Domain from Domain\userName
            if (userName.Contains(@"\")) {
                userName = userName.Substring(userName.IndexOf(@"\", StringComparison.CurrentCultureIgnoreCase) + 1);
            }
            return GetUserAccountFromLoginName(userName, doRefresh);
        }




        private UserAccount GetUserAccountFromLoginName(string userName, bool doRefresh) {
            UserAccount user = null;
            if (doRefresh) {
                ReloadUserInCache(userName);
            }
            user = SearchUserAccountFromLoginName(userName);
            if (user == null) {
                ReloadUserInCache(userName);
                user = SearchUserAccountFromLoginName(userName);
            }
            return user;
        }

        private UserAccount SearchUserAccountFromLoginName(string loginName) {
            return _UserAccountCache.Where(i =>
                                        i.Item.UserName.Equals(loginName, StringComparison.CurrentCultureIgnoreCase)
                                        && i.LastUpdate.AddMinutes(_ServerConfiguration.UserCacheValidityTime) > (DateTime.UtcNow)
                                        ).Select(s => s.Item).FirstOrDefault();
        }

        private void ReloadUserInCache(string loginName) {
            lock (_Lock) {
                var userAccount = GetUser(loginName);
                if (userAccount == null) {
                    return;
                }
                if (_UserAccountCache.Any(i => i.Item.UserName.Equals(userAccount.UserName))) {
                    _UserAccountCache.First(i => i.Item.UserName.Equals(userAccount.UserName)).Item = userAccount;
                    _UserAccountCache.First(i => i.Item.UserName.Equals(userAccount.UserName)).LastUpdate = DateTime.UtcNow;
                }
                else {
                    var cacheItem = new CacheItem<UserAccount> {
                        Item = userAccount,
                        LastUpdate = DateTime.UtcNow
                    };
                    _UserAccountCache.Add(cacheItem);
                }
            }
        }

        private UserAccount GetUser(string loginName) {
            var userAccountList = new List<UserAccount>();
            var userAccount = new UserAccount {
                UserName = "administrator",//Replace with Windows User name !
                ApplicationPermissions = new List<ApplicationPermission>()
            }; 

            userAccount.ApplicationPermissions.Add(new ApplicationPermission { IsValid = true, Permission = "Administrator" });
            userAccount.ApplicationPermissions.Add(new ApplicationPermission { IsValid = true, Permission = "User" });
            //userAccount.ApplicationPermissions.Add(new ApplicationPermission { IsValid = true, Permission = "TestTable" });

            userAccountList.Add(userAccount);

            var userAccount2 = new UserAccount {
                UserName = "magnus",//Replace with Windows User name !
                ApplicationPermissions = new List<ApplicationPermission>()

            };
            userAccount2.ApplicationPermissions.Add(new ApplicationPermission { IsValid = true, Permission = "User" });
            userAccount2.ApplicationPermissions.Add(new ApplicationPermission { IsValid = true, Permission = "TestTable" });

            userAccountList.Add(userAccount2);

            return userAccountList.FirstOrDefault(f=>f.UserName.Equals(loginName, StringComparison.OrdinalIgnoreCase));
        }
    }
}

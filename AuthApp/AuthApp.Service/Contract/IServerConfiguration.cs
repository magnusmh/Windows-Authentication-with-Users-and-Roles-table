using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApp.Contract {
    public interface IServerConfiguration {

        string AdministratorPermissionName { get; }
        int UserCacheValidityTime { get; }
    }
}


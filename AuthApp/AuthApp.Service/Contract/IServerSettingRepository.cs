using AuthApp.Common;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace AuthApp.Contract {
    public interface IServerSettingRepository {

        IEnumerable<ServerSetting> GetServerSettings();

    }
}

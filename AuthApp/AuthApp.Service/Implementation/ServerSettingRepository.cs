using AuthApp.Common;
using AuthApp.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApp.Implementation {
    public class ServerSettingRepository : IServerSettingRepository {


        public IEnumerable<ServerSetting> GetServerSettings() {
            var settings = new List<ServerSetting>();

            settings.Add(new ServerSetting { SettingKey = "UserCacheValidityTime", SettingValue = "10" });
            settings.Add(new ServerSetting { SettingKey = "AdministratorPermissionName", SettingValue = "Administrator" });

            return settings;
        }
    }
}
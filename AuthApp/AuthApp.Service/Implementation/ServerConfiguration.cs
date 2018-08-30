using AuthApp.Common;
using AuthApp.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthApp.Implementation {
    public class ServerConfiguration : IServerConfiguration {
        private const int _ServerSettingCacheTime = 10;

    private readonly IServerSettingRepository _ServerSettingRepository;
    private readonly List<ServerSetting> _ServerSettings;
    private object _Lock = new object();
    private DateTime _LastRefreshTime;

 
    public ServerConfiguration(IServerSettingRepository serverSettingRepository) {
        this._ServerSettingRepository = serverSettingRepository;
        _ServerSettings = new List<ServerSetting>();

        ReloadServerSettings();
    }

    private void ReloadServerSettings() {
        lock (_Lock) {
            if (_LastRefreshTime.AddMinutes(_ServerSettingCacheTime) < DateTime.UtcNow) {
                foreach (var item in _ServerSettingRepository.GetServerSettings()) {
                    if (_ServerSettings.Any(i => i.SettingKey.Equals(item.SettingKey))) {
                        _ServerSettings.First(i => i.SettingKey.Equals(item.SettingKey)).SettingValue = item.SettingValue;
                    }
                    else {
                        _ServerSettings.Add(item);
                    }
                }
                _LastRefreshTime = DateTime.UtcNow;
            }
        }
    }
    private string GetValue(string key) {
        return _ServerSettings.FirstOrDefault(i => i.SettingKey.Equals(key))?.SettingValue;
    }


    //Config Section

    public string AdministratorPermissionName {
        get {
            return GetValue(nameof(AdministratorPermissionName));
        }
    }

    public int UserCacheValidityTime {
        get {
            var timeAsString = GetValue(nameof(UserCacheValidityTime));
            int timeAsInt;
            if (Int32.TryParse(timeAsString, out timeAsInt) == false) {
                timeAsInt = 10;
            }
            return timeAsInt;
        }
    }
}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndoorMap.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IndoorMap.Helpers
{
    public class AppSettings :INotifyPropertyChanged
    {
        #region NotifyPropertyChangedEvent

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion

        //AppSettings
        public const string LocationSettingKey = "LocationSettingKey";
        public const string NetworkSettingKey = "NetworkSettingKey";
        public const string SelectedCityIdKey = "SelectedCityIdKey";
        public const string LocationCityKey = "LocationCityKey";

        private static object _locker = new object();

        public AppSettings()
        {

        }

        private static AppSettings instance;
         
        public static AppSettings Intance
        {
            get
            {
                if (instance == null)
                {
                    if (_locker != null)
                    {
                        instance = new AppSettings();
                    }
                }
                return instance;
            }
        }

        public bool _LocationSetting;
        public bool LocationSetting
        {
            get
            {
                _LocationSetting = StorageHelper.GetSettingsValueWithKey<bool>(LocationSettingKey);
                return _LocationSetting;
            }
            set
            {
                StorageHelper.SetSettingsValueAndKey(value, LocationSettingKey);
                _LocationSetting = value;
                NotifyPropertyChanged("LocationSetting");
            }
        }

        public bool NetworkSetting
        {
            get
            {
                return StorageHelper.GetSettingsValueWithKey<bool>(NetworkSettingKey);
            }
            set
            {
                StorageHelper.SetSettingsValueAndKey(value, NetworkSettingKey);
                NotifyPropertyChanged("NetworkSetting");
            }
        }

        public string SelectedCityId
        {
            get
            {
                return StorageHelper.GetSettingsValueWithKey<string>(SelectedCityIdKey);
            }
            set
            {
                StorageHelper.SetSettingsValueAndKey(value, SelectedCityIdKey);
                NotifyPropertyChanged("SelectedCityId");
            }
        }

        public string LocationCity
        {
            get
            {
                return StorageHelper.GetSettingsValueWithKey<string>(LocationCityKey);
            }
            set
            {
                StorageHelper.SetSettingsValueAndKey(value, LocationCityKey);
                NotifyPropertyChanged("LocationCityKey");
            }
        }

    }
}

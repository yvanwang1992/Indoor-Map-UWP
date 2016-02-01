using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndoorMap.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IndoorMap.Helpers
{  
    public class AppSettings
    { 
        //AppSaveSetting
        public const string SelectedCityIdKey = "SelectedCityIdKey";
        public const string LocationCityKey = "LocationCityKey";
        public const string IsFirstRunKey = @"IsFirstRunKey";

        //AppSettingPage
        public const string LocationSettingKey = "LocationSettingKey";
        public const string NetworkSettingKey = "NetworkSettingKey";
        public const string LandmarksVisibleSettingKey = "LandmarksVisibleSettingKey";

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

        public void SetAppSetting(string AppSettingKey ,bool AppSettingValue)
        {
            StorageHelper.SetSettingsValueAndKey(AppSettingValue, AppSettingKey);
        }

        public bool GetAppSetting(string AppSettingKey)
        {
            return StorageHelper.GetSettingsValueWithKey<bool>(AppSettingKey);
        }


        #region 设置列表  已删除
        //private ObservableCollection<SettingModel> settingList;
        //public ObservableCollection<SettingModel> SettingList
        //{
        //    get
        //    {
        //        if (settingList == null)
        //        {
        //            settingList = new ObservableCollection<SettingModel>()
        //            {
        //                new SettingModel(true, LocationSettingKey) { DisplayName = "是否开启定位"},
        //                new SettingModel(false, NetworkSettingKey) { DisplayName = "仅WIFI下可用"},
        //                new SettingModel(false, LandMarkSettingKey) { DisplayName = "是否开启3D地图"}
        //            };
        //        }
        //        return settingList;
        //    }
        //    set { settingList = value; }
        //}

        #endregion
        
        //选中的城市
        public string SelectedCityId
        {
            get
            {
                return StorageHelper.GetSettingsValueWithKey<string>(SelectedCityIdKey);
            }
            set
            {
                 StorageHelper.SetSettingsValueAndKey(value, SelectedCityIdKey);
            }
        }

        //定位城市
        public string LocationCity
        {
            get { return StorageHelper.GetSettingsValueWithKey<string>(LocationCityKey); }
            set
            {
                StorageHelper.SetSettingsValueAndKey(value, LocationCityKey);
             }
        }

        //第一次运行
        public bool IsFirstRun
        {
            get{ return StorageHelper.GetSettingsValueWithKey<bool>(IsFirstRunKey, true); }
            set
            {
                StorageHelper.SetSettingsValueAndKey(value, IsFirstRunKey);
            }
        }
     }

    
    public class SettingModel:INotifyPropertyChanged
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
        
        public SettingModel(bool defaultToggleValue, string settingKey)
        {
            DefaultToggleValue = defaultToggleValue;
            SettingKey = settingKey;
        }

        public static bool DefaultToggleValue { get; set; }

        public string SettingKey { get; set; }
        
        private bool toggleValue = DefaultToggleValue;
        public bool ToggleValue
        { 
            get
            {
                toggleValue= StorageHelper.GetSettingsValueWithKey<bool>(SettingKey, DefaultToggleValue);
                return toggleValue;
            }
            set
            {
                toggleValue = value;
                NotifyPropertyChanged("ToggleValue");
                StorageHelper.SetSettingsValueAndKey(value, SettingKey);
            }
        }

        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                NotifyPropertyChanged("DisplayName");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace IndoorMap.Helpers
{
    public class StorageHelper
    {
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public static void SetSettingsValueAndKey(object value ,string key)
        {
            if (!localSettings.Values.ContainsKey(key))
            {
                localSettings.Values.Add(key, value);
            }
            localSettings.Values[key] = value;
        }

        public static T GetSettingsValueWithKey<T>(string key)
        {
            if (!localSettings.Values.ContainsKey(key))
            {
                return default(T);
            }
            return (T)localSettings.Values[key];
        }
    }
}

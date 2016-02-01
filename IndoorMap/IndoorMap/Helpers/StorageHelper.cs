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
        #region Setting  Can Save bool、 string 

        #endregion
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        static StorageFolder folder = ApplicationData.Current.LocalFolder;

        public static void DeleteAllTheKeyAndValue()
        {
            localSettings.Values.Clear();
        }

        public static void SetSettingsValueAndKey(object value ,string key)
        {
            if (!localSettings.Values.ContainsKey(key))
            {
                localSettings.Values.Add(key, value);
            }
            localSettings.Values[key] = value;
        }

        public static T GetSettingsValueWithKey<T>(string key, Object defaultValue = null)
        {
            if (localSettings.Values.ContainsKey(key))
            {
                return (T)localSettings.Values[key];
            }
            if(null != defaultValue)
            {
                return (T)defaultValue;
            }
            return default(T);
        }

        public async static void GetFileUsingUrl()
        {
            Uri uri = new Uri("ms-appx:///VenueAtlasPage.html");
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
        }

        public async static Task SetLocalTextFile(string fileName, string content)
        {
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, content);
        }

        public async static Task<String> GetLocalTextFile(string fileName)
        {
            try
            {
                var file = await folder.GetFileAsync(fileName);
                string text = await FileIO.ReadTextAsync(file);
                return text;
            }
            catch
            {
                return null;
            }
        }
    }
}

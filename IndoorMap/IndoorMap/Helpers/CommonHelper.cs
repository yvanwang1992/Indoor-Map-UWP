using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace IndoorMap.Helpers
{
    public class CommonHelper
    {
        public const string SettingLocation = "ms-settings:privacy-location";

        #region Go To System Settings   

        public static async void NavigationToSettingUri(string settingUrl)
        {
           bool result = await Launcher.LaunchUriAsync(new Uri(settingUrl));
            if (!result)
            {
                //error
            }
        }
        
        #endregion
    }
}

using MVVMSidekick.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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


         
        #region GetHostPage

        public static Frame HostPage
        {
            get
            {
                Frame frame = Window.Current.Content as Frame;
                if (frame != null)
                {
                    return frame;
                }
                return null;
            }
        }

        #endregion

    }
}

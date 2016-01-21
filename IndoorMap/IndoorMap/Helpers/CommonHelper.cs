﻿using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.System;
using Windows.UI.Popups;
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
                //MVVMPage page = frame.Content as MVVMPage;
                if (frame != null)
                {
                    return frame;
                }
                return null;
            }
        }
         
        #endregion

        public static async Task<bool> ShowMessageDialog(string content, string title = "")
        {
            bool result = false;
            MessageDialog message = new MessageDialog("是否开启定位");
            message.Commands.Add(new UICommand()
            {
                Label = "确定",
                Invoked = (n) =>
                {
                    result = true;
                }
            });
            message.Commands.Add(new UICommand()
            {
                Label = "取消",
                Invoked = (n) =>
                {
                    result = false;
                }
            });
            await message.ShowAsync();
            return result;
        }
         
    }


}

using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Store;
using Windows.Devices.Geolocation;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IndoorMap.Helpers
{
    public class CommonHelper
    {

        public const string SettingLocationUri = "ms-settings:privacy-location";
        public const string AppReviewUri = "ms-windows-store://review/?ProductId=";

        #region Go To System Settings   

        public static async void NavigationToSettingUri()
        {
           bool result = await Launcher.LaunchUriAsync(new Uri(SettingLocationUri));
            if (!result)
            {
                //error
            }
        }

        public async static void Feedback(string subject = "主题", string content = "内容", string to = "RecipientEmail")
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.Subject = subject;
            emailMessage.Body = content;
            emailMessage.To.Add(new EmailRecipient(to));
            await EmailManager.ShowComposeNewEmailAsync(emailMessage); 
        }

        public async static void Review()
        { 
             bool result = await Launcher.LaunchUriAsync(new Uri(AppReviewUri + CurrentApp.AppId));
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

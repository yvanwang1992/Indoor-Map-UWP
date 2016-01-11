using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IndoorMap.Controller
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WaitingPanel : Page
    {
        public WaitingPanel()
        {
            this.InitializeComponent();
            this.Height = Configmanager.ScreenHeight;
            this.Width = Configmanager.ScreenWidth;
        }

        internal Popup WaitingPopup
        {
            get;
            private set;
        }

        public bool isOpen
        {
            get
            {
                return WaitingPopup != null && WaitingPopup.IsOpen;
            }
        }

        public async void Hide()
        {
            if(null != this.WaitingPopup)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    progressRing.IsActive = false;
                });
                this.WaitingPopup.IsOpen = false;
            }
        }

        public async void Show()
        {
            if(this.WaitingPopup == null)
            {
                this.WaitingPopup = new Popup()
                {
                    Width = Configmanager.ScreenWidth,
                    Height = Configmanager.ScreenHeight
                };
                this.WaitingPopup.Child = this;
            }
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => 
            {
                progressRing.IsActive = true;
            });
            this.WaitingPopup.IsOpen = true;
        }
    }
}

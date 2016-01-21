
using IndoorMap.ViewModels;
using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
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
using Windows.Phone.UI.Input;
using IndoorMap.Models;
using Newtonsoft.Json;
using Windows.UI.Popups;
using IndoorMap.Controller;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IndoorMap
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : MVVMPage
    {
        public SettingPage()
        {
           
            this.InitializeComponent();
            this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
            {
                StrongTypeViewModel = this.ViewModel as SettingPage_Model;
            });
            this.NavigationCacheMode = NavigationCacheMode.Required;

            StrongTypeViewModel = this.ViewModel as SettingPage_Model;


        }
        
        public SettingPage_Model StrongTypeViewModel
        {
            get { return (SettingPage_Model)GetValue(StrongTypeViewModelProperty); }
            set { SetValue(StrongTypeViewModelProperty, value); }
        }

        public static readonly DependencyProperty StrongTypeViewModelProperty =
                    DependencyProperty.Register("StrongTypeViewModel", typeof(SettingPage_Model), typeof(SettingPage), new PropertyMetadata(null));

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            SystemNavigationManager.GetForCurrentView().BackRequested -= App_BackRequested;
        }

        private async void webView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            await new MessageDialog(e.Value).ShowAsync();
        }

        private async void btnInvoker_Click(object sender, RoutedEventArgs e)
        {
            var floors = StrongTypeViewModel.building.floors;
            List<Object> obj = new List<Object>() { };
            foreach (var floor in floors)
            {
                if (!string.IsNullOrEmpty(floor.floorB3))
                {
                    obj.Add(new FloorB3Class() { FloorB3 = floor.floorB3 });
                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floorB2))
                {
                    obj.Add(new FloorB2Class() { FloorB2 = floor.floorB2 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floorB1))
                {
                    obj.Add(new FloorB1Class() { FloorB1 = floor.floorB1 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor1))
                {
                    obj.Add(new Floor1Class() { Floor1 = floor.floor1 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor2))
                {
                    obj.Add(new Floor2Class() { Floor2 = floor.floor2 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor3))
                {
                    obj.Add(new Floor3Class() { Floor3 = floor.floor3 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor4))
                {
                    obj.Add(new Floor4Class() { Floor4 = floor.floor4 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor5))
                {
                    obj.Add(new Floor5Class() { Floor5 = floor.floor5 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor6))
                {
                    obj.Add(new Floor6Class() { Floor6 = floor.floor6 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor7))
                {
                    obj.Add(new Floor7Class() { Floor7 = floor.floor7 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor8))
                {
                    obj.Add(new Floor8Class() { Floor8 = floor.floor8 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor9))
                {
                    obj.Add(new Floor9Class() { Floor9 = floor.floor9 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor10))
                {
                    obj.Add(new Floor10Class() { Floor10 = floor.floor10 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor11))
                {
                    obj.Add(new Floor11Class() { Floor11 = floor.floor11 });

                    continue;
                }
                if (!string.IsNullOrEmpty(floor.floor12))
                {
                    obj.Add(new Floor12Class() { Floor12 = floor.floor12 });

                    continue;
                }
            }

            //object[] obj = new object[] {
            //    new FloorB2Class{ FloorB2 = "539098c7b8604a3d36934658"},
            //    new FloorB1Class { FloorB1 = "539098c7b8604a3d3693463d"},
            //    new Floor1Class { Floor1 = "539098c7b8604a3d369345ed"} };

            string param = JsonConvert.SerializeObject(obj); 
            await webView.InvokeScriptAsync("StartInit", new string[] { param });
        }

        private void btnSearchShop_Click(object sender, RoutedEventArgs e)
        {
            string url = string.Format("http://ap.atlasyun.com/poi/shop/search?kw={0}&bid={1}",0, StrongTypeViewModel.building.id);
            FormAction action = new FormAction(url);
            action.isShowWaitingPanel = true;
            action.Run();
            action.FormActionCompleted += (ss, ee) => 
            {

            };
        }


        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if(StrongTypeViewModel.StageManager.DefaultStage.CanGoBack)
            {    StrongTypeViewModel.StageManager.DefaultStage.Frame.GoBack();
                e.Handled = true;
            }
        }
    }

    

}


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
using IndoorMap.Helpers;
using System.Collections.ObjectModel;

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
            StrongTypeViewModel = this.ViewModel as SettingPage_Model;
            App.Current.Resuming += Current_Resuming;
        }

        private void Current_Resuming(object sender, object e)
        {
            throw new NotImplementedException();
        }

        public SettingPage_Model StrongTypeViewModel
        {
            get { return (SettingPage_Model)GetValue(StrongTypeViewModelProperty); }
            set { SetValue(StrongTypeViewModelProperty, value); }
        }

        public static readonly DependencyProperty StrongTypeViewModelProperty =
                    DependencyProperty.Register("StrongTypeViewModel", typeof(SettingPage_Model), typeof(SettingPage), new PropertyMetadata(null));

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //if (AppSettings.Intance.GetAppSetting(AppSettings.LocationSettingKey))
            //{
            //    bool canLocate = await LocationManager.IsLocationCanAccess();
            //    if (canLocate)
            //        txtLocationStatus.Text = "地图可以使用你的位置";
            //    else
            //        txtLocationStatus.Text = "地图无法使用你的位置";
            //}
            //else
            //{
            //    txtLocationStatus.Text = "未开启定位";
            //}
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        { 
            base.OnNavigatedFrom(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CommonHelper.NavigationToSettingUri();
        } 
    }
}

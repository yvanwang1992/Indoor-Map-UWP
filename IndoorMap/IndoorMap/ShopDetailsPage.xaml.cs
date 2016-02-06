
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
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IndoorMap
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShopDetailsPage : MVVMPage
    { 
        public ShopDetailsPage()
        {
            this.InitializeComponent();
            this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
            {
                StrongTypeViewModel = this.ViewModel as ShopDetailsPage_Model;
            });
            StrongTypeViewModel = this.ViewModel as ShopDetailsPage_Model;
            //this.NavigationCacheMode = NavigationCacheMode.Enabled;

            //SystemNavigationManager.GetForCurrentView().BackRequested += ShopDetailsPage_BackRequested;

            //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = StrongTypeViewModel.StageManager.DefaultStage.CanGoBack ?
            //    AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
        
        public ShopDetailsPage_Model StrongTypeViewModel
        {
            get { return (ShopDetailsPage_Model)GetValue(StrongTypeViewModelProperty); }
            set { SetValue(StrongTypeViewModelProperty, value); }
        }

        public static readonly DependencyProperty StrongTypeViewModelProperty =
                    DependencyProperty.Register("StrongTypeViewModel", typeof(ShopDetailsPage_Model), typeof(ShopDetailsPage), new PropertyMetadata(null));
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtTest.Text = "点击后的数据";
        }
        //private void ShopDetailsPage_BackRequested(object sender, BackRequestedEventArgs e)
        //{
        //    StrongTypeViewModel.StageManager.DefaultStage.Frame.GoBack();
        //}
    }
}

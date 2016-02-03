using MVVMSidekick.Views;
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
using IndoorMap.ViewModels;
using IndoorMap.Helpers;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using IndoorMap.Controller;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.Graphics.Display;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IndoorMap
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : MVVMPage
    {
        
        public MainPage()
        {
            this.InitializeComponent();
            this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
            {
                StrongTypeViewModel = this.ViewModel as MainPage_Model;
            });
            StrongTypeViewModel = this.ViewModel as MainPage_Model;
            this.NavigationCacheMode = NavigationCacheMode.Required;

            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
            this.SizeChanged += MainPage_SizeChanged;


            
        }


        public MainPage_Model StrongTypeViewModel
        {
            get { return (MainPage_Model)GetValue(StrongTypeViewModelProperty); }
            set { SetValue(StrongTypeViewModelProperty, value); }
        }

        public static readonly DependencyProperty StrongTypeViewModelProperty =
                    DependencyProperty.Register("StrongTypeViewModel", typeof(MainPage_Model), typeof(MainPage), new PropertyMetadata(null));
         
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //StatusBar
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundColor = Configmanager.ThemeColor;
                statusBar.BackgroundOpacity = 1;
                statusBar.ForegroundColor = Colors.White;
                await statusBar.ShowAsync();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void frameAtals_Navigated(object sender, NavigationEventArgs e)
        { 
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                this.frameAtals.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
         }


        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (frameAtals.CanGoBack)
            {
                if (this.ActualWidth <= 500 && frameAtals.BackStackDepth == 1)
                {
                    StrongTypeViewModel.MainVisibility = Visibility.Visible;
                }
                frameAtals.GoBack();
                e.Handled = true;
            }
        }


        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {//窗口大小
            double width = ApplicationView.GetForCurrentView().VisibleBounds.Width;
            double height = ApplicationView.GetForCurrentView().VisibleBounds.Height;
            double rawPixel = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;


            if (this.ActualWidth <= 500)
            {
                if (frameAtals.CanGoBack)
                {
                    StrongTypeViewModel.MainVisibility = Visibility.Collapsed;
                }
                else
                {
                    StrongTypeViewModel.MainVisibility = Visibility.Visible;
                }
                this.frameSplitContent.Height = height - lvHorizontal.ActualHeight - BottomAppBar.ActualHeight;
                this.rpMain.Width = width;
            }
            else
            {
                this.frameSplitContent.Height = height;

                this.rpMain.Width = width;
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            CommonHelper.Feedback();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
             autoSuggestBox.Focus(FocusState.Keyboard);
        }
    }
}

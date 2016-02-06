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

        bool backFlag = false;  //是否退出判断的标识

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
            else
            {
                if (!backFlag)
                {
                    CustomExitToast toast = new CustomExitToast("再按一次退出程序");
                    toast.seconds = 2;
                    toast.Show();
                    backFlag = true;
                    toast.ToastHiddenCompleted += (ss, ee) =>
                    {
                        backFlag = false;
                    };
                    e.Handled = true;
                }
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
                this.frameSplitContent.Height = height - rpTop.ActualHeight - BottomAppBar.ActualHeight;
                this.rpMain.Width = width;  
            }
            else
            {

                this.frameSplitContent.Height = height - rpTop.ActualHeight;

                //手机上 要变小   并且是横屏 
                if ((DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.LandscapeFlipped ||
                    DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.Landscape)
                    && Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                {
                    this.rpMain.Width = width * 2 / 5  ;
                }
                else
                {
                    this.rpMain.Width = width;
                } 
            }
        }
         
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
             autoSuggestBox.Focus(FocusState.Keyboard);
        }

        private void autoSuggestBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(this.ActualWidth <= 500)
            {
                SeachBoxHideStoryboard.Begin();
            }
        }

        private void btnHamburg_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private void appbarBtnReview_Click(object sender, RoutedEventArgs e)
        {
            CommonHelper.Review();
        }

        private void appbarBtnFeedback_Click(object sender, RoutedEventArgs e)
        {
            CommonHelper.Feedback("商场室内地图-反馈", "请在此输入内容,谢谢！", "yffswyf@163.com");
        }

        private void appbarBtnAbout_Click(object sender, RoutedEventArgs e)
        {
            StrongTypeViewModel.GoToAbout();
        }
    }
}

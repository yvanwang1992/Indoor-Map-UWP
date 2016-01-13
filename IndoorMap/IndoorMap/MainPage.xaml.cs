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
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }


        public MainPage_Model StrongTypeViewModel
        {
            get { return (MainPage_Model)GetValue(StrongTypeViewModelProperty); }
            set { SetValue(StrongTypeViewModelProperty, value); }
        }

        public static readonly DependencyProperty StrongTypeViewModelProperty =
                    DependencyProperty.Register("StrongTypeViewModel", typeof(MainPage_Model), typeof(MainPage), new PropertyMetadata(null));

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        { 
            
            
        }

        private void GetCity(Geocoordinate geocoordinate)
        { 
            string url = string.Format(@"http://api.map.baidu.com/geocoder/v2/?ak={0}&location={1},{2}&output=json",
                Configmanager.BAIDUMAP_APPKEY, geocoordinate.Point.Position.Latitude, geocoordinate.Point.Position.Longitude);

            FormAction GeocodingBaiDuAction = new FormAction(url);
            GeocodingBaiDuAction.Run(false);
            GeocodingBaiDuAction.FormActionCompleted += (result, ss) =>
            {
                /*
                {"status":0,"result":{"location":{"lng":121.34878897239,"lat":31.219452183508},"formatted_address":"上海市闵行区仙霞西路地道","business":"华漕,虹桥机场","addressComponent":{"city":"上海市","country":"中国","direction":"","distance":"","district":"闵行区","province":"上海市","street":"仙霞西路地道","street_number":"","country_code":0},"poiRegions":[{"direction_desc":"\u5185","name":"\u4e0a\u6d77\u8679\u6865\u673a\u573a"}],"sematic_description":"上海虹桥机场内,许浦港东119米","cityCode":289}}
                */
                JToken jtoken = JToken.Parse(result);
                string status = jtoken["status"].ToString();
                if (string.Equals(status, "0"))
                {
                    string city = jtoken["result"]["addressComponent"]["city"].ToString().Replace("市", "");
                    //Save the Located City
                    AppSettings.Intance.LocationCity = city;
                }
            };
        }

        private void appbarRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingPage));
        }

        private async void appbarLocate_Click(object sender, RoutedEventArgs e)
        {
            var position = await LocationManager.GetPosition();
            if (position != null)
            {
                AppSettings.Intance.LocationSetting = true;
                GetCity(position.Coordinate);
                maps.Center = position.Coordinate.Point;
                maps.ZoomLevel = 50;

                foreach (var item in StrongTypeViewModel.MallList)
                {
                    MapIcon mapIcon = new MapIcon();
                    mapIcon.Location = new Geopoint(new BasicGeoposition() { Latitude = Double.Parse(item.lat), Longitude = Double.Parse(item.lon) });
                    mapIcon.Title = item.name;
                    mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/delete_auto.png"));
                    maps.MapElements.Add(mapIcon);
                }
            }
            else
            {
                AppSettings.Intance.LocationSetting = false;
            }
        }
    }
}

using IndoorMap.Helpers;
using IndoorMap.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace IndoorMap.UserControls
{

    public enum MapOptionItemType
    {
        MapOptionItemGridPerspective,
        MapOptionItemGPS,
        MapOptionItemLayer
    }

    public class MapElementClickItemEventArgs
    {
        public MapElementClickEventArgs args;
        public bool isMaxZoom;  //ture: 为了放大的时候能够将点击的图标置于中间
    }

    public sealed partial class MapUserControl : UserControl
    {
        public Geopoint defaultGeopoint = new Geopoint(new BasicGeoposition() { Latitude = 28.23, Longitude = 117.02 });

        public delegate void MapElementClickHandler(MapControl sender, MapElementClickItemEventArgs args);
        public event MapElementClickHandler ElementClickEvent;

        public List<PaneModel> MapOptionList { get; set; }
         public MapUserControl()
        {
            this.InitializeComponent();
            maps.Style = MapStyle.Road;
            maps.Center = defaultGeopoint;

            //所有的应该用同一个PaneModel类   以后更改
            MapOptionList = new List<PaneModel>();
            MapOptionList.Add(new PaneModel() { Label = "倾斜", Icon = "\xE809" });
            MapOptionList.Add(new PaneModel() { Label = "定位", Icon = "\xE1D2" });
            MapOptionList.Add(new PaneModel() { Label = "视图", Icon = "\xE81E" });
        }

        public bool MapUserLandMarkVisible
        {
            get { return (bool)GetValue(MapUserLandMarkVisibleProperty); }
            set { SetValue(MapUserLandMarkVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapUserLandMarkVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapUserLandMarkVisibleProperty =
            DependencyProperty.Register("MapUserLandMarkVisible", typeof(bool), typeof(MapUserControl), new PropertyMetadata(true, (sender, args) =>
            {
                MapUserControl mapUser = sender as MapUserControl;
                mapUser.ChangeLandMarkVisible();
            }));

        public void ChangeLandMarkVisible()
        {
            maps.LandmarksVisible = MapUserLandMarkVisible;
        }

        public int MapUserZoomLevel
        {
            get { return (int)GetValue(MapUserZoomLevelProperty); }
            set { SetValue(MapUserZoomLevelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapUserZoomLevel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapUserZoomLevelProperty =
            DependencyProperty.Register("MapUserZoomLevel", typeof(int), typeof(MapUserControl), new PropertyMetadata(0, (sender, args) =>
            {
                MapUserControl mapUser = sender as MapUserControl;
                mapUser.ChangeZoomLevel();
            }));

        public void ChangeZoomLevel()
        {
            maps.ZoomLevel = MapUserZoomLevel;
        }

        public Geopoint MapUserCenter
        {
            get { return (Geopoint)GetValue(MapUserCenterProperty); }
            set { SetValue(MapUserCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapUserCenter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapUserCenterProperty =
            DependencyProperty.Register("MapUserCenter", typeof(Geopoint), typeof(MapUserControl), new PropertyMetadata(new Geopoint(new BasicGeoposition() { }), (sender, args) =>
            {
                MapUserControl mapUser = sender as MapUserControl;
                mapUser.ChangeCenter();
            }));

        private void ChangeCenter()
        {
            maps.Center = MapUserCenter; 
        }

        //Map Element
        public ObservableCollection<MapElement> MapUserElements
        {
            get { return (ObservableCollection<MapElement>)GetValue(MapUserElementsProperty); }
            set { SetValue(MapUserElementsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapUserElements.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapUserElementsProperty =
            DependencyProperty.Register("MapUserElements", typeof(ObservableCollection<MapElement>), typeof(MapUserControl), new PropertyMetadata(new ObservableCollection<MapElement>(), OnMapElementsChanged));

        public static void OnMapElementsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MapUserControl mapUser = d as MapUserControl;
            mapUser.ClearAllMapElements();
            mapUser.AddMapElementInMap();
        }

        private void ClearAllMapElements()
        {
            maps.MapElements.Clear();
        }

        public void AddMapElementInMap()
        {
            foreach (var element in MapUserElements)
            {
                maps.MapElements.Add(element);
            }
        }

        private async void maps_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var location = args.Location.Position;
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //if (maps.ZoomLevel < 15)
                //{
                //    maps.StartContinuousZoom(2);
                //    maps.ZoomLevelChanged += (ss, ee) =>
                //    {
                //        if (maps.ZoomLevel >= 15)
                //        {
                //            maps.StopContinuousZoom();
                //        }
                //    };
                //    if (ElementClickEvent != null)
                //    {
                //        ElementClickEvent(sender, new MapElementClickItemEventArgs() { args = args, isMaxZoom = false });
                //    }
                //}
                //else
                //{
                //    if (ElementClickEvent != null)
                //    {
                //        ElementClickEvent(sender, new MapElementClickItemEventArgs() { args = args, isMaxZoom = true });
                //    }
                //} 
                ElementClickEvent(sender, new MapElementClickItemEventArgs() { args = args, isMaxZoom = true });

            });


        }

        private async void Location()
        {
            var position = await LocationManager.GetPosition();
            if (position != null)
            {
                maps.Children.Clear();
                var markerLocation = LocationMarker();
                maps.Children.Add(markerLocation);
                var marsPosition = LocationManager.TransformFromWorldlToMars(position.Coordinate.Point);
                MapControl.SetLocation(markerLocation, marsPosition);
                MapControl.SetNormalizedAnchorPoint(markerLocation, new Point(0.5, 0.5));
                //AppSettings.Intance.LocationSetting = true;
                //GetCity(position.Coordinate);
                await this.maps.TrySetViewAsync(marsPosition);
                this.maps.ZoomLevel = 15;

                //add mark of my location
            }
        }

        private static UIElement LocationMarker()
        {
            Canvas marker = new Canvas();
            Ellipse outer = new Ellipse() { Height = 25, Width = 25 };
            outer.Fill = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            outer.Margin = new Thickness(-12.5, -12.5, 0, 0);

            Ellipse inner = new Ellipse() { Width = 20, Height = 20 };
            inner.Fill = new SolidColorBrush(Colors.Black);
            inner.Margin = new Thickness(-10, -10, 0, 0);

            Ellipse core = new Ellipse() { Width = 10, Height = 10 };
            core.Fill = new SolidColorBrush(Colors.White);
            core.Margin = new Thickness(-5, -5, 0, 0);

            marker.Children.Add(outer);
            marker.Children.Add(inner);
            marker.Children.Add(core);
            return marker;
        }

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var btn = sender as Button;
        //    foreach (var item in Enum.GetValues(typeof(MapStyle)))
        //    {
        //        maps.Style = (MapStyle)Enum.Parse(typeof(MapStyle), item.ToString());
        //        btn.Content = maps.Style.ToString();
        //        await Task.Delay(5000);
        //    } 
        //} 

        bool isTilt = false; 

        private async void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var model = (sender as Button).DataContext as PaneModel;
            switch (model.Label)
            {
                case "倾斜":
                    if (!isTilt)//Not Tilt
                    {
                        await maps.TryTiltAsync(70);
                        isTilt = true;
                    }
                    else
                    {
                        await maps.TryTiltAsync(-70);
                        isTilt = false;
                    }
                    break;
                case "定位":
                    Location();
                    break;
                case "视图":
                    if (maps.Style == MapStyle.Road)
                        maps.Style = MapStyle.Aerial;
                    else
                        maps.Style = MapStyle.Road;
                    break;
            }
        }
    }
}

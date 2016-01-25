using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace IndoorMap.UserControls
{
    public class MapElementClickItemEventArgs
    {
        public MapElementClickEventArgs args;
        public bool isMaxZoom;  //ture: 为了放大的时候能够将点击的图标置于中间
    }

    public sealed partial class MapUserControl : UserControl
    {
        public delegate void MapElementClickHandler(MapControl sender, MapElementClickItemEventArgs args);
        public event MapElementClickHandler ElementClickEvent;

        public MapUserControl()
        {
            this.InitializeComponent();
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
            DependencyProperty.Register("MapUserCenter", typeof(Geopoint), typeof(MapUserControl), new PropertyMetadata(new Geopoint(new BasicGeoposition() { }), (sender,args)=> 
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
                if (maps.ZoomLevel < 15)
                {
                    maps.StartContinuousZoom(1);
                    maps.ZoomLevelChanged += (ss, ee) =>
                    {
                        if (maps.ZoomLevel >= 15)
                        {
                            maps.StopContinuousZoom();
                        }
                    };
                    if (ElementClickEvent != null)
                    {
                        ElementClickEvent(sender, new MapElementClickItemEventArgs() { args = args, isMaxZoom = false });
                    }
                }
                else
                {
                    if (ElementClickEvent != null)
                    {
                        ElementClickEvent(sender, new MapElementClickItemEventArgs() { args = args, isMaxZoom = true });
                    }
                }

            });

            
        }
    }
}

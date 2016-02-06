using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace IndoorMap
{
    public static class Configmanager
    {
        public const string INDOORMAP_APPKEY = @"a1b6117970d60145d3e35e730e7d5336";
        public const string BAIDUMAP_APPKEY = @"F2bfa45a60b28310dcdadf7f39de917d";
        
        //整体屏幕大小
        public static double ScreenHeight = Window.Current.Bounds.Height;
        public static double ScreenWidth = Window.Current.Bounds.Width;

        public static string App_Version = "v1.0.0"; 

        public static Color ThemeColor = Color.FromArgb(100,90, 165, 240);
        
    }
}

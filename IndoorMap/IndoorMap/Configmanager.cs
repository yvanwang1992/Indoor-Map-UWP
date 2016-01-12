using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace IndoorMap
{
    public static class Configmanager
    {
        public const string INDOORMAP_APPKEY = @"a1b6117970d60145d3e35e730e7d5336";
        public const string BAIDUMAP_APPKEY = @"F2bfa45a60b28310dcdadf7f39de917d";



        public static double ScreenHeight = Window.Current.Bounds.Height;
        public static double ScreenWidth = Window.Current.Bounds.Width;

    }
}

using IndoorMap.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace IndoorMap.Helpers
{
    public class DataManager
    { 
        public static double ScreenWidth = Window.Current.Bounds.Width;
        public static double ScreenHeight = Window.Current.Bounds.Height;


        public const string CityMallListFile = "CityMallListFolder";
        public const string CityListListFile = "CityListListFolder";

        private static List<SavedMallListModel> savedMallList;
        public static List<SavedMallListModel> SavedMallList
        {
            get
            {
                if (savedMallList == null)
                {
                    savedMallList = new List<SavedMallListModel>();
                }
                return savedMallList;
            }
            set { savedMallList = value; }
        } 
    }
}

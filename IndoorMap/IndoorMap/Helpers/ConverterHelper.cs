using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace IndoorMap.Helpers
{ 
    public class OpenTimeConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, String language)
        {
            if (value != null)
            {
                string openTime = value.ToString();

                return "营业时间：" + openTime;
            }
            return string.Empty;
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, String language)
        {
            return value;
        }
    }

    public class EmptyStringToVisibilityConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, String language)
        {
            Visibility visible = Visibility.Visible;
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                visible = Visibility.Collapsed;
            return visible;
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, String language)
        {
            return value;
        }
    }

    public class TrafficMultiLineConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, String language)
        {
            if(value == null)
            {
                return string.Empty;
            }
            string traffic = value.ToString();
            var list = traffic.Split(';');
            string returnValue = string.Empty;
            foreach (var item in list)
            {
                if (list.LastOrDefault() != item)
                    returnValue += item + "\r\n";
                else
                    returnValue += item;
            }
            return returnValue.TrimStart();
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, String language)
        {
            return value;
        }
    }


    public class FloorConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, String language)
        {
            string floor = value.ToString();

            floor = floor.Replace("Floor", "") + (floor.Contains("B")? "" : "  ")  + "楼";

            return floor;
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, String language)
        {
            return value;
        }
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IndoorMap.Helpers
{
    public class ListViewDependencyProperty : DependencyObject
    {  
        public static void OnScrollToIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int index = Int32.Parse(e.NewValue.ToString());
            if (index != -1)
            {
                var listView = d as ListView;
                var item = listView.Items[index];
                listView.ScrollIntoView(item);
            } 
        }
          
        public static int GetScrollToIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(ScrollToIndexProperty);
        }

        public static void SetScrollToIndex(DependencyObject obj, int value)
        {
            obj.SetValue(ScrollToIndexProperty, value);
        }

        // Using a DependencyProperty as the backing store for ScrollToIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollToIndexProperty =
            DependencyProperty.RegisterAttached("ScrollToIndex", typeof(int), typeof(ListViewDependencyProperty), new PropertyMetadata(0, OnScrollToIndexChanged));


    }
}

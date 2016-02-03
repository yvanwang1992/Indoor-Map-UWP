using IndoorMap.Models;
using IndoorMap.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace IndoorMap.Helpers
{
    public class ListViewDependencyProperty : DependencyObject
    {

        private static int GoToListViewIndex(ListView listView, DependencyPropertyChangedEventArgs e)
        {
            if(listView.SelectedItem == null)
            {
                return -1;
            }
            MallModel selectedItem;
            if (listView.SelectedIndex == 0 && e.OldValue != null)
                selectedItem = e.OldValue as MallModel;
            else
                selectedItem = e.NewValue as MallModel;

            int index = 0; 
            foreach (var item in listView.Items)
            {
                if ((item as MallModel).id == selectedItem.id)
                {
                    break;
                }
                else
                    index++;
            }
            return index;
        }
        //附加属性
        public static async void OnScrollToIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listView = d as ListView;
            int index = GoToListViewIndex(listView,  e);
            if (index != -1)
            {
                var item = listView.Items[index];
                if (item != null)
                {
                    listView.SelectedIndex = index;
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => 
                    {
                        listView.ScrollIntoView(item, ScrollIntoViewAlignment.Leading);
                    });
                }
            }
        }
          
        public static MallModel GetScrollToIndex(DependencyObject obj)
        {
            return (MallModel)obj.GetValue(ScrollToIndexProperty);
        }

        public static void SetScrollToIndex(DependencyObject obj, MallModel value)
        {
            obj.SetValue(ScrollToIndexProperty, value);
        }
        //注册附加属性
        // Using a DependencyProperty as the backing store for ScrollToIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollToIndexProperty =
            DependencyProperty.RegisterAttached("ScrollToIndex", typeof(MallModel), typeof(ListViewDependencyProperty), new PropertyMetadata(new MallModel(), OnScrollToIndexChanged));
         
    }

    public class AutoSuggestBoxDependencyProperty: DependencyObject
    {

        public static void OnIsFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var autoSuggestBox = d as AutoSuggestBox;

            bool isFocus = (bool)e.NewValue;
            if (isFocus)
            {
                Button a = (Button)autoSuggestBox.ContainerFromIndex(0);//.Focus(FocusState.Programmatic);
            }
            else
                autoSuggestBox.Focus(FocusState.Unfocused);
            
        }

        public static bool GetIsAutoSuggestBoxFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAutoSuggestBoxFocusProperty);
        }

        public static void SetIsAutoSuggestBoxFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAutoSuggestBoxFocusProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsAutoSuggestBoxFocus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAutoSuggestBoxFocusProperty =
            DependencyProperty.RegisterAttached("IsAutoSuggestBoxFocus", typeof(bool), typeof(AutoSuggestBoxDependencyProperty), new PropertyMetadata(false, OnIsFocusChanged));


    }
}

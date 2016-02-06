using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace IndoorMap.Helpers
{
    public static class Funtions
    {
         public static IEnumerable<DependencyObject> GetChilds<T>(this DependencyObject d)  where T:class
        {
            Queue<DependencyObject> queue = new Queue<DependencyObject>();

            queue.Enqueue(d);
            while (queue.Count > 0)
            {
                DependencyObject obj = queue.Dequeue();
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    var child = VisualTreeHelper.GetChild(obj, i);
                    var childType = child as T;
                    if (childType != null)
                    {
                        yield return child;
                    }
                    queue.Enqueue(child);
                }
            }
        }

        public static DependencyObject GetParent(this DependencyObject d)
        {
            var parent =VisualTreeHelper.GetParent(d);
            if (parent != null)
                return parent;
            return null;
        }

        public static IEnumerable<DependencyObject> GetVisualChildren(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return GetVisualChildrenAndSelfIterator(element).Skip<DependencyObject>(1);
        }

        public static IEnumerable<DependencyObject> GetVisualDescendants(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return GetVisualDescendantsAndSelfIterator(element).Skip<DependencyObject>(1);
        }

        private static IEnumerable<DependencyObject> GetVisualDescendantsAndSelfIterator(DependencyObject element)
        {
            Queue<DependencyObject> iteratorVariable0 = new Queue<DependencyObject>();
            iteratorVariable0.Enqueue(element);
            while (true)
            {
                if (iteratorVariable0.Count <= 0)
                {
                    yield break;
                }
                DependencyObject iteratorVariable1 = iteratorVariable0.Dequeue();
                yield return iteratorVariable1;
                foreach (DependencyObject obj2 in GetVisualChildren(iteratorVariable1))
                {
                    iteratorVariable0.Enqueue(obj2);
                }
            }
        }

        private static IEnumerable<DependencyObject> GetVisualChildrenAndSelfIterator(DependencyObject element)
        {
            yield return element;
            int childrenCount = VisualTreeHelper.GetChildrenCount(element);
            int childIndex = 0;
            while (true)
            {
                if (childIndex >= childrenCount)
                {
                    yield break;
                }
                yield return VisualTreeHelper.GetChild(element, childIndex);
                childIndex++;
            }
        }

        public static T GetVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            T t = default(T);

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    t = child as T;
                    break;
                }
                else
                {
                    var t1 = GetVisualChild<T>(child);
                    if (t1 != null)
                    {
                        t = t1;
                        break;
                    }
                }
            }
            return t;
        }

     
    }
}

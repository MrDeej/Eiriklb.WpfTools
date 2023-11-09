using System.Windows;
using System.Windows.Media;

namespace Eiriklb.WpfTools
{
    public static class WpfExtensions
    {
        public static T? FindControl<T>(DependencyObject root)
        {
            var queue = new Queue<DependencyObject>(new[] { root });

            do
            {
                var item = queue.Dequeue();

                if (item is T viewer)
                    return viewer;

                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(item); i++)
                    queue.Enqueue(VisualTreeHelper.GetChild(item, i));
            } while (queue.Count > 0);

            return default(T);
        }
    }
}

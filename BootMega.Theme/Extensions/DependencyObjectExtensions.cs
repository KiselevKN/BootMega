using System.Windows;
using System.Windows.Media;

namespace BootMega.Theme.Extensions
{
    public static class DependencyObjectExtensions
    {
        public static T GetParentByType<T>(this DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            T parent = parentObject as T;
            return (parent != null) ? parent : GetParentByType<T>(parentObject);
        }
    }
}

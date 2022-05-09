using Caliburn.Micro;
using System.Linq;
using System.Windows;

namespace WPF.Common.Helpers
{
    internal static class WindowHelper
    {
        public static bool IsWindowOpen<T>(this IWindowManager windowManager, string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
                ? Application.Current.Windows.OfType<T>().Any()
                : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
    }
}

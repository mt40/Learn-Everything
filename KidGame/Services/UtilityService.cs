using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidGame.Services
{
    public static class UtilityService
    {
        public static Random GlobalRandom = new Random();

        /// <summary>
        /// Create a unique string
        /// </summary>
        public static string GenerateUniqueString()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public static void GoToVisualState(System.Windows.Controls.Control control, string state_name, bool isAnimated = true)
        {
            System.Windows.VisualStateManager.GoToState(control, state_name, isAnimated);
        }

        public static bool IsEmptyOrWhiteSpace(this string s)
        {
            if (s == null)
                return true;
            return s.All(char.IsWhiteSpace);
        }

        /// <summary>
        /// Shuffle a generic list
        /// </summary>
        public static List<T> Shuffle<T>(this IList<T> list, Random rng = null)
        {
            if (rng == null)
                rng = new Random();
            //clone the list. But keep reference to old list 
            var resultList = list.ToList();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = resultList[k];
                resultList[k] = resultList[n];
                resultList[n] = value;
            }

            return resultList;
        }

        /// <summary>
        /// Search for a child element of a control. Use to find child element inside template.
        /// </summary>
        public static object FindVisualChildByType<T>(System.Windows.DependencyObject element, String name)
        {
            if (element == null) return null;
            var frameworkElement = element as System.Windows.FrameworkElement;
            if (frameworkElement == null) return null;
            if (frameworkElement.Name.Equals(name)) return frameworkElement;
            int childrenCount = System.Windows.Media.VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childrenCount; i++)
            {
                object childElement = FindVisualChildByType<T>(System.Windows.Media.VisualTreeHelper.GetChild(element, i), name);
                if (childElement != null)
                    return childElement;
            }
            return null;
        }

        /// <summary>
        /// Search for first occurence of an object of type T
        /// </summary>
        public static T FindFirstElementInVisualTree<T>(System.Windows.DependencyObject parentElement) where T : System.Windows.DependencyObject
        {
            var count = System.Windows.Media.VisualTreeHelper.GetChildrenCount(parentElement);
            if (count == 0)
                return null;

            for (int i = 0; i < count; i++)
            {
                var child = System.Windows.Media.VisualTreeHelper.GetChild(parentElement, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    var result = FindFirstElementInVisualTree<T>(child);
                    if (result != null)
                        return result;

                }
            }
            return null;
        }

        /// <summary>
        /// Random a color dark enough to go with white text
        /// </summary>
        public static System.Windows.Media.Color RandomDarkColor(Random r = null)
        {
            if (r == null)
                r = new Random();

            var color = System.Windows.Media.Color.FromArgb(255, (byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));

            //r, g, b all greater than 200 will result in a very light color
            if (color.R >= 200)
            {
                color.G = (byte)r.Next(0, 200);
                color.B = (byte)r.Next(0, 200);
            }
            else if (color.G >= 200)
            {
                color.R = (byte)r.Next(0, 200);
                color.B = (byte)r.Next(0, 200);
            }
            else if (color.B >= 200)
            {
                color.R = (byte)r.Next(0, 200);
                color.G = (byte)r.Next(0, 200);
            }

            return color;
        }
    }
}

using System;
using System.Windows;
using System.Windows.Resources;
using Telerik.Windows.Controls;

namespace KidGame.Models
{
    public class TemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate
        {
            get;
            set;
        }

        public DataTemplate NoSoundTemplate
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            RadSlideView view = ElementTreeHelper.FindVisualAncestor<RadSlideView>(container);
            if (view == null)
            {
                return null;
            }

            if((item as Concept).HasSound)
                return this.DefaultTemplate;
            return NoSoundTemplate;
        }
    }
}

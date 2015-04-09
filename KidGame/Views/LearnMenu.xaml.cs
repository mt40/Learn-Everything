using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace KidGame.Views
{
    public partial class LearnMenu : PhoneApplicationPage
    {
        private KidGame.Services.GeneralService _generalService = KidGame.Services.GeneralService.Instance;

        public LearnMenu()
        {
            InitializeComponent();

            this.Loaded += LearnMenu_Loaded;
        }

        void LearnMenu_Loaded(object sender, RoutedEventArgs e)
        {
            ListCategory.ItemsSource = _generalService.Categories;
        }

        private void CategoryItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            _generalService.CurrentCategory = (sender as FrameworkElement).DataContext as KidGame.Models.Category;

            if(_generalService.CurrentMode == Models.GameMode.LearnMode)
                NavigationService.Navigate(new Uri("/Views/ConceptPage.xaml", UriKind.RelativeOrAbsolute));
            else if(_generalService.CurrentMode == Models.GameMode.PlayMode)
                NavigationService.Navigate(new Uri("/Views/QuestionPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
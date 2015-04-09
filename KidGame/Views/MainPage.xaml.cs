using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using KidGame.Resources;
using KidGame.Services;

namespace KidGame.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            
            this.Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            (Resources["BackgroundAnimation"] as System.Windows.Media.Animation.Storyboard).Begin();
        }

        private void ButtonLearn_Click(object sender, RoutedEventArgs e)
        {
            GeneralService.Instance.CurrentMode = Models.GameMode.LearnMode;
            NavigationService.Navigate(new Uri("/Views/LearnMenu.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            GeneralService.Instance.CurrentMode = Models.GameMode.PlayMode;
            NavigationService.Navigate(new Uri("/Views/LearnMenu.xaml", UriKind.RelativeOrAbsolute));
        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using KidGame.Services;
using KidGame.Models;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media;

namespace KidGame.Views
{
    public partial class ConceptPage : PhoneApplicationPage
    {
        private GeneralService _generalService = GeneralService.Instance;
        private MediaElement _currentMedia;
        private Grid _previousButton;

        public ConceptPage()
        {
            InitializeComponent();

            Loaded += ConceptPage_Loaded;

            LayoutRoot.Background = new System.Windows.Media.SolidColorBrush(
                UtilityService.RandomDarkColor(UtilityService.GlobalRandom));

            if (_generalService.CurrentCategory != null)
                TextBlockTitle.Text = _generalService.CurrentCategory.Name;
        }

        void ConceptPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (_generalService.CurrentCategory != null)
                SlideViewConcept.ItemsSource = _generalService.CurrentCategory.DisplayItems;
        }

        private void ButtonSound_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var grid = sender as Grid;
            var border = grid.Children[0] as Border;
            var tb = (border.Child as Panel).Children[1] as TextBlock;
            var media = grid.Children[1] as MediaElement;

            if (_currentMedia != null)
                _currentMedia.Stop();

            if (tb.Text == "Hear sound")
            {
                border.Background = new SolidColorBrush(Colors.Magenta);
                tb.Text = "Stop sound";

                media.Source = (grid.DataContext as Concept).Sound;
                media.MediaOpened += (s, arg) =>
                {
                    media.Play();
                    _currentMedia = media;
                };
            }
            else
            {
                border.Background = new SolidColorBrush(Color.FromArgb(255, 135, 206, 235));
                tb.Text = "Hear sound";
                media.Stop();
            }

            if (_previousButton != null)
                ToggleButtonPlaySound(_previousButton);
            _previousButton = grid;
        }

        private void ToggleButtonPlaySound(Grid grid)
        {
            var border = grid.Children[0] as Border;
            var tb = (border.Child as Panel).Children[1] as TextBlock;

            if (tb.Text == "Hear sound")
            {
                border.Background = new SolidColorBrush(Colors.Magenta);
                tb.Text = "Stop sound";
            }
            else
            {
                border.Background = new SolidColorBrush(Color.FromArgb(255, 135, 206, 235));
                tb.Text = "Hear sound";
            }
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (_previousButton != null)
                ToggleButtonPlaySound(_previousButton);
            _previousButton = null;
        }
    }
}
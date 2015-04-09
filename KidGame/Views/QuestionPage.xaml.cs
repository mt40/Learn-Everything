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
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;

namespace KidGame.Views
{
    public partial class QuestionPage : PhoneApplicationPage
    {
        GeneralService _generalService = GeneralService.Instance;
        public ObservableCollection<Question> DisplayQuestions;
        private int _questionCount, _currentQuestion, _helpUsed;
        private bool _isGameOver;
        private Storyboard _counterStoryboard;

        public QuestionPage()
        {
            InitializeComponent();

            Loaded += QuestionPage_Loaded;

        }

        void QuestionPage_Loaded(object sender, RoutedEventArgs e)
        {
            _counterStoryboard = Resources["TimeAnimation"] as Storyboard;
            (Resources["CountDown"] as Storyboard).Begin();
        }

        private void CountDown_Completed(object sender, EventArgs e)
        {
            DisplayQuestions = new ObservableCollection<Question>()
                {
                    GeneralService.GetRandomQuestion(),
                    GeneralService.GetRandomQuestion()
                };
            SlideViewQuestion.ItemsSource = DisplayQuestions;

            _counterStoryboard.SkipToFill();
            _counterStoryboard.Begin();

            _currentQuestion = 0;
            _questionCount = 0;

            TextBlockTitle.Text = "Question " + (_questionCount + 1);
        }

        private void SlideViewQuestion_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            //disable sliding of SlideViewer
            e.Handled = true;
        }


        private void BorderChoice_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var choice = (sender as FrameworkElement).DataContext as Concept;
            var currentQuestion = DisplayQuestions[_currentQuestion];
            if (currentQuestion.Answer.Uid.Equals(choice.Uid))
            {
                SlideViewQuestion.MoveToNextItem();

                //replace the current question with a new one
                DisplayQuestions[_currentQuestion] = GeneralService.GetRandomQuestion();
                _questionCount++;
                TextBlockTitle.Text = "Question " + (_questionCount + 1);

                if (_currentQuestion == 1)
                    _currentQuestion = 0;
                else
                    _currentQuestion = 1;

                _counterStoryboard.Stop();
                _counterStoryboard.Begin();

                TextBlockAnswer.Text = "";
            }
            else
            {
                GameOver();
            }
        }

        private void TimeAnimation_Completed(object sender, EventArgs e)
        {
            GameOver();
        }

        private void GameOver()
        {
            if (_isGameOver == false)
            {
                _counterStoryboard.Stop();
                GridGameOver.Visibility = System.Windows.Visibility.Visible;
                TextBlockAnswerCount.Text = _questionCount.ToString() + "-" + _helpUsed.ToString();
                _generalService.CurrentUser.Score += _questionCount - _helpUsed;
                TextBlockUserScore.Text = _generalService.CurrentUser.Score.ToString();
                TextBlockRank.Text = _generalService.CurrentUser.Rank.ToString();

                _isGameOver = true;
            }

            TextBlockAnswer.Text = "";
        }

        private void ButtonShare_Click(object sender, RoutedEventArgs e)
        {
            var sst = new Microsoft.Phone.Tasks.ShareStatusTask();
            sst.Status = "My score is " + _generalService.CurrentUser.Score + ". Current rank: " + _generalService.CurrentUser.Rank.Value;
            sst.Show();
        }

        private void ButtonReplay_Click(object sender, RoutedEventArgs e)
        {
            _isGameOver = false;
            GridGameOver.Visibility = System.Windows.Visibility.Collapsed;
            (Resources["CountDown"] as Storyboard).Begin();
        }

        private void ButtonQuit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void TextBlockHelp_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlockAnswer.Text = "Hint: " + DisplayQuestions[_currentQuestion].Answer.Name;
            _helpUsed++;
        }


    }
}
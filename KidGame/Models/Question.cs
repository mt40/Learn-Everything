using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidGame.Models
{
    public class Question : System.ComponentModel.INotifyPropertyChanged
    {
        private Concept _answer;        
        private ObservableCollection<Concept> _choices;     

        public Concept Answer
        {
            get { return _answer; }
            set { _answer = value; NotifyPropertyChanged(); }
        }

        public ObservableCollection<Concept> Choices
        {
            get { return _choices; }
            set { _choices = value; NotifyPropertyChanged(); }
        }

        public System.Windows.Media.Imaging.BitmapImage Picture
        {
            get { return _answer.Picture; }
        }

        public Question() 
        {
            _choices = new ObservableCollection<Concept>();
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

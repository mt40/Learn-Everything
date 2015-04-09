using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KidGame.Services;

namespace KidGame.Models
{
    public class Category : System.ComponentModel.INotifyPropertyChanged
    {
        private string _name;

        private System.Collections.ObjectModel.ObservableCollection<Concept> _items;

        public System.Collections.ObjectModel.ObservableCollection<Concept> DisplayItems
        {
            get
            {
                if (_items != null)
                {
                    return new System.Collections.ObjectModel.ObservableCollection<Concept>(_items.Shuffle(UtilityService.GlobalRandom));
                }
                return null;
            }
            set
            {
                _items = value;
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(); }
        }

        public System.Windows.Media.Imaging.BitmapImage Picture { get; set; }

        public Category(string name = "unknown")
        {
            _name = name;
            _items = new System.Collections.ObjectModel.ObservableCollection<Concept>();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidGame.Models
{
    public class Concept : System.ComponentModel.INotifyPropertyChanged
    {
        private string _uid, _name, _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; NotifyPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(); }
        }

        public string Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public bool HasSound = true;

        public string MediaFolderRoot { get; set; }

        public System.Windows.Media.Imaging.BitmapImage Picture
        {
            get
            {
                return new System.Windows.Media.Imaging.BitmapImage(
                    new Uri("/Assets/Pictures/" + MediaFolderRoot + "/" + _name.Replace(" ","_") + ".png", UriKind.RelativeOrAbsolute));
            }
        }

        public Uri Sound
        {
            get
            {
                return new Uri("/Assets/Sounds/" + MediaFolderRoot + "/" + _name.Replace(" ", "_") + ".mp3", UriKind.RelativeOrAbsolute);
                //return new Uri("/Assets/Sounds/" + MediaFolderRoot + "/" + "mouse" + ".mp3", UriKind.RelativeOrAbsolute);
            }
        }

        public Concept(string name = "unknown", string mediaFolderRoot = null, bool hasSound = true)
        {
            _uid = KidGame.Services.UtilityService.GenerateUniqueString();
            _name = name;
            if (mediaFolderRoot == null)
                throw new ArgumentException("MediaFolderRoot property cannot be null");

            MediaFolderRoot = mediaFolderRoot;
            HasSound = hasSound;
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

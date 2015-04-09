using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidGame.Models
{
    public class GameUser
    {
        private int _score;
        //private GameRank _rank;

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                Save("Score", _score);
            }
        }

        public GameRank Rank
        {
            get
            {
                if (_score <= 1)
                    return GameRank.Unknown;
                if (_score <= 10)
                    return GameRank.NewKid;
                if (_score <= 25)
                    return GameRank.Familiar;
                if (_score <= 50)
                    return GameRank.GoodStudent;
                if (_score <= 100)
                    return GameRank.Gifted;
                if (_score <= 150)
                    return GameRank.Genius;
                if (_score <= 200)
                    return GameRank.Scholar;
                return GameRank.Legend;
            }
            //set { _rank = value; }
        }

        public GameUser()
        {
            try
            {
                _score = (int)Load("Score");
            }
            catch { _score = 0; }
            //_rank = (GameRank)Load("Rank");
        }

        /// <summary>
        /// Save object to IsolatedStorage
        /// </summary>
        public static void Save(string key, object obj)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings[key] = obj;
            settings.Save();
        }

        /// <summary>
        /// Load object from IsolatedStorage
        /// </summary>
        public static object Load(string key)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(key))
                return settings[key];
            return null;
        }
    }
}

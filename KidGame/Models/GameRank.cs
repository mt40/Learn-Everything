using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidGame.Models
{
    /// <summary>
    /// Type-safe enum
    /// </summary>
    public class GameRank
    {
        private GameRank(string value = "Unknown") { Value = value; }

        public string Value { get; set; }

        public static GameRank Unknown { get { return new GameRank("Unknown"); } }
        public static GameRank NewKid { get { return new GameRank("New Kid"); } }
        public static GameRank Familiar { get { return new GameRank("Familiar"); } }
        public static GameRank GoodStudent { get { return new GameRank("Good Student"); } }
        public static GameRank Gifted { get { return new GameRank("Gifted"); } }
        public static GameRank Genius { get { return new GameRank("Genius"); } }
        public static GameRank Scholar { get { return new GameRank("Scholar"); } }
        public static GameRank Legend { get { return new GameRank("Legend"); } }

        public override string ToString()
        {
            return Value;
        }
    }
}

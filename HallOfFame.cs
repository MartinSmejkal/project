using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls.Primitives;

namespace project
{
    /*
     * Class providing utilities above stored Games,
     * such as filtering, storing and loading from files
     */
    public class HallOfFame
    {

        public HallOfFame()
        {
            games = new List<Game>();
        }

        private List<Game> games;

        /*
         * Method that goes through all stored games and returns HashSet<string>
         * containing all unique player names.
         */
        public HashSet<string> GetNames()
        {
            HashSet<string> names = new HashSet<string>();
            foreach (Game g in games)
            {
                names.Add(g.PlayerCircle);
                names.Add(g.PlayerCross);
            }
            return names;
        }

        /*
         * Method returning Dictionary<string, ushort> containing 
         * pair of player name and games won, ordered by games won (descending)
         */
        public Dictionary<string, ushort> GetTopPlayers()
        {
            Dictionary<string, ushort> top = new Dictionary<string, ushort>();
            foreach (string name in GetNames())
            {
                top.Add(name, 0);
            }
            foreach (Game game in games)
            {
                if (game.Winner == State.circle)
                {
                    top[game.PlayerCircle]++;
                }
                else if (game.Winner == State.cross)
                {
                    top[game.PlayerCross]++;
                }
            }
            top = top.OrderByDescending(top => top.Value).ToDictionary();
            //top = top.Take(10).ToDictionary(); just in case it is really needed to display only first 10 
            return top;
        }

        /*
         * Method adding the results of new Game to the list of played games.
         */
        public void AddGame(Game game)
        {
            games.Add(game);
        }

        public static string GetResourcesFilePath()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = string.Format("{0}Resources\\SavedGames.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\..\")));
            return FileName;
        }
        public void LoadGames(string filepath)
        {
            if (File.Exists(filepath))
            {
                try
                {
                    games = XmlSerialization.ReadFromXmlFile<List<Game>>(filepath);
                }
                catch (InvalidOperationException e)
                {
                    games = new List<Game>();
                    //corrupted file
                }
            }
            else
            {
                games = new List<Game>();
            }
        }
        public void SaveGames(string filepath)
        {
            XmlSerialization.WriteToXmlFile<List<Game>>(filepath, games);
        }



    }
}

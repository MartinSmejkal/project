using System.IO;

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

        /*
         * Private datastructure to store loaded Game(s).
         */
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

        /*
         * Method returning file path as a string to the SavedGames.txt located in /Resources.
         */
        public static string GetResourcesFilePath()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = string.Format("{0}Resources\\SavedGames.txt", Path.GetFullPath(Path.Combine(RunningPath, @"")));
            return FileName;
        }

        /*
         * Method which tries to load saved games from XML file,
         * requires path to the file as a string,
         * uses XmlSerialization, see more in its class,
         * if any errors occur during import, sets games to empty List<Same>
         */
        public void LoadGames(string filepath)
        {
            if (File.Exists(filepath))
            {
                try
                {
                    games = XmlSerialization.ReadFromXmlFile<List<Game>>(filepath);
                }
                catch (InvalidOperationException)
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

        /*
         * Method that stores loaded games to XML file in given file path,
         * uses XmlSerialization,
         * if file does`t exist, creates new one.
         */
        public void SaveGames(string filepath)
        {
            XmlSerialization.WriteToXmlFile<List<Game>>(filepath, games);
        }
    }
}

using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class HallOfFame
    {

        public HallOfFame() 
        {
            games = new List<Game>();
        }

        private List<Game> games;

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
                }else if (game.Winner == State.cross)
                {
                    top[game.PlayerCross]++;
                }
            }
            
            return (Dictionary<string, ushort>)top.OrderByDescending(top => top.Value);
        }

        private void LoadGames() { }
        private void SaveGames() { }



    }
}

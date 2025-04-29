using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    /*
     * Class storing the results of played games (exported from class Field),
     * the class itself does nothing, for processing see HallOfFame. 
     */
    public class Game
    {
        public Game(string playerCross, string playerCircle, State winner, ushort turnCounter, byte winCondition, byte fieldsize)
        {
            PlayerCross = playerCross;
            PlayerCircle = playerCircle;
            Winner = winner;
            TurnCounter = turnCounter;
            WinCondition = winCondition;
            FieldSize = fieldsize;
        }
        public Game() { }
        public string PlayerCross { get;  set; }

        public string PlayerCircle { get;  set; }

        public State Winner { get;  set; }

        public ushort TurnCounter { get;  set; }

        public byte WinCondition { get;  set; }
        public byte FieldSize { get;  set; }
    }
}

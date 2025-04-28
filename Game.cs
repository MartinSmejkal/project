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

#pragma warning disable CS8618
        private Game() { }
#pragma warning restore CS8618
        public string PlayerCross { get; private set; }

        public string PlayerCircle { get; private set; }

        public State Winner { get; private set; }

        public ushort TurnCounter { get; private set; }

        public byte WinCondition { get; private set; }
        public byte FieldSize { get; private set; }
    }
}

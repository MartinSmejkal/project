using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    /*
     * Class representing the gameboard,
     * contains information about the game state (players, Boxes, settings)
     * and methods to evaluete player actions.
     */
    public class Field
    {
#pragma warning disable CS8618 
        private Field() { }
#pragma warning restore CS8618

        /*
         * Constructor for Field (gameboard),
         * Only required arguments are the 2 player names,
         * optionally fieldSize, winCondition and timerMax,
         * fieldSize must be >= '3', if smaller the value defaults to '3',
         * winCondition must be <= fieldsize, if higher the value defaults to the value of fieldSize, by default '3'
         * timerMax < '0' means overwriting the Box owner is disabled, by default '-1'
         * timerMax == '0' means overwriting the Box owner is enabled without restriction (even the next turn),
         * timerMax > '0' means overwriting the Box owner is enabled, the intended number of rounds in arg timerMax is multiplied by 2
         * to reach number of turns (1 round has 2 turns)
         * for more info see PlayTurn() and Box.LockTimer.
         */
        public Field(string player1, string player2, byte fieldSize = 3, byte winCondition = 3, sbyte timerMax = -1)
        {
            if (fieldSize < 3)
            {
                //throw new ArgumentException("Filed must be atleast of size '3'!");
                fieldSize = 3;
            }
            if (timerMax > 0)
            {
                timerMax *= 2;
            }
            GameField = new Box[fieldSize, fieldSize];
            for (int rows = 0; rows < fieldSize; rows++)
            {
                for (int columns = 0; columns < fieldSize; columns++)
                {
                    GameField[rows, columns] = new Box(timerMax);
                }
            }
            PlayerCross = player1;
            PlayerCircle = player2;
            TurnCounter = 0;
            TurnLockTimer = timerMax;


            FieldSize = fieldSize;
            WinCondition = winCondition;
            if (winCondition > fieldSize)
            {
                WinCondition = fieldSize;
            }
            OnTurn = State.cross;
        }
        /*
         * Box [,] array with dimensions (FieldSize x FieldSize)
         */
        public Box[,] GameField { get; private set; }

        /*
         * Username of the first player, is first OnTurn.
         */
        public string PlayerCross { get; private set; }

        /*
         * Username of the second player
         */
        public string PlayerCircle { get; private set; }


        /*
         * State enum containing the information, who is going to play the next turn.
         */
        public State OnTurn { get; set; }

        /*
         * Number of played turns.
         */
        public ushort TurnCounter { get; set; }
        /*
         * Number of neigbouring Boxes needed to win a game,
         * can`t have higher value than FieldSize.
         */
        public byte WinCondition { get; private set; }
        /*
         * Size of the game field, minimum is 3.
         */
        public byte FieldSize { get; private set; }
        /*
         * Number of turns the Box.LockTimer will be set.
         * TurnTimerMax '-1' means overwriting the Box owner is disabled,
         * otherwise number of turns before Box.Owner can be overwritten.
         */
        public sbyte TurnLockTimer { get; private set; }

        /*
         * Method that schould be called after round was played,
         * uses 4 private methods to check all 8 directions,
         * params row & column are forwarded to them,
         * return true if any of them find that WinCondition was met,
         * if so calls SwitchOnTurn(),
         * follows CheckDraw().
         */
        public bool CheckWin(int row, int column)
        {
            if (CheckTopLeftToBottomRight(row, column))
            {
                SwitchOnTurn();
                return true;
            }

            if (CheckTopRightToBottomLeft(row, column))
            {
                SwitchOnTurn();
                return true;
            }

            if (CheckTopToBottom(row, column))
            {
                SwitchOnTurn();
                return true;
            }

            if (CheckLeftToRight(row, column))
            {
                SwitchOnTurn();
                return true;
            }
            return CheckDraw();
        }

        /*
         * Method which swiches OnTurn after WinCondition was met during CheckWin().
         */
        private void SwitchOnTurn()
        {
            /*OnTurn contains information about who is playing next, so after winning the game
             * it needs to be switched.
             */
            if (OnTurn == State.circle)
            {
                OnTurn = State.cross;
            }
            else if (OnTurn == State.cross)
            {
                OnTurn = State.circle;
            }
        }

        /*
         * Method which check WinCondition in direction top left to bottom right,
         * form coordinates given throught params row & column tries to move as top left as possible
         * via checking the owners of the Boxes,
         * then counts Boxes from top left to bottom right until it counts WinCondition or reaches empty/opponents`s Box,
         * returns true if WinCondition was met.
         */
        private bool CheckTopLeftToBottomRight(int row, int column)
        {
            int current = 1;

            if (row != 0 && column != 0)
            {
                while (GameField[row, column].Owner == GameField[row - 1, column - 1].Owner)
                {
                    row--;
                    column--;
                    if (row == 0 || column == 0)
                    {
                        break;
                    }
                }
            }
            if (row != FieldSize - 1 && column != FieldSize - 1)
            {
                while (GameField[row, column].Owner == GameField[row + 1, column + 1].Owner)
                {
                    current++;
                    if (current == WinCondition)
                    {
                        return true;
                    }
                    row++;
                    column++;
                    if (row == FieldSize - 1 || column == FieldSize - 1)
                    {
                        break;
                    }
                }
            }
            return false;
        }

        /*
         * Method which check WinCondition in direction top right to bottom left,
         * form coordinates given throught params row & column tries to move as top right as possible
         * via checking the owners of the Boxes,
         * then counts Boxes from top right to bottom left until it counts WinCondition or reaches empty/opponents`s Box,
         * returns true if WinCondition was met.
         */
        private bool CheckTopRightToBottomLeft(int row, int column)
        {
            int current = 1;

            if (row != 0 && column != FieldSize - 1)
            {
                while (GameField[row, column].Owner == GameField[row - 1, column + 1].Owner)
                {
                    row--;
                    column++;
                    if (row == 0 || column == FieldSize - 1)
                    {
                        break;
                    }
                }
            }
            if (row != FieldSize - 1 && column != 0)
            {
                while (GameField[row, column].Owner == GameField[row + 1, column - 1].Owner)
                {
                    current++;
                    if (current == WinCondition)
                    {
                        return true;
                    }
                    row++;
                    column--;
                    if (row == FieldSize - 1 || column == 0)
                    {
                        break;
                    }
                }
            }
            return false;
        }

        /*
         * Method which check WinCondition in direction top left to bottom,
         * form coordinates given throught params row & column tries to move as top as possible
         * via checking the owners of the Boxes,
         * then counts Boxes from top to bottom until it counts WinCondition or reaches empty/opponents`s Box,
         * returns true if WinCondition was met.
         */
        private bool CheckTopToBottom(int row, int column)
        {
            int current = 1;

            if (row != 0)
            {
                while (GameField[row, column].Owner == GameField[row - 1, column].Owner)
                {
                    row--;
                    if (row == 0)
                    {
                        break;
                    }
                }
            }
            if (row != FieldSize - 1)
            {
                while (GameField[row, column].Owner == GameField[row + 1, column].Owner)
                {
                    current++;
                    if (current == WinCondition)
                    {
                        return true;
                    }
                    row++;
                    if (row == FieldSize - 1)
                    {
                        break;
                    }
                }
            }
            return false;
        }

        /*
         * Method which check WinCondition in direction from left to right,
         * form coordinates given throught params row & column tries to move as left as possible
         * via checking the owners of the Boxes,
         * then counts Boxes from left to right until it counts WinCondition or reaches empty/opponents`s Box,
         * returns true if WinCondition was met.
         */
        private bool CheckLeftToRight(int row, int column)
        {
            int current = 1;

            if (column != 0)
            {
                while (GameField[row, column].Owner == GameField[row, column - 1].Owner)
                {
                    column--;
                    if (column == 0)
                    {
                        break;
                    }
                }
            }
            if (column != FieldSize - 1)
            {
                while (GameField[row, column].Owner == GameField[row, column + 1].Owner)
                {
                    current++;
                    if (current == WinCondition)
                    {
                        return true;
                    }
                    column++;
                    if (column == FieldSize - 1)
                    {
                        break;
                    }
                }
            }
            return false;
        }

        /*
         * Method that checks for draw condition,
         * if overwriting is enabled (any form) returns false,
         * if overwritng of Box owner is disabled and all Boxes have
         * their owners set -> returns true, ohterwise returns false,
         * if game is found to be draw, OnTurn is set to Statee.empty.
         */
        private bool CheckDraw()
        {
            if (TurnLockTimer < 0)
            {
                for (int i = 0; i < FieldSize; i++)
                {
                    for (int j = 0; j < FieldSize; j++)
                    {
                        if (GameField[i, j].Owner == State.empty)
                        {
                            return false;
                        }
                    }
                }
                OnTurn = State.empty;
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
         * Method that serves the game logic,
         * increments TurnCounter, calls DecrementLocks() and calls SetOwner() for player OnTurn
         * on the Box on coordinates given throught params row & column,
         * returns char symbolising the player, who was On Turn, and setes OnTurn for next round.
         * 
         * In sequnce: 
         *      sets new owner SetOwner()
         *      increment TurnCounter
         *      decrement LockTimers(),  -> -1 after  both turns , -2 per round
         */
        public char PlayTurn(int row, int column)
        {
            char c;
            if (OnTurn == State.cross)
            {
                GameField[row, column].SetOwner(State.cross);
                c = 'X';
                OnTurn = State.circle;
            }
            else
            {
                GameField[row, column].SetOwner(State.circle);
                c = 'O';
                OnTurn = State.cross;
            }
            TurnCounter++;
            DecrementLocks();
            return c;


        }

        /*
         * Method calling DecrementLock() for all Boxes,
         * used in PlayTurn.
         */
        private void DecrementLocks()
        {
            for (int rows = 0; rows < FieldSize; rows++)
            {
                for (int columns = 0; columns < FieldSize; columns++)
                {
                    GameField[rows, columns].DecrementLock();
                }
            }
        }

        /*
         * Method returning bool [,] array of the same size as FieldSize,
         * true values indicate that the player OnTurn can set himself as owner
         * of particular Box on the same coordinates via SetOwner()
         */
        public bool[,] GetPlayableBoxes()
        {
            bool[,] playable = new bool[FieldSize, FieldSize];
            for (int rows = 0; rows < FieldSize; rows++)
            {
                for (int columns = 0; columns < FieldSize; columns++)
                {
                    if (GameField[rows, columns].Owner == OnTurn)
                    {
                        playable[rows, columns] = false;
                    }
                    else if (GameField[rows, columns].Owner == State.empty)
                    {
                        playable[rows, columns] = true;
                    }
                    else if (GameField[rows, columns].LockTimer == 0)
                    {
                        playable[rows, columns] = true;
                    }
                    else
                    {
                        playable[rows, columns] = false;
                    }

                }
            }
            return playable;
        }

        /*
         *  After the game is finished, the results can be exported to Game object 
         *  and for further processing (adding to HallOfFame & saving to file)
         */
        public Game ExportGame()
        {
            return new Game(PlayerCross, PlayerCircle, OnTurn, TurnCounter, WinCondition, FieldSize);
        }

    }
}

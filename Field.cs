using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class Field
    {
        private Field()
        {
            /*
            GameField = new Box[3, 3];
            for (int rows = 0; rows < 3; rows++)
            {
                for (int columns = 0; columns < 3; columns++)
                {
                    GameField[rows, columns] = new Box();
                }
            }
            */
        }

        public Field(byte fieldSize, string player1, string player2, byte winCondition = 3, sbyte timerMax = -1)
        {
            if (fieldSize < 3)
            {
                throw new ArgumentException("Filed must be atleast of size '3'!");
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

            if (winCondition > fieldSize)
            {
                this.winCondition = fieldSize;
            }
            this.fieldSize = fieldSize;
            this.winCondition = winCondition;
            OnTurn = Box.State.cross;
        }

        public Box[,] GameField { get; private set; }

        public string PlayerCross { get; set; }

        public string PlayerCircle { get; set; }



        public Box.State OnTurn { get; set; }

        public ushort TurnCounter { get; set; }

        private byte winCondition;
        private byte fieldSize;

        public bool CheckWin(int row, int column)
        {
            if (CheckTopLeftToBottomRight(row, column))
            {
                return true;
            }

            if (CheckTopRightToBottomLeft(row, column))
            {
                return true;
            }

            if (CheckTopToBottom(row, column))
            {
                return true;
            }

            if (CheckLeftToRight(row, column))
            {
                return true;
            }

            return false;
        }

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
            if (row != fieldSize - 1 && column != fieldSize - 1)
            {
                while (GameField[row, column].Owner == GameField[row + 1, column + 1].Owner)
                {
                    current++;
                    if (current == winCondition)
                    {
                        return true;
                    }
                    row++;
                    column++;
                    if (row == fieldSize - 1 || column == fieldSize - 1)
                    {
                        break;
                    }
                }
            }
            return false;
        }

        private bool CheckTopRightToBottomLeft(int row, int column)
        {
            int current = 1;

            if (row != 0 && column != fieldSize - 1)
            {
                while (GameField[row, column].Owner == GameField[row - 1, column + 1].Owner)
                {
                    row--;
                    column++;
                    if (row == 0 || column == fieldSize - 1)
                    {
                        break;
                    }
                }
            }
            if (row != fieldSize - 1 && column != 0)
            {
                while (GameField[row, column].Owner == GameField[row + 1, column - 1].Owner)
                {
                    current++;
                    if (current == winCondition)
                    {
                        return true;
                    }
                    row++;
                    column--;
                    if (row == fieldSize - 1 || column == 0)
                    {
                        break;
                    }
                }
            }
            return false;
        }

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
            if (row != fieldSize - 1)
            {
                while (GameField[row, column].Owner == GameField[row + 1, column].Owner)
                {
                    current++;
                    if (current == winCondition)
                    {
                        return true;
                    }
                    row++;
                    if (row == fieldSize - 1)
                    {
                        break;
                    }
                }
            }
            return false;
        }

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
            if (column != fieldSize - 1)
            {
                while (GameField[row, column].Owner == GameField[row, column + 1].Owner)
                {
                    current++;
                    if (current == winCondition)
                    {
                        return true;
                    }
                    column++;
                    if (column == fieldSize - 1)
                    {
                        break;
                    }
                }
            }
            return false;
        }

        public char PlayRound(int row, int column)
        {
            char c;
            if (OnTurn == Box.State.cross)
            {
                GameField[row, column].SetOwner(Box.State.cross);
                c = 'X';
                OnTurn = Box.State.circle;
            }
            else
            {
                GameField[row, column].SetOwner(Box.State.circle);
                c = 'O';
                OnTurn = Box.State.cross;
            }
            TurnCounter++;
            DecrementCounters();
            return c;


        }

        private void DecrementCounters() 
        {
            for (int rows = 0; rows < fieldSize; rows++)
            {
                for (int columns = 0; columns < fieldSize; columns++)
                {
                    GameField[rows, columns].DecrementLock();
                }
            }
        }

        public bool[,] GetPlayableBoxes()
        {
            bool[,] playable = new bool[fieldSize, fieldSize];
            for (int rows = 0; rows < fieldSize; rows++)
            {
                for (int columns = 0; columns < fieldSize; columns++)
                {
                    if (GameField[rows, columns].Owner == OnTurn)
                    {
                        playable[rows, columns] = false;
                    }
                    else if (GameField[rows, columns].Owner == Box.State.empty)
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

    }
}

/*
 *  
 */
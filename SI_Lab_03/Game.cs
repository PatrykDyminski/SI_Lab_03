using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Lab_03
{
    class Game
    {
        public static int P1 = 1;
        public static int P2 = 2;

        IPlayer Player1;
        IPlayer Player2;

        public Game(IPlayer player1, IPlayer player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public void Play()
        {
            bool GameEnd = false;

            int[,] board = Utils.BoardInit();

            while (!GameEnd)
            {
                bool P1success = false;

                while (!P1success)
                {
                    int P1Move = Player1.Move(board);
                    (int[,] newBoard, bool successRet) = ChangeBoard(board, P1Move, P1);
                    P1success = successRet;
                    if (P1success)
                    {
                        board = newBoard;
                    }
                }

                bool P2success = false;

                while (!P2success)
                {
                    int P2Move = Player2.Move(board);
                    (int[,] newBoard, bool successRet) = ChangeBoard(board, P2Move, P2);
                    P2success = successRet;
                    if (P2success)
                    {
                        board = newBoard;
                    }
                }

            }


        }

        private (int[,] board, bool success) ChangeBoard(int[,] board, int col, int player)
        {
            int[,] newBoard = Utils.CopyArray(board);

            bool changed = false;
            bool success = false;
            int row = 5;
            while (!changed)
            {
                if(board[row, col] == 0)
                {
                    newBoard[row, col] = player;
                    changed = true;
                    success = true;
                }
                else
                {
                    if(row-1 < 0)
                    {
                        changed = true;
                    }
                    else
                    {
                        row -= 1;
                    }
                }
            }

            return (newBoard, success);

        }

        

    }
}

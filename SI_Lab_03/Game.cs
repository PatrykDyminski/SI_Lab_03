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
            bool gameEnd = false;

            int[,] board = Utils.BoardInit();

            while (!gameEnd)
            {
                int P1Move = GetPlayerMovement(Player1, board);
                board = ChangeBoard(board, P1Move, P1);

                if(CheckWin(board, P1))
                {
                    gameEnd = true;
                    Console.WriteLine("Wygrałeś " + P1);
                    Utils.PrintBoard(board);
                    break;
                }

                int P2Move = GetPlayerMovement(Player2, board);
                board = ChangeBoard(board, P2Move, P2);

                if (CheckWin(board, P2))
                {
                    gameEnd = true;
                    Console.WriteLine("Wygrałeś " + P2);
                    Utils.PrintBoard(board);
                }
            }
        }

        //zwraca legalny ruch gracza
        private int GetPlayerMovement(IPlayer player, int[,] board)
        {
            bool success = false;
            int move = -1;

            while (!success)
            {
                move = player.Move(board);
                success = ValidateMove(move, board);
                if (!success)
                {
                    Console.WriteLine("Ruch nielegalny! Podaj nowy wybór");
                }
            }

            return move;
        }

        //walidacja ruchu gracza
        private bool ValidateMove(int move, int[,] board)
        {
            return (board[0, move] != 0) ? false : true;
        }

        //umieszczenie poprawnego ruchu na planszy
        private int[,] ChangeBoard(int[,] board, int col, int player)
        {
            int[,] newBoard = Utils.CopyArray(board);

            bool changed = false;
            int row = board.GetLength(0)-1;
            while (!changed)
            {
                if (board[row, col] == 0)
                {
                    newBoard[row, col] = player;
                    changed = true;
                }
                else
                { 
                    row -= 1;
                }
            }

            return newBoard;
        }

        private bool CheckSlice(int[] slice, int player)
        {
            for(int i = 0; i < slice.Length; i++)
            {
                if(slice[i] != player)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckWin(int[,] board, int player)
        {
            return  CheckHorizontal(board, player)  ||
                    CheckVertical(board, player)    ||
                    CheckDiagonal1(board, player)   || 
                    CheckDiagonal2(board, player);
        }

        private bool CheckHorizontal(int[,] board, int player)
        {
            bool win;

            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int j = 0; j < board.GetLength(1) - 4 + 1; j++)
                {
                    int[] slice = new int[4];

                    for(int k = 0; k < 4; k++)
                    {
                        slice[k] = board[i,j+k];
                    }

                    win = CheckSlice(slice, player);

                    if (win)
                    {
                        return true;
                    }                    
                }
            }

            return false;
        }

        private bool CheckVertical(int[,] board, int player)
        {
            bool win;

            for (int i = 0; i < board.GetLength(0) - 4 + 1; i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    int[] slice = new int[4];

                    for (int k = 0; k < 4; k++)
                    {
                        slice[k] = board[i+k, j];
                    }

                    win = CheckSlice(slice, player);

                    if (win)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckDiagonal1(int[,] board, int player)
        {
            bool win;

            for (int i = 0; i < board.GetLength(0) - 4 + 1; i++)
            {
                for (int j = 0; j < board.GetLength(1) - 4 + 1; j++)
                {
                    int[] slice = new int[4];

                    for (int k = 0; k < 4; k++)
                    {
                        slice[k] = board[i + k, j + k];
                    }

                    win = CheckSlice(slice, player);

                    if (win)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckDiagonal2(int[,] board, int player)
        {
            bool win;

            for (int i = 0; i < board.GetLength(0) - 4 + 1; i++)
            {
                for (int j = 0; j < board.GetLength(1) - 4 + 1; j++)
                {
                    int[] slice = new int[4];

                    for (int k = 0; k < 4; k++)
                    {
                        slice[k] = board[i + 3 - k, j + k];
                    }

                    win = CheckSlice(slice, player);

                    if (win)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

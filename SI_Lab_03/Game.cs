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
                int P1Move = GetPlayerMovement(Player1, board);
                board = ChangeBoard(board, P1Move, P1);

                int P2Move = GetPlayerMovement(Player2, board);
                board = ChangeBoard(board, P2Move, P2);
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

    }
}

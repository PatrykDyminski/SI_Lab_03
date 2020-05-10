using System;

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
                board = Utils.ChangeBoard(board, P1Move, P1);

                Utils.PrintBoard(board);

                if(Utils.CheckWin(board, P1))
                {
                    gameEnd = true;
                    Console.WriteLine("Wygrałeś " + P1);
                    Utils.PrintBoard(board);
                    break;
                }

                if (Utils.CheckBoardFull(board))
                {
                    gameEnd = true;
                    Console.WriteLine("Remins !!!one one");
                    Utils.PrintBoard(board);
                    break;
                }

                int P2Move = GetPlayerMovement(Player2, board);
                board = Utils.ChangeBoard(board, P2Move, P2);

                Utils.PrintBoard(board);

                if (Utils.CheckWin(board, P2))
                {
                    gameEnd = true;
                    Console.WriteLine("Wygrałeś " + P2);
                    Utils.PrintBoard(board);
                }

                if (Utils.CheckBoardFull(board))
                {
                    gameEnd = true;
                    Console.WriteLine("Remins !!!one one");
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
                success = Utils.ValidateMove(move, board);
                if (!success)
                {
                    Console.WriteLine("Ruch nielegalny! Podaj nowy wybór");
                }
            }

            return move;
        }

    }
}

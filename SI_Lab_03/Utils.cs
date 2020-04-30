using System;

namespace SI_Lab_03
{
    class Utils
    {
        public static void PrintBoard(int[,] board)
        {
            Console.WriteLine(". 1 . 2 . 3 . 4 . 5 . 6 . 7 .");
            Console.WriteLine(". _ . _ . _ . _ . _ . _ . _ .");

            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write("| ");

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i,j] == 0)
                    {
                        Console.Write("  | ");
                    }
                    else
                    {
                        Console.Write(board[i, j] + " | ");
                    }
                }

                Console.WriteLine();
                Console.WriteLine(". _ . _ . _ . _ . _ . _ . _ ."); 
            }
        }

        public static int[,] BoardInit()
        {
            return new int[6, 7]
            {
                { 0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0 }
            };
        }

        //walidacja ruchu gracza
        public static bool ValidateMove(int move, int[,] board)
        {
            return (board[0, move] != 0) ? false : true;
        }

        //umieszczenie poprawnego ruchu na planszy
        public static int[,] ChangeBoard(int[,] board, int col, int player)
        {
            int[,] newBoard = Utils.CopyArray(board);

            bool changed = false;
            int row = board.GetLength(0) - 1;
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

        public static bool CheckBoardFull(int[,] board)
        {
            int ix = board.GetLength(1);
            
            for(int i = 0; i < ix; i++)
            {
                if(board[0,i] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CheckSlice(int[] slice, int player)
        {
            for (int i = 0; i < slice.Length; i++)
            {
                if (slice[i] != player)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CheckWin(int[,] board, int player)
        {
            return CheckHorizontal(board, player) ||
                    CheckVertical(board, player) ||
                    CheckDiagonal1(board, player) ||
                    CheckDiagonal2(board, player);
        }

        public static bool CheckHorizontal(int[,] board, int player)
        {
            bool win;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1) - 4 + 1; j++)
                {
                    int[] slice = new int[4];

                    for (int k = 0; k < 4; k++)
                    {
                        slice[k] = board[i, j + k];
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

        public static bool CheckVertical(int[,] board, int player)
        {
            bool win;

            for (int i = 0; i < board.GetLength(0) - 4 + 1; i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    int[] slice = new int[4];

                    for (int k = 0; k < 4; k++)
                    {
                        slice[k] = board[i + k, j];
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

        public static bool CheckDiagonal1(int[,] board, int player)
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

        public static bool CheckDiagonal2(int[,] board, int player)
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

        public static int OtherPlayer(int player)
        {
            return (player == 1) ? 2 : 1;
        }

        public static int[,] CopyArray(int[,] source)
        {
            int[,] result = new int[source.GetLength(0),source.GetLength(1)];

            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    result[i,j] = source[i,j];
                }
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Lab_03
{
    class Utils
    {
        public static void PrintBoard(int[,] board)
        {
            Console.WriteLine(". 1 . 2 . 3 . 4 . 5 . 6 . 7 .");
            Console.WriteLine(". - . - . - . - . - . - . - .");

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
                Console.WriteLine(". - . - . - . - . - . - . - ."); 
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

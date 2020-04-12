using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Lab_03
{
    class PlayerHuman : IPlayer
    {
        public int Move(int[,] board)
        {
            Utils.PrintBoard(board);

            bool valid = false;

            int move = 0;

            while (!valid)
            {
                Console.WriteLine("Podaj liczbę 1-7");
                string input = Console.ReadLine();
                int number;
                bool success = int.TryParse(input, out number);

                if(success&& number>0 && number < 8)
                {
                    valid = true;
                    move = number;
                }
            }

            return move-1;
        }
    }
}

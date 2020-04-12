using System;

namespace SI_Lab_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Game!");

            int[,] arr = new int[6, 7]
            {
                { 1,2,3,4,5,6,7 },
                { 1,2,3,4,5,6,7 },
                { 1,2,3,4,5,6,7 },
                { 1,2,3,4,5,6,7 },
                { 1,2,3,4,5,6,7 },
                { 1,2,3,4,5,6,7 }
            };

            //Utils.PrintBoard(arr);

            PlayerHuman human = new PlayerHuman();
            PlayerHuman human2 = new PlayerHuman();

            Game gra = new Game(human, human2);

            gra.Play();


            //int move = human.Move(arr);

            //Console.WriteLine(move);


        }
    }
}

using System;

namespace SI_Lab_03
{
    class Program
    {
        static void Main(string[] args)
        {

            int[,] arr = new int[6, 7]
            {
                { 0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0 },
                { 0,0,0,1,1,0,0 },
                { 0,0,1,1,1,2,2 },
                { 0,0,1,2,1,1,2 }
            };


            PlayerHuman human = new PlayerHuman();
            PlayerHuman human2 = new PlayerHuman();
            PlayerAI ai = new PlayerAI(1, 5);
            PlayerAI ai2 = new PlayerAI(2, 5);

            //var res = ai.Move(arr);
            //Console.WriteLine(res);

            Game gra = new Game(ai, ai2);
            gra.Play();



        }
    }
}

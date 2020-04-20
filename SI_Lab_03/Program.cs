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
                { 0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,2 },
                { 0,0,0,1,1,1,2 }
            };


            PlayerHuman human = new PlayerHuman();
            PlayerHuman human2 = new PlayerHuman();
            PlayerAI ai = new PlayerAI(2);

            var res = ai.Move(arr);

            Console.WriteLine(res);

            //Game gra = new Game(human, ai);
            //gra.Play();


        }
    }
}

using SI_Lab_03.AIPlayers;
using SI_Lab_03.ScoringFunctions;
using System;
using System.IO;
using System.Text;

namespace SI_Lab_03
{
    class Program
    {
        static void Main(string[] args)
        {

            int[,] arr = new int[6, 7]
            {
                { 0,0,1,0,0,0,0 },
                { 0,0,0,0,0,0,0 },
                { 1,1,0,0,1,0,0 },
                { 0,0,0,0,0,1,0 },
                { 0,0,1,0,0,0,0 },
                { 0,0,1,0,0,0,0 }
            };


            //PlayerHuman human = new PlayerHuman();
            //PlayerHuman human2 = new PlayerHuman();

            //NaiveScore sb = new NaiveScore();

            //AIPlayer ai = new MiniMaxPlayer(1, 5, sb, true);
            //AIPlayer ai2 = new MiniMaxPlayer(2, 5, sb, true);

            //AIPlayer ai3 = new AlphaBetaPlayer(1, 10, sb, true);
            //AIPlayer ai4 = new AlphaBetaPlayer(2, 8, sb, true);

            ////var res = ai.Move(arr);
            ////Console.WriteLine(res);

            //Game gra = new Game(ai3, ai4);
            //gra.Play();

            //ai3.GetAvgTime();

            RunnerAI();

            //OtherScore os = new OtherScore();
            //int score = os.Score(arr, 1);
            //Console.WriteLine(score);

        }

        static void RunnerAI()
        {
            var csv = new StringBuilder();

            PlayerHuman human = new PlayerHuman();
            PlayerHuman human2 = new PlayerHuman();

            NaiveScore sb = new NaiveScore();
            OtherScore os = new OtherScore();

            IScoreBoard sf = os;

            AIPlayer ai1 = new MiniMaxPlayer(1, 5, sf, true);
            AIPlayer ai2 = new MiniMaxPlayer(2, 5, sf, true);

            AIPlayer ai3 = new AlphaBetaPlayer(1, 6, sf, true);
            AIPlayer ai4 = new AlphaBetaPlayer(2, 6, sf, true);

            var p1 = ai3;
            var p2 = ai4;

            Game gra = new Game(p1, p2);
            gra.Play();

            var p1avg = p1.GetAvgTime();
            var p1all = p1.GetAllTimes();

            Console.WriteLine(p1avg);

            var p2avg = p2.GetAvgTime();
            var p2all = p2.GetAllTimes();

            Console.WriteLine(p2avg);

            var firstL = "";
            var secondL = "";

            foreach(TimeSpan time in p1all)
            {
                //Console.WriteLine(string.Format("{0:0}:{1:00}:{2:000},", time.Minutes, time.Seconds, time.Milliseconds));
                //firstL += string.Format("{0:0}:{1:00}:{2:000};", time.Minutes, time.Seconds, time.Milliseconds);

                firstL += string.Format("{0};", time.Milliseconds);
            }

            foreach (TimeSpan time in p2all)
            {
                //secondL += string.Format("{0:0}:{1:00}:{2:000};", time.Minutes, time.Seconds, time.Milliseconds);

                secondL += string.Format("{0};", time.Milliseconds);
            }

            firstL += ";";
            secondL += ";";

            csv.AppendLine(firstL);
            csv.AppendLine(secondL);

            File.WriteAllText("results.csv", csv.ToString());
        }
    }
}

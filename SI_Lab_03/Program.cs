using SI_Lab_03.AIPlayers;
using SI_Lab_03.ScoringFunctions;
using System;
using System.Collections.Generic;
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

            //RunnerAI();

            RunnerLogger();

            //OtherScore os = new OtherScore();
            //int score = os.Score(arr, 1);
            //Console.WriteLine(score);

        }

        static void RunnerLogger()
        {
            var csv = new StringBuilder();

            string start = "description;winner;depth;heur;avgInSec;avgInmili;moves;time";
            csv.AppendLine(start);

            NaiveScore sb = new NaiveScore();
            OtherScore os = new OtherScore();
            BalancedScore bs = new BalancedScore();

            Console.WriteLine("Start");

            List<IScoreBoard> heurs = new List<IScoreBoard>()
            {
                sb,os,bs
            };

            List<IScoreBoard> heurs2 = new List<IScoreBoard>()
            {
                sb,os,bs
            };

            foreach (var heur1 in heurs)
            {
                IScoreBoard h1 = heur1;

                foreach (var heur2 in heurs2)
                {
                    IScoreBoard h2 = heur2;

                    Console.WriteLine("H1: {0} H2: {1}", h1.GetName(), h2.GetName());

                    for (int i = 2; i < 7; i++)
                    {
                        for (int j = 2; j < 9; j++)
                        {
                            Console.WriteLine("P1:{0} - {1} P2: {2} - {3}", h1.GetName(), i, h2.GetName(), j);

                            AIPlayer ai1 = new MiniMaxPlayer(1, i, h1, true);
                            AIPlayer ai2 = new AlphaBetaPlayer(2, j, h2, true);

                            Game gra = new Game(ai1, ai2);
                            int winningP = gra.Play();

                            var line = "";

                            line += string.Format("P1:{0} - {1} P2: {2} - {3};", h1.GetName(), i, h2.GetName(), j);

                            if (winningP == 1)
                            {
                                line += string.Format("{0};{1};{2};", 1, i, h1.GetName());

                                var avg = ai1.GetAvgTime();
                                var times = ai1.GetAllTimes();

                                //foreach (TimeSpan time in times)
                                //{
                                //    line += string.Format("{0};", time.Milliseconds);
                                //}

                                line += string.Format("{0};", avg.TotalSeconds);
                                line += string.Format("{0};", avg.TotalMilliseconds);
                                line += string.Format("{0};", times.Count);
                                line += string.Format("{0:00}:{1:000};", avg.Seconds, avg.Milliseconds);

                                csv.AppendLine(line);
                            }
                            else if (winningP == 2)
                            {
                                line += string.Format("{0};{1};{2};", 2, j, h2.GetName());

                                var avg = ai2.GetAvgTime();
                                var times = ai2.GetAllTimes();

                                //foreach (TimeSpan time in times)
                                //{
                                //    line += string.Format("{0};", time.Milliseconds);
                                //}

                                line += string.Format("{0};", avg.TotalSeconds);
                                line += string.Format("{0};", avg.TotalMilliseconds);
                                line += string.Format("{0};", times.Count);
                                line += string.Format("{0:00}:{1:000};", avg.Seconds, avg.Milliseconds);

                                csv.AppendLine(line);
                            }
                            else
                            {
                                line += string.Format("{0};{1};{2};", 0, i, h1.GetName());

                                var avg = ai1.GetAvgTime();
                                var times = ai1.GetAllTimes();

                                //foreach (TimeSpan time in times)
                                //{
                                //    line += string.Format("{0};", time.Milliseconds);
                                //}
                                line += string.Format("{0};", avg.TotalSeconds);
                                line += string.Format("{0};", avg.TotalMilliseconds);
                                line += string.Format("{0};", times.Count);
                                line += string.Format("{0:00}:{1:000};", avg.Seconds, avg.Milliseconds);

                                csv.AppendLine(line);
                            }
                        }
                    }
                }
            }

           
            Console.WriteLine("ukończono");

            File.WriteAllText("results.csv", csv.ToString());
        }

        static void RunnerAI()
        {
            var csv = new StringBuilder();

            PlayerHuman human = new PlayerHuman();
            PlayerHuman human2 = new PlayerHuman();

            NaiveScore sb = new NaiveScore();
            OtherScore os = new OtherScore();
            BalancedScore bs = new BalancedScore();

            IScoreBoard sf = bs;

            AIPlayer ai1 = new MiniMaxPlayer(1, 5, sf, true);
            AIPlayer ai2 = new MiniMaxPlayer(2, 5, sf, true);

            AIPlayer ai3 = new AlphaBetaPlayer(1, 9, sf, true);
            AIPlayer ai4 = new AlphaBetaPlayer(2, 9, sf, true);

            var p1 = ai3;
            var p2 = ai4;

            Game gra = new Game(p1, p2);
            int winningP = gra.Play();

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

            firstL += string.Format(";{0};", p1avg.Ticks);

            foreach (TimeSpan time in p2all)
            {
                //secondL += string.Format("{0:0}:{1:00}:{2:000};", time.Minutes, time.Seconds, time.Milliseconds);

                secondL += string.Format("{0};", time.Milliseconds);
            }

            secondL += string.Format(";{0};", p2avg.Ticks);

            firstL += ";";
            secondL += ";";

            if(winningP == 1)
            {
                csv.AppendLine(firstL);
            }
            else if(winningP == 2)
            {
                csv.AppendLine(secondL);
            }
            else
            {
                csv.AppendLine(firstL);
                csv.AppendLine(secondL);
            }

            File.WriteAllText("results.csv", csv.ToString());
        }
    }
}

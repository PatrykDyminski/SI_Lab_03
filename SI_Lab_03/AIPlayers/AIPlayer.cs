using SI_Lab_03.ScoringFunctions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SI_Lab_03.AIPlayers
{
    abstract class AIPlayer : IPlayer
    {
        protected int Player;
        protected int Depth;
        protected bool FirstMoveMade = false;
        protected IScoreBoard Sb;
        protected bool FirstMoveRandom;
        protected List<TimeSpan> times;

        public AIPlayer(int player, int depth, IScoreBoard sb, bool firstMoveRandom)
        {
            Player = player;
            Depth = depth;
            Sb = sb;
            FirstMoveRandom = firstMoveRandom;
            //times = new List<TimeSpan>();
        }

        public abstract int Move(int[,] board);

        protected bool CheckIfFirstMove(int[,] board)
        {
            if (!FirstMoveMade)
            {
                int ix = board.GetLength(1);
                int iy = board.GetLength(0) - 1;

                for (int i = 0; i < ix; i++)
                {
                    if (board[iy, i] != 0)
                    {
                        FirstMoveMade = true;
                        return false;
                    }
                }

                FirstMoveMade = true;
                return true;
            }
            else
            {
                FirstMoveMade = true;
                return false;
            }
        }

        protected List<int> GetValidMoves(int[,] board)
        {
            List<int> moves = new List<int>();

            for (int i = 0; i < board.GetLength(1); i++)
            {
                if (board[0, i] == 0)
                {
                    moves.Add(i);
                }
            }

            return moves;
        }

        protected int GetRandomMove()
        {
            Random rnd = new Random();
            return rnd.Next(6);
        }

        public TimeSpan GetAvgTime()
        {
            double doubleAverageTicks = times.Average(timeSpan => timeSpan.Ticks);
            long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

            //foreach (var time in times)
            //{
            //    Console.WriteLine(String.Format("{0:0}:{1:00}:{2:000}", time.Minutes, time.Seconds, time.Milliseconds));
            //}

            Console.WriteLine();

            var avg = new TimeSpan(longAverageTicks);

            //Console.WriteLine("Średio: ");
            //Console.WriteLine(String.Format("{0:0}:{1:00}:{2:000}", avg.Minutes, avg.Seconds, avg.Milliseconds));

            return avg;
        }

        public List<TimeSpan> GetAllTimes()
        {
            return times;
        }
    }
}

using SI_Lab_03.ScoringFunctions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SI_Lab_03.AIPlayers
{
    class AlphaBetaPlayer : AIPlayer
    {
        public AlphaBetaPlayer(int player, int depth, IScoreBoard sb, bool firstMoveRandom) : base(player, depth, sb, firstMoveRandom)
        {
            Player = player;
            Depth = depth;
            Sb = sb;
            FirstMoveRandom = firstMoveRandom;
        }

        public override int Move(int[,] board)
        {
            Stopwatch timer = Stopwatch.StartNew();

            int ret;

            if (CheckIfFirstMove(board) && FirstMoveRandom)
            {
                ret = GetRandomMove();
            }
            else
            {
                int alpha = int.MinValue;
                int beta = int.MaxValue;

                ret = AlphaBeta(board, Player, Depth, false, true, alpha, beta);
            }

            timer.Stop();
            TimeSpan timespan = timer.Elapsed;
            times.Add(timespan);

            return ret;
        }

        private int AlphaBeta(int[,] board, int nextPlayer, int depth, bool mini, bool init, int alpha, int beta)
        {
            if (depth == 0) return Sb.Score(board, Player);

            List<int> moves = GetValidMoves(board);

            if (moves.Count == 0) return Sb.Score(board, Player);

            Dictionary<int, int> results = new Dictionary<int, int>();

            int newAlpha = alpha;
            int newBeta = beta;

            for (int i = 0; i < moves.Count; i++)
            {
                int[,] newBoard = Utils.ChangeBoard(board, moves[i], nextPlayer);

                var result = AlphaBeta(newBoard, Utils.OtherPlayer(nextPlayer), depth - 1, !mini, false, newAlpha, newBeta);
                results.Add(moves[i], result);

                //alphaBeta part
                if (mini)
                {
                    newBeta = result < newBeta ? result : newBeta;

                    if (newBeta <= newAlpha)
                    {
                        break;
                    }

                }
                else
                {
                    newAlpha = result < newAlpha ? newAlpha : result;

                    if (newBeta <= newAlpha)
                    {
                        break;
                    }
                }
            }

            if (init)
            {
                int maxValue = results.Values.Max();
                var result = results.FirstOrDefault(x => x.Value == maxValue);
                return result.Key;
            }

            if (mini)
            {
                int minValue = results.Values.Min();
                return minValue;
            }
            else
            {
                int maxValue = results.Values.Max();
                return maxValue;
            }
        }
    }
}

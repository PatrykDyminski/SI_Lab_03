using SI_Lab_03.ScoringFunctions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SI_Lab_03.AIPlayers
{
    class MiniMaxPlayer : AIPlayer
    {
        public MiniMaxPlayer(int player, int depth, IScoreBoard sb, bool firstMoveRandom) : base(player, depth, sb, firstMoveRandom)
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
                ret = MiniMax(board, Player, Depth, false, true);
            }

            timer.Stop();
            TimeSpan timespan = timer.Elapsed;
            times.Add(timespan);

            return ret;
        }

        private int MiniMax(int[,] board, int nextPlayer, int depth, bool mini, bool init)
        {
            if (depth == 0) return Sb.Score(board, Player);

            List<int> moves = GetValidMoves(board);

            if (moves.Count == 0) return Sb.Score(board, Player);

            Dictionary<int, int> results = new Dictionary<int, int>();

            for (int i = 0; i < moves.Count; i++)
            {
                int[,] newBoard = Utils.ChangeBoard(board, moves[i], nextPlayer);

                var result = MiniMax(newBoard, Utils.OtherPlayer(nextPlayer), depth - 1, !mini, false);
                results.Add(moves[i], result);
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

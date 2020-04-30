using SI_Lab_03.ScoringFunctions;
using System.Collections.Generic;
using System.Linq;

namespace SI_Lab_03.AIPlayers
{
    class MiniMaxPlayer : AIPlayer
    {
        public MiniMaxPlayer(int player, int depth, IScoreBoard sb) : base(player, depth, sb)
        {
            Player = player;
            Depth = depth;
            Sb = sb;
        }

        public override int Move(int[,] board)
        {
            if (CheckIfFirstMove(board))
            {
                return 4;
            }
            else
            {
                var ret = MiniMax(board, Player, Depth, false, true);
                return ret;
            }
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

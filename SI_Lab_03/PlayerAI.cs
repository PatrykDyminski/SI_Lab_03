using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_Lab_03
{
    class PlayerAI : IPlayer
    {
        private int Player { get; }
        private int Depth { get; }

        public PlayerAI(int player, int depth)
        {
            Player = player;
            Depth = depth;
        }

        public int Move(int[,] board)
        {
            var ret = MiniMax(board, Player, -1, Depth, false, true);
            return ret;
        }

        private List<int> GetValidMoves(int[,] board)
        {
            List<int> moves = new List<int>();

            for(int i = 0; i < board.GetLength(1); i++)
            {
                if(board[0, i] == 0)
                {
                    moves.Add(i);
                }
            }

            return moves;
        }

        private int ScoreBoard(int[,] board)
        {
            if (Utils.CheckWin(board, Player))
            {
                if(Utils.CheckWin(board, Utils.OtherPlayer(Player)))
                {
                    return -1;
                }

                return 1;
            }
            else if(Utils.CheckWin(board, Utils.OtherPlayer(Player)))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        private int MiniMax(int[,] board, int nextPlayer, int move, int depth, bool mini, bool init)
        {
            if (depth == 0)
            {
                var score = ScoreBoard(board);
                //Console.WriteLine(score);
                //Utils.PrintBoard(board);
                return score;
            }

            List<int> moves = GetValidMoves(board);

            //Console.WriteLine("Dobrych ruchów: " + moves.Count);

            Dictionary<int, int> results = new Dictionary<int, int>();

            for (int i = 0; i < moves.Count; i++)
            {
                int[,] newBoard = Utils.ChangeBoard(board, moves[i], nextPlayer);

                int newDepth = depth - 1;
                bool nextMiniOrMax = !mini;

                var result = MiniMax(newBoard, Utils.OtherPlayer(nextPlayer), moves[i], newDepth, nextMiniOrMax, false);
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

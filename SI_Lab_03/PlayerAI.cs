using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_Lab_03
{
    class PlayerAI : IPlayer
    {
        private int Player { get; }

        public PlayerAI(int player)
        {
            Player = player;
        }

        public int Move(int[,] board)
        {
            //Random rnd = new Random();
            //return rnd.Next(7);

            var depth = 2;

            var ret = MiniMax(board, Player, -1, depth, false, true);
            return ret.move;
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

        private int ScoreBoard(int[,] board, int player)
        {
            if (Utils.CheckWin(board, player))
            {
                return 1;
            }
            else if(Utils.CheckWin(board, Utils.OtherPlayer(player)))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        private (int move, int score) MiniMax(int[,] board, int nextPlayer, int move, int depth, bool mini, bool init)
        {
            if (depth == 0)
            {
                var score = ScoreBoard(board, Player);
                //Console.WriteLine(score);
                //Utils.PrintBoard(board);
                return (move, score);
            }

            //Console.WriteLine(depth);

            List<int> moves = GetValidMoves(board);

            //Console.WriteLine("Dobrych ruchów: " + moves.Count);

            Dictionary<int, int> results = new Dictionary<int, int>();

            for (int i = 0; i < moves.Count; i++)
            {
                int[,] newBoard = Utils.ChangeBoard(board, moves[i], nextPlayer);

                int newDepth = depth - 1;
                bool nextMiniOrMax = !mini;

                var result = MiniMax(newBoard, Utils.OtherPlayer(nextPlayer), moves[i], newDepth, nextMiniOrMax, false);
                results.Add(moves[i], result.score);
            }

            if (init)
            {
                int maxValue = results.Values.Max();
                var result = results.FirstOrDefault(x => x.Value == maxValue);
                return (result.Key, result.Value);
            }

            if (mini)
            {
                int minValue = results.Values.Min();
                return (move, minValue);
            }
            else
            {
                int maxValue = results.Values.Max();
                return (move, maxValue);
            }
        }
    }
}

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

        private bool FirstMoveMade = false;

        public PlayerAI(int player, int depth)
        {
            Player = player;
            Depth = depth;
        }

        public int Move(int[,] board)
        {
            if (CheckIfFirstMove(board))
            {
                return 5;
            }
            else
            {
                var ret = MiniMax(board, Player, Depth, false, true);
                return ret;
            }

        }

        private bool CheckIfFirstMove(int[,] board)
        {
            if (!FirstMoveMade)
            {
                Console.WriteLine("sprawdzam pierwszy ruch");

                int ix = board.GetLength(1);
                int iy = board.GetLength(0) - 1;

                for (int i = 0; i < ix; i++)
                {
                    if(board[iy, i] != 0)
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
            
            if(Utils.CheckWin(board, Utils.OtherPlayer(Player)))
            {
                return -1;
            }
            else if (Utils.CheckWin(board, Player))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private int MiniMax(int[,] board, int nextPlayer, int depth, bool mini, bool init)
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

                var result = MiniMax(newBoard, Utils.OtherPlayer(nextPlayer), newDepth, nextMiniOrMax, false);
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

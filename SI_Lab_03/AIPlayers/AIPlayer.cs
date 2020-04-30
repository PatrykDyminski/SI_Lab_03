using SI_Lab_03.ScoringFunctions;
using System.Collections.Generic;

namespace SI_Lab_03.AIPlayers
{
    abstract class AIPlayer : IPlayer
    {
        protected int Player;
        protected int Depth;
        protected bool FirstMoveMade = false;
        protected IScoreBoard Sb;

        public AIPlayer(int player, int depth, IScoreBoard sb)
        {
            Player = player;
            Depth = depth;
            Sb = sb;
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
    }
}

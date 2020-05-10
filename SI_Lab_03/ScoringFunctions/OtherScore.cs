using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_Lab_03.ScoringFunctions
{
    class OtherScore : IScoreBoard
    {

        Dictionary<string, int> p1Patterns = new Dictionary<string, int>()
        {
            { "1111", 5 },
            { "1110", 5 },
            { "0111", 5 },
            { "1101", 5 },
            { "1011", 5 },
            { "0110", 5 },
            { "011",  5 },
            { "110",  5 },
        };

        Dictionary<string, int> p2Patterns = new Dictionary<string, int>()
        {

        };

        public int Score(int[,] board, int player)
        {
            return ScoreHorizontal(board, player) +
                    ScoreVertical(board, player) +
                    ScoreDiagonal1(board, player) +
                    ScoreDiagonal2(board, player);
        }

        private static int ScoreLine(int[] row, string v)
        {
            throw new NotImplementedException();
        }

        public static int ScoreHorizontal(int[,] board, int player)
        {
            int score = 0;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                int[] row = GetRow(board, i);

                score += ScoreLine(row, "row");
            }

            return score;
        }

        public static int ScoreVertical(int[,] board, int player)
        {
            int score = 0;

            for (int i = 0; i < board.GetLength(1); i++)
            {
                int[] row = GetCol(board, i);

                score += ScoreLine(row, "col");
            }

            return score;
        }

        public static int ScoreDiagonal1(int[,] board, int player)
        {
            bool win;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < board.GetLength(1) - 4 + 1; j++)
                {
                    int[] slice = new int[4];

                    for (int k = 0; k < 4; k++)
                    {
                        slice[k] = board[i + k, j + k];
                    }
                }
            }

            return 0;
        }

        public static int[] GetSlice(int[,] board, int startRow, int startCol, int len)
        {
            int[] slice = new int[len];

            for (int k = 0; k <= len; k++)
            {
                slice[k] = board[startRow + k, startCol + k];
            }

            return slice;
        }

        public static int ScoreDiagonal2(int[,] board, int player)
        {
            bool win;

            for (int i = 0; i < board.GetLength(0) - 4 + 1; i++)
            {
                for (int j = 0; j < board.GetLength(1) - 4 + 1; j++)
                {
                    int[] slice = new int[4];

                    for (int k = 0; k < 4; k++)
                    {
                        slice[k] = board[i + 3 - k, j + k];
                    }

                    //win = CheckSlice(slice, player);

                    //if (win)
                    //{
                    //    return 1;
                    //}
                }
            }

            return 0;
        }

        public static int[] GetRow(int[,] matrix, int row)
        {
            var rowLength = matrix.GetLength(1);
            var rowVector = new int[rowLength];

            for (var i = 0; i < rowLength; i++)
                rowVector[i] = matrix[row, i];

            return rowVector;
        }

        public static int[] GetCol(int[,] matrix, int col)
        {
            var colLength = matrix.GetLength(0);
            var colVector = new int[colLength];

            for (var i = 0; i < colLength; i++)
                colVector[i] = matrix[i, col];

            return colVector;
        }

    }
}

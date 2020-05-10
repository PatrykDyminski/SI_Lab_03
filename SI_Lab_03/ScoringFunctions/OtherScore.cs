using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_Lab_03.ScoringFunctions
{
    class OtherScore : IScoreBoard
    {
        //p1
        static Dictionary<string, int> p1row = new Dictionary<string, int>()
        {
            { "1111", 50000 },
            { "1110", 50 },
            { "0111", 50 },
            { "1101", 50 },
            { "1011", 50 },
            { "0101", 20 },
            { "1010", 20 },
            { "0110", 20 },
            { "011",  5 },
            { "110",  5 },
        };

        static Dictionary<string, int> p1col = new Dictionary<string, int>()
        {
            { "1111", 50000 },
            { "0111", 50 },
            { "011",  5 },
        };

        static Dictionary<string, int> p1diag = new Dictionary<string, int>()
        {
            { "1111", 50000 },
            { "1110", 50 },
            { "0111", 50 },
            { "1101", 50 },
            { "1011", 50 },
            { "0101", 20 },
            { "1010", 20 },
            { "0110", 20 },
            { "110",  5 },
            { "011",  5 },
        };


        //p2
        static Dictionary<string, int> p2row = new Dictionary<string, int>()
        {
            { "2222", 50000 },
            { "2220", 50 },
            { "0222", 50 },
            { "2202", 50 },
            { "2022", 50 },
            { "0202", 20 },
            { "2020", 20 },
            { "020", 20 },
            { "022",  5 },
            { "220",  5 }
        };

        static Dictionary<string, int> p2col = new Dictionary<string, int>()
        {
            { "2222", 50000 },
            { "0222", 50 },
            { "022",  5 },
        };

        static Dictionary<string, int> p2diag = new Dictionary<string, int>()
        {
            { "2222", 50000 },
            { "2220", 50 },
            { "0222", 50 },
            { "2202", 50 },
            { "2022", 50 },
            { "0202", 20 },
            { "2020", 20 },
            { "020", 20 },
            { "022",  5 },
            { "220",  5 }
        };

        public int Score(int[,] board, int player)
        {
            return ScoreHorizontal(board, player) +
                    ScoreVertical(board, player) +
                    ScoreDiagonal1(board, player) +
                    ScoreDiagonal2(board, player)
                    ;
        }

        private static int ScoreLine(int[] line, string type, int player)
        {
            Dictionary<string, int> dict;

            string arrStr = string.Join("", line);

            int score = 0;

            if (type.Equals("row"))
            {
                dict = (player == 1) ? p1row : p2row; 
            }
            else if (type.Equals("col"))
            {
                dict = (player == 1) ? p1col : p2col;
            }
            else
            {
                dict = (player == 1) ? p1diag : p2diag;
            }

            foreach((string key, int value) in dict)
            {
                if (arrStr.Contains(key))
                {
                    score += value;
                }
            }

            return score;
        }

        public static int ScoreHorizontal(int[,] board, int player)
        {
            int score = 0;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                int[] row = GetRow(board, i);

                score += ScoreLine(row, "row", player);
                score -= ScoreLine(row, "row", Utils.OtherPlayer(player));
            }

            return score;
        }

        public static int ScoreVertical(int[,] board, int player)
        {
            int score = 0;

            for (int i = 0; i < board.GetLength(1); i++)
            {
                int[] row = GetCol(board, i);

                score += ScoreLine(row, "col", player);
                score -= ScoreLine(row, "col", Utils.OtherPlayer(player));
            }

            return score;
        }

        public static int ScoreDiagonal1(int[,] board, int player)
        {
            int score = 0;

            List<int[]> slices = new List<int[]>()
            {
                GetDiagSlice1(board,3,0,4),
                GetDiagSlice1(board,4,0,5),
                GetDiagSlice1(board,5,0,6),
                GetDiagSlice1(board,5,1,6),
                GetDiagSlice1(board,5,2,5),
                GetDiagSlice1(board,5,3,4)
            };

            foreach(int[] slice in slices)
            {
                score += ScoreLine(slice, "diag", player);
                score -= ScoreLine(slice, "diag", Utils.OtherPlayer(player));
            }

            return score;
        }

        public static int ScoreDiagonal2(int[,] board, int player)
        {
            int score = 0;

            List<int[]> slices = new List<int[]>()
            {
                GetDiagSlice2(board,2,0,4),
                GetDiagSlice2(board,1,0,5),
                GetDiagSlice2(board,0,0,6),
                GetDiagSlice2(board,0,1,6),
                GetDiagSlice2(board,0,2,5),
                GetDiagSlice2(board,0,3,4)
            };

            foreach (int[] slice in slices)
            {
                score += ScoreLine(slice, "diag", player);
                score -= ScoreLine(slice, "diag", Utils.OtherPlayer(player));
            }

            return score;
        }

        public static int[] GetDiagSlice1(int[,] board, int startRow, int startCol, int len)
        {
            int[] slice = new int[len];

            for (int k = 0; k < len; k++)
            {
                slice[k] = board[startRow - k, startCol + k];
            }

            return slice;
        }

        public static int[] GetDiagSlice2(int[,] board, int startRow, int startCol, int len)
        {
            int[] slice = new int[len];

            for (int k = 0; k < len; k++)
            {
                slice[k] = board[startRow + k, startCol + k];
            }

            return slice;
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

namespace SI_Lab_03.ScoringFunctions
{
    class NaiveScore : IScoreBoard
    {
        public string GetName()
        {
            return "Naive";
        }

        public int Score(int[,] board, int player)
        {
            if (Utils.CheckWin(board, Utils.OtherPlayer(player)))
            {
                return -1;
            }
            else if (Utils.CheckWin(board, player))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}

namespace SI_Lab_03.ScoringFunctions
{
    interface IScoreBoard
    {
        string GetName();

        int Score(int[,] board, int player);
    }
}

namespace PoolStageSimulator.Core
{
    public class CompetitionResult
    {
        public int TotalGoalsAgainstOpponent { get; set; }
        public int TotalGoalsMadeByOpponent { get; set; }
        public int GoalDifference
        {
            get => TotalGoalsAgainstOpponent - TotalGoalsMadeByOpponent;
        }
    }
}
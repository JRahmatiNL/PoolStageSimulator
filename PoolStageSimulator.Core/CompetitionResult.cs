namespace PoolStageSimulator.Core
{
    public class CompetitionResult
    {
        public Team ParticipatingTeam { get; set; }
        public Team OpponentsTeam { get; set; }

        public int TotalGoalsAgainstOpponent { get; set; }
        public int TotalGoalsMadeByOpponent { get; set; }
        public int GoalDifference
        {
            get => TotalGoalsAgainstOpponent - TotalGoalsMadeByOpponent;
        }
    }
}
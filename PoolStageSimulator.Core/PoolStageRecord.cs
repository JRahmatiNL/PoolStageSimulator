namespace PoolStageSimulator.Core
{
    public class PoolStageRecord
    {
        public Team Team { get; set; }
        // 1. points
        public int TotalPoints { get; set; }
        // 2. goal difference
        public int GoalDifference { get; set; }
        // 3. scored points
        public int TotalGoalsAgainstOpponents { get; set; }
        // 4. lost points
        public int TotalGoalsMadeByOpponents { get; set; }
        // 5. results within individual competitions
        // TODO: Figure out a way to sort by individual competition results
        // This is required when above 4 points remain equal between one or more teams
    }
}
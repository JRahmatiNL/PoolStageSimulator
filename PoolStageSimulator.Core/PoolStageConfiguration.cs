namespace PoolStageSimulator.Core
{
    public class PoolStageConfiguration
    {
        public int TotalPointsToIncreaseOnWonRounds { get; set; } = 3;
        public int TotalPointsToIncreaseOnEqualRounds { get; set; } = 1;
    }
}
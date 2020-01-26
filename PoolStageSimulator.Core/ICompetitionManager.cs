using System.Collections.Generic;

namespace PoolStageSimulator.Core
{
    public interface ICompetitionManager
    {
        CompetitionResult RunCompetition(Team team, Team opponent);
        IList<PoolStageRecord> SortPoolStageResults(IList<PoolStageRecord> poolStageRecords);
    }
}
using System.Collections.Generic;

namespace PoolStageSimulator.Core
{
    public interface IPoolStageRecordsBuilder
    {
        IEnumerable<PoolStageRecord> Build();
        void Add(CompetitionResult mainTeamsCompetitionResult);
    }
}
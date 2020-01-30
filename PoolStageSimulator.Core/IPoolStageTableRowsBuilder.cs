using System.Collections.Generic;

namespace PoolStageSimulator.Core
{
    public interface IPoolStageTableRowsBuilder
    {
        IEnumerable<PoolStageTableRow> Build();
        void Add(CompetitionResult mainTeamsCompetitionResult);
    }
}
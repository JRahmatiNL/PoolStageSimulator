using System.Collections.Generic;

namespace PoolStageSimulator.Core
{
    public interface IPoolStageTableRowsBuilder
    {
        IList<PoolStageTableRow> Build();
        void Add(CompetitionResult mainTeamsCompetitionResult);
    }
}
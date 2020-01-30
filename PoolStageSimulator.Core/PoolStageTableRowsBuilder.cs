using System.Collections.Generic;

namespace PoolStageSimulator.Core
{
    public class PoolStageTableRowsBuilder : IPoolStageTableRowsBuilder
    {
        private IList<CompetitionResult> _competitionResults = new List<CompetitionResult>();
        public void Add(CompetitionResult mainTeamsCompetitionResult)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PoolStageTableRow> Build()
        {
            throw new System.NotImplementedException();
        }
    }
}
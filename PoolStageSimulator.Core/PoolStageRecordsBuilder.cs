using System.Collections.Generic;

namespace PoolStageSimulator.Core
{
    public class PoolStageRecordsBuilder : IPoolStageRecordsBuilder
    {
        private IList<CompetitionResult> _competitionResults = new List<CompetitionResult>();
        public void Add(CompetitionResult mainTeamsCompetitionResult)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PoolStageRecord> Build()
        {
            throw new System.NotImplementedException();
        }
    }
}
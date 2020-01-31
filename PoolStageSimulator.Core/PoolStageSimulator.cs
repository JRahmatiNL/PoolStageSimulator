using System;
using System.Collections.Generic;
using System.Linq;

namespace PoolStageSimulator.Core
{
    public class PoolStageSimulator : IPoolStageSimulator
    {
        private ICompetitionManager _competitionManager;

        public PoolStageSimulator(ICompetitionManager competitionManager) {
            _competitionManager = competitionManager;
        }

        public IList<PoolStageTableRow> Play(IList<Team> participatingTeams)
        {
            var poolStageTableRowsBuilder = new PoolStageTableRowsBuilder(new PoolStageConfiguration());
            
            for (int teamIndex = 0; teamIndex < participatingTeams.Count; teamIndex++)
            {
                var team = participatingTeams[teamIndex];
                for (int opponentIndex = teamIndex + 1; opponentIndex < participatingTeams.Count; opponentIndex++)
                {
                    var opponent = participatingTeams[opponentIndex];
                    var competitionResult = _competitionManager.RunCompetition(team, opponent);

                    poolStageTableRowsBuilder.Add(competitionResult);
                }
            }
            var poolStageTableRows = poolStageTableRowsBuilder.Build();
            return _competitionManager.SortPoolStageResults(poolStageTableRows);
        }
    }
}
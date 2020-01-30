using System;
using System.Collections.Generic;
using System.Linq;

namespace PoolStageSimulator.Core
{
    public class SoccerCompetitiontManager : ICompetitionManager
    {
        private Random _randomNumberGenerator = new Random();

        public CompetitionResult RunCompetition(Team team, Team opponent)
        {
            var randomAttackLevel = _randomNumberGenerator.Next(team.AverageAttackLevel);

            return new CompetitionResult
            {
                TotalGoalsAgainstOpponent = Math.Max(randomAttackLevel / 2, randomAttackLevel - opponent.AverageDefenseLevel),
                TotalGoalsMadeByOpponent = Math.Max(0, opponent.AverageAttackLevel - team.AverageDefenseLevel),
            };
        }

        public IList<PoolStageTableRow> SortPoolStageResults(IList<PoolStageTableRow> poolStageTableRows)
        {
            // https://stackoverflow.com/a/5430085
            return poolStageTableRows.OrderByDescending(r => r ).ToList();
        }
    }
}
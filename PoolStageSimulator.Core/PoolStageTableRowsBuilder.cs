using System;
using System.Collections.Generic;
using System.Linq;

namespace PoolStageSimulator.Core
{
    public class PoolStageTableRowsBuilder : IPoolStageTableRowsBuilder
    {   
        private IList<CompetitionResult> _competitionResults = new List<CompetitionResult>();
        private List<PoolStageTableRow> _poolStageTableRows = new List<PoolStageTableRow>();
        private PoolStageConfiguration _poolStageConfiguration;

        public PoolStageTableRowsBuilder(PoolStageConfiguration poolStageConfiguration)
        {
            _poolStageConfiguration = poolStageConfiguration;       
        }

        public void Add(CompetitionResult competitionResult)
        {
            _competitionResults.Add(competitionResult);

            SetupPoolStageTableRowByTeam(competitionResult.ParticipatingTeam);
            SetupPoolStageTableRowByTeam(competitionResult.OpponentsTeam);
        }

        private void SetupPoolStageTableRowByTeam(Team teamToSetup)
        {
            var poolStageTableRow = _poolStageTableRows.SingleOrDefault(record => record.Team == teamToSetup);
            if(poolStageTableRow == null)
            {
                poolStageTableRow = new PoolStageTableRow(){ Team = teamToSetup };
                _poolStageTableRows.Add(poolStageTableRow);
            }
        }

        public IList<PoolStageTableRow> Build()
        {
            foreach (var competitionResult in _competitionResults)
            {
                var teamsPoolStageTableRow = _poolStageTableRows.SingleOrDefault(
                    record => record.Team == competitionResult.ParticipatingTeam
                );
                var opponentsPoolStageTableRow = _poolStageTableRows.SingleOrDefault(
                    record => record.Team == competitionResult.OpponentsTeam
                );

                var totalGoalsAgainstOpponent = competitionResult.TotalGoalsAgainstOpponent;
                var totalGoalsMadeByOpponent = competitionResult.TotalGoalsMadeByOpponent;
                if(totalGoalsAgainstOpponent > totalGoalsMadeByOpponent)
                {
                    teamsPoolStageTableRow.TotalPoints += 
                        _poolStageConfiguration.TotalPointsToIncreaseOnWonRounds;
                    teamsPoolStageTableRow.DefeatedTeams.Add(competitionResult.OpponentsTeam);
                }
                else if(totalGoalsAgainstOpponent == totalGoalsMadeByOpponent)
                {
                    teamsPoolStageTableRow.TotalPoints += 
                        _poolStageConfiguration.TotalPointsToIncreaseOnEqualRounds;
                    teamsPoolStageTableRow.TotalPoints += 
                        _poolStageConfiguration.TotalPointsToIncreaseOnEqualRounds;
                }
                else
                {
                    opponentsPoolStageTableRow.TotalPoints += 
                        _poolStageConfiguration.TotalPointsToIncreaseOnWonRounds;
                    opponentsPoolStageTableRow.DefeatedTeams.Add(competitionResult.ParticipatingTeam);
                }

                opponentsPoolStageTableRow.TotalGoalsAgainstOpponents += 
                    competitionResult.TotalGoalsMadeByOpponent;
                opponentsPoolStageTableRow.TotalGoalsMadeByOpponents += 
                    competitionResult.TotalGoalsAgainstOpponent;
                opponentsPoolStageTableRow.GoalDifference += -competitionResult.GoalDifference;

                teamsPoolStageTableRow.TotalGoalsAgainstOpponents += 
                    competitionResult.TotalGoalsAgainstOpponent;
                teamsPoolStageTableRow.TotalGoalsMadeByOpponents += 
                    competitionResult.TotalGoalsMadeByOpponent;
                teamsPoolStageTableRow.GoalDifference += 
                    competitionResult.GoalDifference;
            }
            return _poolStageTableRows;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoolStageSimulator.Core
{
    public class PoolStageSimulator : IPoolStageSimulator
    {
        const int TotalPointsToIncreaseOnWonRounds = 3;
        const int TotalPointsToIncreaseOnEqualRounds = 1;
        private ICompetitionManager _competitionManager;
        public PoolStageSimulator(ICompetitionManager competitionManager)
        {
            _competitionManager = competitionManager;
        }

        public IList<PoolStageTableRow> Play(IList<Team> participatingTeams)
        {
            var poolStageTableRows = new List<PoolStageTableRow>();
            for (int teamIndex = 0; teamIndex < participatingTeams.Count; teamIndex++)
            {
                var team = participatingTeams[teamIndex];
                var teamsRecordToMutate = poolStageTableRows.SingleOrDefault(record => record.Team == team);
                if(teamsRecordToMutate == null)
                {
                    teamsRecordToMutate = new PoolStageTableRow(){ Team = team };
                    poolStageTableRows.Add(teamsRecordToMutate);
                }

                for (int opponentIndex = teamIndex + 1; opponentIndex < participatingTeams.Count; opponentIndex++)
                {
                    var opponent = participatingTeams[opponentIndex];
                    var opponentsRecordToMutate = poolStageTableRows.SingleOrDefault(record => record.Team == opponent);
                    if(opponentsRecordToMutate == null)
                    {
                        opponentsRecordToMutate = new PoolStageTableRow(){ Team = opponent };
                        poolStageTableRows.Add(opponentsRecordToMutate);
                    }
                    var competitionResult = _competitionManager.RunCompetition(team, opponent);
                    var totalGoalsAgainstOpponent = competitionResult.TotalGoalsAgainstOpponent;
                    var totalGoalsMadeByOpponent = competitionResult.TotalGoalsMadeByOpponent;
                    if(totalGoalsAgainstOpponent > totalGoalsMadeByOpponent)
                    {
                        teamsRecordToMutate.TotalPoints += TotalPointsToIncreaseOnWonRounds;
                        teamsRecordToMutate.DefeatedTeams.Add(opponent);
                    }
                    else if(totalGoalsAgainstOpponent == totalGoalsMadeByOpponent)
                    {
                        teamsRecordToMutate.TotalPoints += TotalPointsToIncreaseOnEqualRounds;
                        opponentsRecordToMutate.TotalPoints += TotalPointsToIncreaseOnEqualRounds;
                    }
                    else
                    {
                        opponentsRecordToMutate.TotalPoints += TotalPointsToIncreaseOnWonRounds;
                        opponentsRecordToMutate.DefeatedTeams.Add(team);
                    }
                    teamsRecordToMutate.TotalGoalsAgainstOpponents += competitionResult.TotalGoalsAgainstOpponent;
                    teamsRecordToMutate.TotalGoalsMadeByOpponents += competitionResult.TotalGoalsMadeByOpponent;
                    teamsRecordToMutate.GoalDifference += competitionResult.GoalDifference;
                }
            }
            return _competitionManager.SortPoolStageResults(poolStageTableRows);
        }
    }
}
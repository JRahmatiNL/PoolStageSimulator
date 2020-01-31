using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PoolStageSimulator.Core;

namespace PoolStageSimulator.Tests
{
    [TestFixture]
    public class PoolStageTableRowsBuilderTest
    {
        private PoolStageConfiguration _poolStageConfiguration;
        private IPoolStageTableRowsBuilder _poolStageTableRowsBuilder;
        private IList<CompetitionResult> _2CompetitionResults_With4UniqueTeams_Returns4PoolStageTableRows;

        [SetUp]
        public void Setup()
        {
            _poolStageConfiguration = new PoolStageConfiguration
            {
                TotalPointsToIncreaseOnWonRounds = 5,
                TotalPointsToIncreaseOnEqualRounds = 1
            };
            _poolStageTableRowsBuilder = new PoolStageTableRowsBuilder(_poolStageConfiguration);

            _2CompetitionResults_With4UniqueTeams_Returns4PoolStageTableRows = new List<CompetitionResult>
            {
                new CompetitionResult { 
                    ParticipatingTeam = new Team("Team1"),
                    OpponentsTeam = new Team("Team2") 
                },
                new CompetitionResult { 
                    ParticipatingTeam = new Team("Team3"),
                    OpponentsTeam = new Team("Team4") 
                },
            };
        }

        [Test]
        public void Build_Using2Competitions_With4UniqueTeams_Returns4PoolStageTableRows()
        {
            // 1. Arrange
            var simulator = new Core.PoolStageSimulator(
                new SoccerCompetitiontManager()
            );

            // 2. Act
            foreach (var competitionResult in _2CompetitionResults_With4UniqueTeams_Returns4PoolStageTableRows)
            {
                _poolStageTableRowsBuilder.Add(competitionResult);                
            }
            var poolStageTableRows = _poolStageTableRowsBuilder.Build();

            // 3. Assert
            Assert.AreEqual(4, poolStageTableRows.Count);
        }

        [Test]
        public void Build_UsingMainTeamsLostCompetition_IncludesOpponentsTotalsInResults()
        {
            // 1. Arrange
            var mainTeam = new Team("Loser");
            var opponentsTeam = new Team("Winner");

            // 2. Act
            var mainTeamsCompetitionResult = new CompetitionResult
            {
                ParticipatingTeam = mainTeam,
                OpponentsTeam = opponentsTeam,
                TotalGoalsAgainstOpponent = 0,
                TotalGoalsMadeByOpponent = 1,
            };
            _poolStageTableRowsBuilder.Add(mainTeamsCompetitionResult);
            var someOtherCompetitionResult = new CompetitionResult
            {
                ParticipatingTeam = new Team(""),
                OpponentsTeam = new Team(""),
                TotalGoalsAgainstOpponent = 0,
                TotalGoalsMadeByOpponent = 0,
            };
            _poolStageTableRowsBuilder.Add(someOtherCompetitionResult);
            var poolStageTableRows = _poolStageTableRowsBuilder.Build();

            // 3. Assert
            var opponentsRow = poolStageTableRows.Single(record => record.Team == opponentsTeam);
            Assert.AreEqual(
                _poolStageConfiguration.TotalPointsToIncreaseOnWonRounds, 
                opponentsRow.TotalPoints
            );
            Assert.AreEqual(0, opponentsRow.TotalGoalsMadeByOpponents);
            Assert.AreEqual(1, opponentsRow.TotalGoalsAgainstOpponents);
            Assert.AreEqual(1, opponentsRow.GoalDifference);
        }
    }
}
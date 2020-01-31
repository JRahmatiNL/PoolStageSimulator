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

        [SetUp]
        public void Setup()
        {
            _poolStageConfiguration = new PoolStageConfiguration
            {
                TotalPointsToIncreaseOnWonRounds = 5,
                TotalPointsToIncreaseOnEqualRounds = 1
            };
            _poolStageTableRowsBuilder = new PoolStageTableRowsBuilder(_poolStageConfiguration);
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
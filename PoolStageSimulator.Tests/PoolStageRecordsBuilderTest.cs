using System.Linq;
using NUnit.Framework;
using PoolStageSimulator.Core;

namespace PoolStageSimulator.Tests
{
    [TestFixture]
    public class PoolStageRecordsBuilderTest
    {
        public IPoolStageRecordsBuilder PoolStageRecordsBuilder { get; private set; }

        [SetUp]
        public void Setup()
        {
            PoolStageRecordsBuilder = new PoolStageRecordsBuilder();
        }

        [Test]
        public void Build_UsingMainTeamsLostCompetition_IncludesOpponentsTotalGoalsInResults()
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
            PoolStageRecordsBuilder.Add(mainTeamsCompetitionResult);
            var someOtherCompetitionResult = new CompetitionResult
            {
                ParticipatingTeam = new Team(""),
                OpponentsTeam = new Team(""),
                TotalGoalsAgainstOpponent = 0,
                TotalGoalsMadeByOpponent = 0,
            };
            PoolStageRecordsBuilder.Add(someOtherCompetitionResult);
            var poolStageRecords = PoolStageRecordsBuilder.Build().ToList();

            // 3. Assert
            var opponentsRecord = poolStageRecords.Single(record => record.Team == opponentsTeam);
            Assert.AreEqual(
                mainTeamsCompetitionResult.TotalGoalsMadeByOpponent, 
                opponentsRecord.TotalGoalsAgainstOpponents
            );
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PoolStageSimulator.Core;

namespace PoolStageSimulator.Tests
{
    [TestFixture]
    public class SimulatorTest
    {
        private IList<Team> _teamsFromBestToWorst = new List<Team>();

        // Note that there is a slight chance that game can end in a draw

        [SetUp]
        public void Setup()
        {
            for (int teamNumber = 1; teamNumber <= 4; teamNumber++)
            {
                var team = new Team($"Team {teamNumber}")
                {
                    AverageAttackLevel = (5 - teamNumber) * 2,
                    AverageDefenseLevel = (5 - teamNumber) * 2
                };
                _teamsFromBestToWorst.Add(team);
            }
        }

        [Test]
        public void BestTeamsWinMostOfTheTimes()
        {
            // 1. Arrange
            var simulator = new Core.PoolStageSimulator(
                new SoccerCompetitiontManager()
            );

            // 2. Act
            var totalTimesTeamsGotOnExpectedPlaceBasedOnStrength = 0;
            for (int simulationIndex = 0; simulationIndex < 10; simulationIndex++)
            {
                var results = simulator.Play(_teamsFromBestToWorst);
                var teamsFrom1stTo4th = new List<Team>
                {
                    results[1-1].Team,
                    results[2-1].Team,
                    results[3-1].Team,
                    results[4-1].Team,
                };
                var didTeamsGetExpectedPlacesBasedOnStrength = true;
                for (int i = 0; i < teamsFrom1stTo4th.Count; i++)
                {
                    if(teamsFrom1stTo4th[i] != _teamsFromBestToWorst[i])
                    {
                        didTeamsGetExpectedPlacesBasedOnStrength = false;
                        break;
                    }
                }
                if(didTeamsGetExpectedPlacesBasedOnStrength)
                {
                    totalTimesTeamsGotOnExpectedPlaceBasedOnStrength++;
                }
            }

            // 3. Assert
            // Ensure that expected places are sorted based on team strength
            Assert.IsTrue(totalTimesTeamsGotOnExpectedPlaceBasedOnStrength > 2);
        }
    }
}
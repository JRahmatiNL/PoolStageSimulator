using System.Collections.Generic;
using NUnit.Framework;
using PoolStageSimulator.Core;

namespace PoolStageSimulator.Tests
{
    [TestFixture]
    public class SoccerCompetitionManagerTest
    {
        private SoccerCompetitiontManager _competitionManager;        

        private Team _teamA;
        private Team _teamB;
        private Team _teamC;

        
        [SetUp]
        public void Setup()
        {
            _competitionManager = new SoccerCompetitiontManager();
            _teamA = new Team("Team A");
            _teamB = new Team("Team B");
            _teamC = new Team("Team C");
        }

        [Test]
        public void SortResultsBasedOnIndividualCompetitionsIfNecessary()
        {
            // 1. Arrange
            var resultsToSort = new List<PoolStageRecord>()
            {
                new PoolStageRecord { 
                    Team = _teamC
                },
                new PoolStageRecord { 
                    Team = _teamB, 
                    DefeatedTeams = new List<Team>() { _teamC } 
                },
                new PoolStageRecord { 
                    Team = _teamA, 
                    DefeatedTeams = new List<Team>() { _teamB, _teamC } 
                },
            };

            // 2. Act
            var sortedResults = _competitionManager.SortPoolStageResults(resultsToSort);

            // 3. Assert
            Assert.AreEqual(sortedResults[1-1].Team.Name, _teamA.Name);
            Assert.AreEqual(sortedResults[2-1].Team.Name, _teamB.Name);
            Assert.AreEqual(sortedResults[3-1].Team.Name, _teamC.Name);
        }
    }
}
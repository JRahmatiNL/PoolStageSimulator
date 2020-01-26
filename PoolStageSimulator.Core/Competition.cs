using System.Collections.Generic;

namespace PoolStageSimulator.Core
{
    internal class Competition
    {
        public IEnumerable<Team> ParticipatingTeams { get; internal set; }
    }
}
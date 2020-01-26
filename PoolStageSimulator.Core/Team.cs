namespace PoolStageSimulator.Core
{
    public class Team
    {
        public string Name { get; set; }
        public int AverageDefenseLevel { get; set; }
        public int AverageAttackLevel { get; set; }

        public Team(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
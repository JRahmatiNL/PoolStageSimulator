using System;
using System.Collections.Generic;
using PoolStageSimulator.Core;

namespace PoolStageSimulator.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var simulator = new PoolStageSimulator.Core.PoolStageSimulator(new SoccerCompetitiontManager());
            var participatingTeams = GetTeamsFromUserInput();
            var timesToRepeatSimulation = GetDesiredNumberOfSimulationsFromUserInput();

            for (int simulationIndex = 0; simulationIndex < timesToRepeatSimulation; simulationIndex++)
            {
                var results = simulator.Play(participatingTeams);

                Console.WriteLine("Simulation ended with following results: ");
                Console.WriteLine("=========================================");
                var consoleTable = ConsoleTables.ConsoleTable.From(results);
                consoleTable.Columns.Remove(nameof(PoolStageRecord.DefeatedTeams));
                consoleTable.Write();
            }
        }

        private static int GetDesiredNumberOfSimulationsFromUserInput()
        {
            do
            {
                Console.WriteLine("How many simulations do you want to run?");
                if (int.TryParse(Console.ReadLine(), out var desiredNumberOfSimulations))
                {
                    return desiredNumberOfSimulations;
                }
                Console.WriteLine("Invalid input, please enter a whole number");
            } while (true);
        }

        private static IList<Team> GetTeamsFromUserInput()
        {
            var participatingTeams = new List<Team>();
            for (int teamNumber = 1; teamNumber <= 4; teamNumber++)
            {
                Console.WriteLine($"Enter name of team {teamNumber}:");
                var participatingTeam = new Team(Console.ReadLine());
                do
                {
                    Console.WriteLine("Enter teams average attack level:");
                    if (int.TryParse(Console.ReadLine(), out var averageAttackLevel))
                    {
                        participatingTeam.AverageAttackLevel = averageAttackLevel;
                        break;
                    }
                    Console.WriteLine("Invalid input, please enter a whole number");
                } while (true);
                do
                {
                    Console.WriteLine("Enter teams average defense level:");
                    if (int.TryParse(Console.ReadLine(), out var averageDefenseLevel))
                    {
                        participatingTeam.AverageDefenseLevel = averageDefenseLevel;
                        break;
                    }
                    Console.WriteLine("Invalid input, please enter a whole number");
                } while (true);

                participatingTeams.Add(participatingTeam);
            }
            return participatingTeams;
        }
    }
}

!!! Warning !!! This project is sill in a prototype stage, so it may contain a lot of bad practices, boilerplate code as well as code that is not stable enough to be used in production! Only use this code for experimentations or for feedback reasons.

# Intro 
The intention of this document is to cover the most important notes required for taking part
on this git project. Please read those carefully before making any changes.
Note that this project is meant to be portable.

Making any changes could impact all projects relying on this code base.
One way to deal with this is through semantic versioning in combination with different git branches.


# Semantic versioning 
Semantic versioning is explained in detail at https://semver.org/.
Although not in the initial commit, it is still very wise to take semantic versioning into consideration while working on this project.
Eventually you don't want any headaches with compatiblity issues when adding new features or making breaking changes.

# Unity3D 2019.3.05f & .Net Standard 2.0 
Note that this project is written with an intention of portability with Unity3D.
While composing this document the latest version of Unity 2019.3.05f doesn't support any dotnet standard versions higher than 2.0


# Other notes/links 
https://stackoverflow.com/questions/53447595/nuget-packages-in-unity
https://github.com/BrianPeek/NuGet2Unity
https://github.com/straighteight/SpecFlow-VS-Mac-Integration

Visual Studio code => .Net Core Team Explorer

# Initial goals 
There are multiple ways to keep track of tasks, but I will keep goals here for now, since this project is still in a prototyping stage.

Tasks to accomplish:
- A pool stage simulator that can simulate competitions where the strongest team often wins
- It should be possible to at least sort the results based on 5 criterias:
    1. The sum of points earned by winning competitions (3 points per competition) or ending them in a draw (1 point per competition)
    2. The sum of all goal differences from all competitions
    3. The sum of goals made against opponents
    4. The sum of goals made by opponents
    5. If all 4 other points still cause a draw between some teams, then also by individual competition results

- Integration with Unity3D (Unity3D uses its own package management system which is not nuget) using some of the tools/links/ideas below:
    - https://github.com/BrianPeek/NuGet2Unity
    - A Web API with rest calls

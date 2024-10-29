// See https://aka.ms/new-console-template for more information
using AdventOfCode2022.Utils;
using System.Reflection;

try
{
    var solversClasses = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => typeof(ISolver).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
        .ToList();

    if (!solversClasses.Any())
    {
        Console.WriteLine("No puzzle solvers found.");
        return;
    }

    var latestSolver = solversClasses
        .OrderByDescending(x => int.Parse(x.Name.ToLowerInvariant().Replace("day", "")))
        .FirstOrDefault();

    if (latestSolver == null)
    {
        Console.WriteLine("No valid puzzle solvers found with proper naming.");
        return;
    }

    //latestSolver = typeof(Day1);

    Console.WriteLine($"Executing {latestSolver}");

    (Activator.CreateInstance(latestSolver) as ISolver)?.Solve();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
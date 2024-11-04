using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day2 : ISolver
{
    private string fileName = "Inputs/Day2.txt";
    private int sum = 0;
    private Dictionary<char, int> dict = new() { { 'X', 1 }, { 'Y', 2 }, { 'Z', 3 } };
    private HashSet<string> myWinConditions = ["AY", "BZ", "CX"];
    private HashSet<string> myDrawConditions = ["AX", "BY", "CZ"];
    public string Solve()
    {
        try
        {
            using var sr = new StreamReader(fileName);
            var tempSum = 0;

            while (sr.ReadLine() is string line)
            {
                var opp = line[0];
                var mine = line[2];

                sum += dict[mine];
                sum += myWinConditions.Contains($"{opp}{mine}") ? 6
                    : myDrawConditions.Contains($"{opp}{mine}") ? 3
                    : 0;                
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }

        return sum.ToString();
    }
}
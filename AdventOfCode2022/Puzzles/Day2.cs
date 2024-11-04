using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day2 : ISolver
{
    private string fileName = "Inputs/Day2.txt";
    private int sum = 0;

    private static int[] myWinCondition = [2, 3, 1]; // AY, BZ, CX
    private static int[] myDrawCondition = [1, 2, 3];
    private static int[] myLoseCondition = [3, 1, 2];

    private Dictionary<char, int[]> dict = new() { { 'X', myLoseCondition }, { 'Y', myDrawCondition }, { 'Z', myWinCondition } };
    public string Solve()
    {
        try
        {
            using var sr = new StreamReader(fileName);
            var tempSum = 0;

            while (sr.ReadLine() is string line)
            {
                var oppScore = line[0] - 'A';
                var condition = line[2];

                sum += dict[condition][oppScore] + (condition - 'X') * 3;                            
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }

        return sum.ToString();
    }
}
using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day3 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day3)}.txt";
    private int sum = 0;

    private HashSet<char> chars = [];

    public string Solve()
    {
        try
        {
            using var sr = new StreamReader(fileName);

            while (sr.ReadLine() is string line)
            {
                string firstHalf = line.Substring(0, line.Length / 2);
                string secondHalf = line.Substring(line.Length / 2);

                var charSet = firstHalf.ToHashSet();

                var item = secondHalf.FirstOrDefault(charSet.Contains);
                if (item != default)
                {
                    sum += item <= 'Z' ? item - 'A' + 27 : item - 'a' + 1;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }

        return sum.ToString();
    }
}
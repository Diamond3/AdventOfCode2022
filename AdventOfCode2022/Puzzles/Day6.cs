using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day6 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day6)}.txt";

    public string Solve()
    {
        try
        {
            using var sr = new StreamReader(fileName);
            var str = sr.ReadLine()!;
            var hashSet = new HashSet<char>();

            for (int i = 3; i < str.Length; i++)
            {
                hashSet.Clear();
                for (int j = (i >= 13 ? i - 13 : 0); j <= i; j++)
                {
                    hashSet.Add(str[j]);
                }
                if (hashSet.Count == 14)
                {
                    return (i + 1).ToString();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }

        return "";
    }
}
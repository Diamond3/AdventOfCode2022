using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day4 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day4)}.txt";
    private int sum = 0;

    public string Solve()
    {
        try
        {
            using var sr = new StreamReader(fileName);

            while (sr.ReadLine() is string str)
            {
                var splited = str.Split(',');
                var x = splited[0].Split('-').Select(int.Parse).ToArray();
                var y = splited[1].Split('-').Select(int.Parse).ToArray();

                if (!((x[0] > y[1] && x[1] > y[1]) || (x[0] < y[0] && x[1] < y[0])))
                {
                    sum++;
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
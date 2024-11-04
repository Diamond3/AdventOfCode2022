using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day3 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day3)}.txt";
    private int sum = 0;

    public string Solve()
    {
        try
        {
            using var sr = new StreamReader(fileName);

            while ((sr.ReadLine(), sr.ReadLine(), sr.ReadLine()) is var (x, y, z))
            {
                if (x == null)
                { 
                    break;
                }

                var charSetX = x.ToHashSet();
                var charSetY = y.ToHashSet();
                var item = z.FirstOrDefault(z => charSetX.Contains(z) && charSetY.Contains(z));

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
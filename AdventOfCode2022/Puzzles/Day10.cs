using AdventOfCode2022.Utils;
using System.Security;

namespace AdventOfCode2022.Puzzles;

public class Day10 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day10)}.txt";
    private int spritePos = 0;

    public string Solve()
    {
        using var sr = new StreamReader(fileName);
        var x = 1;
        var cycle = 0;
        var sum = 0L;
        var currentPixel = 0;

        while (sr.ReadLine() is string str)
        {
            var command = str.Split(' ')[0];
            var iterations = 1;
            var num = 0;

            if (command != "noop")
            {
                iterations = 2;
                num = int.Parse(str.Split(' ')[1]);
            }

            for (int i = 0; i < iterations; i++)
            {
                if (cycle % 40 == 0)
                {
                    Console.WriteLine();
                }
                PrintPixel(x, cycle);
                cycle++;
            }
            if (iterations == 2)
            {
                x += num;
            }
        }

        return sum.ToString();
    }

    private void PrintPixel(int x, int currentPixel)
    {
        spritePos = (x - 1);
        if (currentPixel % 40 == spritePos || currentPixel % 40 == spritePos + 1 || currentPixel % 40 == spritePos + 2)
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(".");
        }
    }
}
using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day10 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day10)}.txt";

    public string Solve()
    {
        using var sr = new StreamReader(fileName);
        var x = 1L;
        var cycle = 0;
        var sum = 0L;

        while (sr.ReadLine() is string str)
        {
            var command = str.Split(' ')[0];
            if (command == "noop")
            {
                cycle++;
                if ((cycle + 20) % 40 == 0)
                {
                    sum += x * cycle;
                    Console.WriteLine(cycle);
                    Console.WriteLine(cycle * x);
                    Console.WriteLine();
                }
                continue;
            }

            var num = int.Parse(str.Split(' ')[1]);

            if ((cycle + 21) % 40 == 0)
            {
                sum += x * (cycle + 1);
                Console.WriteLine((cycle + 1));
                Console.WriteLine((cycle + 1) * x);
                Console.WriteLine();
            }

            cycle += 2;
            if ((cycle + 20) % 40 == 0)
            {
                sum += x * cycle;
                Console.WriteLine(cycle);
                Console.WriteLine(cycle * x);
                Console.WriteLine();
            }
            x += num;
        }

        return sum.ToString();
    }
}
using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day8 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day8)}.txt";

    private List<int[]> map = [];

    public string Solve()
    {
        using var sr = new StreamReader(fileName);

        while (sr.ReadLine() is string str)
        {
            var row = new int[str.Length];
            var i = 0;
            foreach (var item in str)
            {
                row[i++] = item - '0';
            }
            map.Add(row);
        }

        var max = long.MinValue;

        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                var score = FindVisibilityScore(map, y, x);
                if (score > max)
                {
                    max = score;
                }
            }
        }

        return max.ToString();
    }

    private int FindVisibilityScore(List<int[]> map, int y, int x)
    {
        int a = 0, b = 0, c = 0, d = 0;
        for (int i = x + 1; i < map[0].Length; i++) // Righ
        {
            a++;
            if (map[y][i] >= map[y][x])
            {
                break;
            }
        }

        for (int i = y + 1; i < map.Count; i++) // Down
        {
            b++;
            if (map[i][x] >= map[y][x])
            {
                break;
            }
        }

        for (int i = x - 1; i >= 0; i--) // Left
        {
            c++;
            if (map[y][i] >= map[y][x])
            {
                break;
            }
        }

        for (int i = y - 1; i >= 0; i--) // Up
        {
            d++;
            if (map[i][x] >= map[y][x])
            {
                break;
            }
        }

        return a * b * c * d;
    }
}
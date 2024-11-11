using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day9 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day9)}.txt";

    private Dictionary<string, Point> directions = new() {
            { "R", new Point(1, 0)},
            { "D", new Point(0, -1) },
            { "L", new Point(-1, 0) },
            { "U", new Point(0, 1) }
        };

    public string Solve()
    {
        using var sr = new StreamReader(fileName);

        var visited = new HashSet<Point>();
        var head = new Point(0, 0);

        var tails = Enumerable.Range(0, 9).Select(_ => new Point(0, 0)).ToArray();

        while (sr.ReadLine() is string str)
        {
            var dir = directions[str.Split(' ')[0]];
            var count = int.Parse(str.Split(' ')[1]);

            for (var i = 0; i < count; i++)
            {
                head += dir;
                tails[0] = GetNextTailPos(head, tails[0]);

                for (var j = 1; j < tails.Length; j++)
                {
                    tails[j] = GetNextTailPos(tails[j - 1], tails[j]);
                }

                visited.Add(tails[tails.Length - 1]);
            }
        }

        return visited.Count.ToString();
    }

    private void PrintMap(Point head, Point tail)
    {
        for (int i = 5; i >= 0; i--)
        {
            var str = "";
            for (int j = 0; j < 6; j++)
            {
                if (head.x == j && head.y == i)
                {
                    str += "H";
                }
                else if (tail.x == j && tail.y == i)
                {
                    str += "T";
                }
                else
                {
                    str += ".";
                }
            }
            Console.WriteLine(str);
        }
        Console.WriteLine();
    }

    private Point GetNextTailPos(Point head, Point tail)
    {
        var distance = Point.CalcDist(head, tail);
        if (distance <= 1.5f)
        {
            return tail;
        }
        var moveDir = Point.Normalize(head - tail);
        return tail += moveDir;
    }

    public struct Point
    {
        public int x { get; }
        public int y { get; }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point operator +(Point a, Point b) => new Point(a.x + b.x, a.y + b.y);

        public static Point operator -(Point a, Point b) => new Point(a.x - b.x, a.y - b.y);

        public static Point Normalize(Point a) // Since max distance is up to 2.236 (sqrt(5)), 1 step towards 0 needed to avoid double step
        {
            var x = a.x;
            var y = a.y;

            if (Math.Abs(x) > 1)
            {
                x -= Math.Sign(x) * 1;
            }

            if (Math.Abs(y) > 1)
            {
                y -= Math.Sign(y) * 1;
            }

            return new Point(x, y);
        }

        public static float CalcDist(Point a, Point b)
        {
            var c = a - b;
            return (float)Math.Sqrt((c.x * c.x + c.y * c.y));
        }
    }
}
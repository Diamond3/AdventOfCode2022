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

        var count = GetVisibleThreeCount(map);
        return count.ToString();
    }

    private int GetVisibleThreeCount(List<int[]> map)
    {
        var visibleSet = new HashSet<(int y, int x)>();
        var maxValue = new List<int>[4]; // top-down, left-right, right-left, bot-up

        for (int i = 0; i < 4; i++)
        {
            maxValue[i] = new List<int>();
        }

        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                LeftRight(map, visibleSet, maxValue[0], y, x);
                TopDown(map, visibleSet, maxValue[1], y, x);
                RightLeft(map, visibleSet, maxValue[2], y, x);
                BotUp(map, visibleSet, maxValue[3], y, x);
            }
        }
        return visibleSet.Count;
    }

    private void BotUp(List<int[]> map, HashSet<(int y, int x)> visibleSet, List<int> maxValue, int y, int x)
    {
        y = map.Count - y - 1;

        if (y == map.Count - 1)
        {
            AddToLists(map, visibleSet, maxValue, y, x);
            return;
        }

        UpdateIfVisible(map, visibleSet, maxValue, x, y, true);
    }

    private void RightLeft(List<int[]> map, HashSet<(int y, int x)> visibleSet, List<int> maxValue, int y, int x)
    {
        x = map[0].Length - x - 1;

        if (x == map[0].Length - 1)
        {
            AddToLists(map, visibleSet, maxValue, y, x);
            return;
        }
        UpdateIfVisible(map, visibleSet, maxValue, x, y, false);
    }

    private void TopDown(List<int[]> map, HashSet<(int y, int x)> visibleSet, List<int> maxValue, int y, int x)
    {
        if (y == 0)
        {
            AddToLists(map, visibleSet, maxValue, y, x);
            return;
        }

        UpdateIfVisible(map, visibleSet, maxValue, x, y, true);
    }

    private static void LeftRight(List<int[]> map, HashSet<(int y, int x)> visibleSet, List<int> maxValue, int y, int x)
    {
        if (x == 0)
        {
            AddToLists(map, visibleSet, maxValue, y, x);
            return;
        }

        UpdateIfVisible(map, visibleSet, maxValue, x, y, false);
    }

    private static void AddToLists(List<int[]> map, HashSet<(int y, int x)> visibleSet, List<int> maxValue, int y, int x)
    {
        maxValue.Add(map[y][x]);
        visibleSet.Add((y, x));
    }

    private static void UpdateIfVisible(List<int[]> map, HashSet<(int y, int x)> visibleSet, List<int> maxValue, int x, int y, bool fromLeftTop)
    {
        if (maxValue[fromLeftTop ? x : y] < map[y][x])
        {
            visibleSet.Add((y, x));
            maxValue[fromLeftTop ? x : y] = map[y][x];
        }
    }
}
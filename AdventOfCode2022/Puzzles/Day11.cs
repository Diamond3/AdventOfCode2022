using AdventOfCode2022.Utils;
using System.Diagnostics.Contracts;
using System.Security;

namespace AdventOfCode2022.Puzzles;

public class Day11 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day11)}.txt";
    private int spritePos = 0;
    private Dictionary<int, Monkey> monkeys = [];

    public string Solve()
    {
        using var sr = new StreamReader(fileName);
        var emptyLines = 0;

        while (sr.ReadLine() is string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                emptyLines++;
                if (emptyLines == 2)
                {
                    break;
                }
                continue;
            }

            emptyLines = 0;

            var monkeyNr = int.Parse(str!.Split([' ', ':'])[1]);
            var items = sr.ReadLine()!
                .Split([' ', ':', ','])
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Skip(2)
                .Select(ulong.Parse)
                .ToList();

            var operationsLine = sr.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var operation = operationsLine[4];
            var operationNum = operationsLine[5];

            var newMonkey = new Monkey()
            {
                CurrentItems = items,
                Operation = operationsLine[4],
                OperationNum = operationsLine[5],
                DivisibleNum = int.Parse(sr.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries)[3]),
                NextMonkey = new Dictionary<bool, int>()
                {
                    { true, int.Parse(sr.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries)[5]) },
                    { false, int.Parse(sr.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries)[5]) },
                }
            };

            monkeys.Add(monkeyNr, newMonkey);
        }

        var currentMonkey = 0;

        for (int i = 0; i < 1000; i++)
        {
            foreach (var monkey in monkeys.Values)
            {
                monkey.CurrentItems.AddRange(monkey.NextRoundItems);
                monkey.NextRoundItems.Clear();
                monkey.CurrentItems = monkey.CurrentItems.Select(x => monkey.Calclulate(x)).ToList();
                monkey.InspectedItemsCount += monkey.CurrentItems.Count;
                foreach (var item in monkey.CurrentItems)
                {
                    var nextMonkey = monkey.NextMonkey[(item % monkey.DivisibleNum) == 0];
                    monkeys[nextMonkey].NextRoundItems.Add(item);
                }
                monkey.CurrentItems.Clear();
            }
        }
        var counts = monkeys.Values.Select(x => x.InspectedItemsCount).ToList();
        counts.Sort();

        return (counts[counts.Count - 1] * counts[counts.Count - 2]).ToString();
    }
}

public class Monkey()
{
    public List<ulong> CurrentItems = new List<ulong>();
    public List<ulong> NextRoundItems = new List<ulong>();
    public string Operation;
    public string OperationNum;
    public float DivisibleNum;
    public Dictionary<bool, int> NextMonkey = [];
    public decimal InspectedItemsCount = 0;

    public ulong Calclulate(ulong x)
    {
        var val = OperationNum == "old" ? x : ulong.Parse(OperationNum);

        if (Operation == "*")
        {
            return x * val;
        }

        if (Operation == "+")
        {
            return x + val;
        }
        if (Operation == "-")
        {
            return x - val;
        }
        return x / val;
    }
}
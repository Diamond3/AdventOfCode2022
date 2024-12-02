using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day11 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day11)}.txt";
    private int spritePos = 0;
    private Dictionary<long, Monkey> monkeys = [];

    public string Solve()
    {
        using var sr = new StreamReader(fileName);
        var emptyLines = 0;
        var mulPrimes = 1L;

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
                .Select(long.Parse)
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

            mulPrimes *= newMonkey.DivisibleNum;
            monkeys.Add(monkeyNr, newMonkey);
        }

        for (int i = 0; i < 10000; i++)
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
                    monkeys[nextMonkey].NextRoundItems.Add(item % mulPrimes);
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
    public List<long> CurrentItems = new List<long>();
    public List<long> NextRoundItems = new List<long>();
    public string Operation;
    public string OperationNum;
    public long DivisibleNum;
    public Dictionary<bool, int> NextMonkey = [];
    public long InspectedItemsCount = 0;

    public long Calclulate(long x)
    {
        var val = OperationNum == "old" ? x : long.Parse(OperationNum);

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
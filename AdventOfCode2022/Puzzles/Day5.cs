using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day5 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day5)}.txt";

    private List<char[]> input = [];
    private List<Stack<char>> inputStacks = [];

    public string Solve()
    {
        try
        {
            using var sr = new StreamReader(fileName);

            var movesList = false;

            while (sr.ReadLine() is string str)
            {
                if (string.IsNullOrEmpty(str))
                {
                    for (int i = 0; i < input![0].Length; i++)
                    {
                        var newStack = new Stack<char>();
                        for (int j = input.Count - 2; j >= 0; j--)
                        {
                            if (input[j][i] != 32)
                            {
                                newStack.Push(input[j][i]);
                            }
                        }
                        inputStacks.Add(newStack);
                    }

                    input = null;
                    movesList = true;
                    continue;
                }

                if (!movesList)
                {
                    input.Add(str.Where((c, index) => ((index - 1) % 4 == 0)).ToArray());
                }
                else
                {
                    var instr = str.Split(' ').Where((x, index) => (index + 1) % 2 == 0).Select(int.Parse).ToArray();

                    var amount = instr[0];
                    var from = instr[1] - 1;
                    var to = instr[2] - 1;

                    var tempStack = new List<char>();
                    for (int i = 0; i < amount; i++)
                    {
                        tempStack.Add(inputStacks[from].Pop());
                    }

                    tempStack.Reverse();
                    tempStack.ForEach(inputStacks[to].Push);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }

        var ans = "";
        foreach (var stack in inputStacks)
        {
            ans += stack.Pop();
        }

        return ans;
    }
}
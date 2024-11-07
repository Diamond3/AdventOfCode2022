using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day7 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day7)}.txt";
    private Node tree = new();
    private long sum = 0;

    public string Solve()
    {
        try
        {
            using var sr = new StreamReader(fileName);
            var currentNode = tree;

            while (sr.ReadLine() is string str)
            {
                var split = str.Split([' ']);

                var command = split switch
                {
                ["$", "cd", ".."] => Commands.Return,
                ["$", "cd", _] => Commands.CD,
                ["$", "ls"] => Commands.LS,
                ["dir", _] => Commands.DIR,
                    _ => Commands.Other
                };

                if (command == Commands.CD && split[2] != "/")
                {
                    currentNode = currentNode.Children[split[2]];
                }
                else if (command == Commands.DIR)
                {
                    var newNode = new Node()
                    {
                        Name = split[1],
                        Parent = currentNode
                    };

                    currentNode.Children.Add(newNode.Name, newNode);
                }
                else if (command == Commands.LS)
                {
                }
                else if (command == Commands.Return)
                {
                    currentNode.Parent.Sum += currentNode.Sum;

                    if (currentNode.Sum <= 100000L)
                    {
                        sum += currentNode.Sum;
                    }

                    currentNode = currentNode.Parent;
                }
                else if (command == Commands.Other)
                {
                    currentNode.Sum += long.Parse(split[0]);
                }
            }

            if (currentNode.Sum <= 100000L)
            {
                sum += currentNode.Sum;
            }

            currentNode.Parent.Sum += currentNode.Sum;
            currentNode = currentNode.Parent;

            if (currentNode.Sum <= 100000L)
            {
                sum += currentNode.Sum;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }

        return sum.ToString();
    }
}

public enum Commands
{
    CD, LS, DIR, Return, Other
}

public class Node
{
    public string Name = "/";
    public Node? Parent = null;
    public Dictionary<string, Node> Children = [];
    public long Sum = 0;
}
using AdventOfCode2022.Utils;

namespace AdventOfCode2022.Puzzles;

public class Day7 : ISolver
{
    private string fileName = $"Inputs/{nameof(Day7)}.txt";
    private Node tree = new();
    private long sum = 0;
    private long neededSystemSpace = 30000000L;
    private long fileSystem = 70000000L;

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
                    currentNode = currentNode.Parent;
                }
                else if (command == Commands.Other)
                {
                    currentNode.Sum += long.Parse(split[0]);
                }
            }

            while (currentNode.Parent != null)
            {
                currentNode.Parent.Sum += currentNode.Sum;
                currentNode = currentNode.Parent;
            }

            var currentFreeSpace = fileSystem - currentNode.Sum;
            var neededSpace = neededSystemSpace - currentFreeSpace;

            // BFS
            var queue = new Queue<Node>();
            var lowestPossible = long.MaxValue;

            queue.Enqueue(currentNode);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (lowestPossible > node.Sum && node.Sum >= neededSpace)
                {
                    lowestPossible = node.Sum;

                    foreach (var child in node.Children.Values)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
            return lowestPossible.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }

        return "";
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
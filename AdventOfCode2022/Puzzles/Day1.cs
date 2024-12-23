﻿using AdventOfCode2022.Utils;
using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Puzzles;

public class Day1 : ISolver
{
    private string fileName = "Inputs/Day1.txt";
    private int max = 0;
    private int trioMin = 0;

    private (int x, int y, int z) topTrio;

    public string Solve()
    {
        try
        {
            using var sr = new StreamReader(fileName);
            var tempSum = 0;

            while (sr.ReadLine() is string line)
            {
                if (line.Length == 0)
                {
                    if (trioMin < tempSum)
                    {
                        topTrio = AddToTopTrio(tempSum);
                        trioMin = topTrio.x;
                    }

                    tempSum = 0;
                }
                else
                {
                    tempSum += int.Parse(line);
                }
            }

            if (trioMin < tempSum)
            {
                topTrio = AddToTopTrio(tempSum);
                trioMin = topTrio.x;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }

        return (topTrio.x + topTrio.y + topTrio.z).ToString();
    }

    private (int, int, int) AddToTopTrio(int currentVal)
    {
        var (x, y, z) = topTrio;

        if (currentVal > topTrio.z)
        {
            x = y;
            y = z;
            z = currentVal;
        }
        else if (currentVal > topTrio.y)
        {
            x = y;
            y = currentVal;
        }
        else if (currentVal > topTrio.x)
        {
            x = currentVal;
        }

        return (x, y, z);
    }
}
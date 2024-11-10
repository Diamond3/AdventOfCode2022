using System;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Program
{
    private static void Main(string[] args)
    {
        var a = SolveNQueens(9);
    }

    public static IList<IList<string>> SolveNQueens(int n)
    {
        var map = new List<IList<string>>();
        var board = new int[n][]; // 0 - empty, 1 - queen, -1 - inAtack

        for (int i = 0; i < board.Length; i++)
        {
            board[i] = new int[n];
        }

        if (n == 0)
        {
            map.Add(new List<string>());
        }

        if (n == 1)
        {
            map.Add(new List<string>(){
                new string('Q', n)
            });
        }

        var startPos = (0, -1);

        PlaceQueen(board, startPos, 0, map);

        /*foreach (var item in map)
        {
            foreach (var item2 in item)
            {
                Console.WriteLine(item2);
            }
            Console.WriteLine();
        }*/

        return map;
    }

    private static void PlaceQueen(int[][] board, (int x, int y) pos, int placedQueens, IList<IList<string>> outputMap)
    {
        if (pos.y == board.Length - 1)
        {
            if (placedQueens == board.Length)
            {
                outputMap.Add(new List<string>());
                for (int i = 0; i < board.Length; i++)
                {
                    var row = new string('.', board.Length).ToCharArray();
                    for (int j = 0; j < board.Length; j++)
                    {
                        if (board[i][j] == 1)
                        {
                            row[j] = 'Q';
                            break;
                        }
                    }
                    outputMap.Last().Add(new string(row));
                }
                //PrintBoard(board);
            }
            return;
        }

        pos.y++;

        for (int i = pos.x; i < board.Length; i++)
        {
            if (board[pos.y][i] == 0)
            {
                var newBoard = FillBoardWithAttackSquares(board, (i, pos.y));
                PlaceQueen(newBoard, (0, pos.y), placedQueens + 1, outputMap);
            }
        }
    }

    private static void PrintBoard(int[][] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board.Length; j++)
            {
                Console.Write(board[i][j] == 1 ? "Q " : board[i][j] == -1 ? "o " : ". ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private static int[][] FillBoardWithAttackSquares(int[][] prevBoard, (int x, int y) pos)
    {
        var board = new int[prevBoard.Length][];
        for (int i = 0; i < prevBoard.Length; i++)
        {
            var tempArr = new int[prevBoard[i].Length];
            for (int j = 0; j < prevBoard[i].Length; j++)
            {
                tempArr[j] = prevBoard[i][j];
            }
            board[i] = tempArr;
        }
        //PrintBoard(board);

        Array.Fill(board[pos.y], -1); // Row

        for (int i = 0; i < board.Length; i++) // Column
        {
            board[i][pos.x] = -1;
        }

        for (int i = 1; i < board.Length; i++) // Diagonal
        {
            if (pos.y + i < board.Length && pos.x + i < board.Length)
            {
                board[pos.y + i][pos.x + i] = -1;
            }
            if (pos.y - i >= 0 && pos.x - i >= 0)
            {
                board[pos.y - i][pos.x - i] = -1;
            }
            if (pos.y + i < board.Length && pos.x - i >= 0)
            {
                board[pos.y + i][pos.x - i] = -1;
            }
            if (pos.y - i >= 0 && pos.x + i < board.Length)
            {
                board[pos.y - i][pos.x + i] = -1;
            }
        }
        board[pos.y][pos.x] = 1;

        return board;
    }
}
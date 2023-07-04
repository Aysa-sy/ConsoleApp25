using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudokuchart
{
    class Program
    { 
        static void Main(string[] args)
        {
            int[,] sudokuGrid = new int[,]
            {
                { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
                { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
                { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
                { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
                { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
                { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
                { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
                { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
                { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
            };

            Sudokuchart solver = new Sudokuchart(sudokuGrid);
            if (solver.Solve())
            {
                Console.WriteLine("Sudoku solved:");
                solver.PrintGrid();
            }
            else
            {
                Console.WriteLine("No solution found!");
            }

            Console.ReadLine();
        }
    }

    class Sudokuchart
    {
        private int[,] grid;

        public Sudokuchart(int[,] initialGrid)
        {
            grid = (int[,])initialGrid.Clone();
        }

        public bool Solve()
        {
            int line, col;
            if (!FindEmptyCell(out line, out col))
            {
                return true; // Puzzle solved
            }

            for (int num = 1; num <= 9; num++)
            {
                if (IsValidMove(line, col, num))
                {
                    grid[line, col] = num;

                    if (Solve())
                    {
                        return true;
                    }

                    grid[line, col] = 0; // Backtrack
                }
            }

            return false; // No valid number can be placed
        }

        private bool FindEmptyCell(out int line, out int col)
        {
            for (line = 0; line < 9; line++)
            {
                for (col = 0; col < 9; col++)
                {
                    if (grid[line, col] == 0)
                    {
                        return true;
                    }
                }
            }

            line = -1;
            col = -1;
            return false;
        }

        private bool IsValidMove(int line, int col, int num)
        {
            return !IsInline(line, num) && !IsInColumn(col, num) && !IsInBox(line - line % 3, col - col % 3, num);
        }

        private bool IsInline(int line, int num)
        {
            for (int col = 0; col < 9; col++)
            {
                if (grid[line, col] == num)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsInColumn(int col, int num)
        {
            for (int line = 0; line < 9; line++)
            {
                if (grid[line, col] == num)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsInBox(int startline, int startCol, int num)
        {
            for (int line = 0; line < 3; line++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (grid[startline + line, startCol + col] == num)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void PrintGrid()
        {
            for (int line = 0; line < 9; line++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Console.Write(grid[line, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

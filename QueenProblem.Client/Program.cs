using System;
using ChessAlgorithms;

namespace QueenProblem.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const int boardSize = 8;

            //FindOneSolution(boardSize);

            FindAllSolutions(boardSize);

            Console.ReadKey();
        }

        private static void FindOneSolution(int boardSize)
        {
            var queen = new Queen(boardSize);
            var solution = queen.FindOneSolution();

            PrintPositions(solution, PrintType.Numeric);
        }

        private static void FindAllSolutions(int boardSize)
        {
            var queen = new Queen(boardSize);
            var solutions = queen.FindAllSolutions();

            for (int i = 0; i < solutions.Count; i++)
            {
                Console.WriteLine($"Solution #{i}:");
                PrintPositions(solutions[i], PrintType.Char);
                Console.WriteLine();
            }

            Console.WriteLine($"Found {solutions.Count} solutions.");
        }

        private static void PrintPositions(int[] positions, PrintType printType)
        {
            for (int row = 0; row < positions.Length; row++)
            {
                for (int col = 0; col < positions.Length; col++)
                {
                    if (row == positions[col])
                    {
                        Console.Write(printType == PrintType.Char ? $"{'H',3} " : $"{positions[col],3} ");
                    }
                    else
                    {
                        Console.Write($"{'-',3} ");
                    }
                }

                Console.WriteLine();
            }
        }

        private enum PrintType
        {
            Char,
            Numeric
        }
    }
}

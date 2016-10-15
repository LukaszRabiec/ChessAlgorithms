using System;
using System.Collections.Generic;
using System.Reflection;
using ChessAlgorithms;
using ChessAlgorithms.EventsHelpers;
using ChessAlgorithms.Helpers;

namespace KnightTour.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const int boardRows = 5;
            const int boardCols = 5;
            var startingPosition = new Position(2, 2);

            //FindOneSolution(boardRows, boardCols, startingPosition);

            FindAllSolution(boardRows, boardCols, startingPosition);

            Console.ReadKey();
        }

        private static void FindOneSolution(int boardRows, int boardCols, Position startingPosition)
        {
            KnightSolution solution;

            var knight = new Knight(boardRows, boardCols);

            try
            {
                solution = knight.FindSolution(startingPosition);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message} (Method: {MethodBase.GetCurrentMethod().Name})");
                return;
            }

            Console.WriteLine("Board:");
            PrintBoard(solution.MovesBoard);
            Console.WriteLine("Path:");
            PrintHistory(solution.MovesHistory);
        }

        private static void FindAllSolution(int boardRows, int boardCols, Position startingPosition)
        {
            List<KnightSolution> solutions;

            var knight = new Knight(boardRows, boardCols);
            knight.FindingProgressChanged += OnFindingProgressChanged;

            try
            {
                solutions = knight.FindAllSolutions(startingPosition);
                knight.FindingProgressChanged -= OnFindingProgressChanged;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message} (Method: {MethodBase.GetCurrentMethod().Name})");
                return;
            }

            Console.Clear();
            for (int index = 0; index < solutions.Count; index++)
            {
                Console.WriteLine($"Solution #{index}:");
                Console.WriteLine("Board:");
                PrintBoard(solutions[index].MovesBoard);
                Console.WriteLine("Path:");
                PrintHistory(solutions[index].MovesHistory);
                Console.WriteLine();
            }

            Console.WriteLine($"Found {solutions.Count} solutions.");
        }

        private static void PrintBoard(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write($"{board[i, j],3} ");
                }
                Console.WriteLine();
            }
        }

        private static void PrintHistory(List<Position> movesHistory)
        {
            Console.Write("[START] ");
            for (int index = 0; index < movesHistory.Count; index++)
            {
                Console.Write($"({movesHistory[index].X},{movesHistory[index].Y}) ");
                if (index != movesHistory.Count - 1)
                {
                    Console.Write("-> ");
                }
            }
            Console.WriteLine("[END] ");
        }

        private static void OnFindingProgressChanged(object sender, FindingProgressEventArgs e)
        {
            Console.Clear();
            Console.WriteLine($"Found {e.FindingProgress} solutions. Finding next...");
        }
    }
}

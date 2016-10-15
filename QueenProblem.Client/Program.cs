using System;
using ChessAlgorithms;

namespace QueenProblem.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const int boardSize = 8;

            var queen = new Queen(boardSize);
            var positions = queen.FindSolution();

            PrintPositions(positions);

            Console.ReadKey();
        }

        private static void PrintPositions(int[] positions)
        {
            for (int row = 0; row < positions.Length; row++)
            {
                for (int col = 0; col < positions.Length; col++)
                {
                    if (row == positions[col])
                    {
                        //Console.Write($"{'H',3} ");
                        Console.Write($"{positions[col],3} ");
                    }
                    else
                    {
                        Console.Write($"{'-',3} ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace ChessAlgorithms
{
    // Codemian Rhapsody:
    //    Is this the real code?
    //    Is this just fantasy?
    //    Caught during pushing, no escape from git history.
    //    Open your IDE, look up to this code and see...
    public class Queen
    {
        private readonly int[] _positionInColumn;
        private readonly bool[] _isFreeRow;
        private readonly bool[] _isFreeFirstDiagonal;
        private readonly bool[] _isFreeSecondDiagonal;
        private readonly int _boardSize;

        public Queen(int boardSize)
        {
            _boardSize = boardSize;
            _positionInColumn = new int[boardSize];
            _isFreeRow = Enumerable.Repeat(true, boardSize).ToArray();
            _isFreeFirstDiagonal = Enumerable.Repeat(true, 2 * boardSize - 1).ToArray();
            _isFreeSecondDiagonal = Enumerable.Repeat(true, 2 * boardSize - 1).ToArray();
        }

        public int[] FindOneSolution()
        {
            bool isFinished = false;

            TryToPlace(0, ref isFinished);

            return _positionInColumn;
        }

        public List<int[]> FindAllSolutions()
        {
            bool isFinished = false;

            var solutions = new List<int[]>();
            TryToPlaceManyTimes(0, ref isFinished, solutions);

            return solutions;
        }

        private void TryToPlace(int iteration, ref bool isFinished)
        {
            for (int positionIndex = 0; positionIndex < _boardSize && !isFinished; positionIndex++)
            {
                isFinished = false;
                int firstDiagonalIndex = iteration + positionIndex;
                int secondDiagonalIndex = iteration - positionIndex + _boardSize - 1;

                if (CanPlaceQueen(positionIndex, firstDiagonalIndex, secondDiagonalIndex))
                {
                    PlaceQueenOnField(iteration, positionIndex);
                    SetRowAndDiagonals(positionIndex, firstDiagonalIndex, secondDiagonalIndex, false);

                    if (iteration < _boardSize - 1)
                    {
                        TryToPlace(iteration + 1, ref isFinished);

                        if (!isFinished)
                        {
                            SetRowAndDiagonals(positionIndex, firstDiagonalIndex, secondDiagonalIndex, true);
                        }
                    }
                    else
                    {
                        isFinished = true;
                    }
                }
            }
        }

        private void TryToPlaceManyTimes(int iteration, ref bool isFinished, List<int[]> solutions)
        {
            for (int positionIndex = 0; positionIndex < _boardSize && !isFinished; positionIndex++)
            {
                isFinished = false;
                int firstDiagonalIndex = iteration + positionIndex;
                int secondDiagonalIndex = iteration - positionIndex + _boardSize - 1;

                if (CanPlaceQueen(positionIndex, firstDiagonalIndex, secondDiagonalIndex))
                {
                    PlaceQueenOnField(iteration, positionIndex);
                    SetRowAndDiagonals(positionIndex, firstDiagonalIndex, secondDiagonalIndex, false);

                    if (iteration < _boardSize - 1)
                    {
                        TryToPlaceManyTimes(iteration + 1, ref isFinished, solutions);

                        if (!isFinished)
                        {
                            SetRowAndDiagonals(positionIndex, firstDiagonalIndex, secondDiagonalIndex, true);
                        }
                    }
                    else
                    {
                        solutions.Add((int[])_positionInColumn.Clone());
                        _positionInColumn[iteration] = 0;
                        SetRowAndDiagonals(positionIndex, firstDiagonalIndex, secondDiagonalIndex, true);
                    }
                }
            }
        }

        private bool CanPlaceQueen(int positionIndex, int firstDiagonalIndex, int secondDiagonalIndex)
        {
            return _isFreeRow[positionIndex]
                   && _isFreeFirstDiagonal[firstDiagonalIndex]
                   && _isFreeSecondDiagonal[secondDiagonalIndex];
        }

        private void PlaceQueenOnField(int iteration, int positionIndex)
        {
            _positionInColumn[iteration] = positionIndex;
        }

        private void SetRowAndDiagonals(int positionIndex, int firstDiagonalIndex, int secondDiagonalIndex, bool setValue)
        {
            _isFreeRow[positionIndex] = setValue;
            _isFreeFirstDiagonal[firstDiagonalIndex] = setValue;
            _isFreeSecondDiagonal[secondDiagonalIndex] = setValue;
        }
    }
}

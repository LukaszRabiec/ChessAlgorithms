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
        private int[] _positionInColumn;
        private bool[] _isFreeRow;
        private bool[] _isFreeFirstDiagonal;
        private bool[] _isFreeSecondDiagonal;
        private int _boardSize;

        public Queen(int boardSize)
        {
            _boardSize = boardSize;
            _positionInColumn = new int[boardSize];
            _isFreeRow = Enumerable.Repeat(true, boardSize).ToArray();
            _isFreeFirstDiagonal = Enumerable.Repeat(true, 2 * boardSize - 1).ToArray();
            _isFreeSecondDiagonal = Enumerable.Repeat(true, 2 * boardSize - 1).ToArray();
        }

        public int[] FindSolution()
        {
            bool isFinished = false;

            TryToPlace(0, ref isFinished);

            return _positionInColumn;
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

                    if (iteration < _boardSize)
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

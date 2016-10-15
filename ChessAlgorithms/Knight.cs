using System;
using System.Collections.Generic;
using ChessAlgorithms.EventsHelpers;
using ChessAlgorithms.Helpers;
using ChessAlgorithms.Utility;

namespace ChessAlgorithms
{
    public class Knight
    {
        public event Delegates.FindingProgressChangedEventHandler FindingProgressChanged;

        private readonly int _chessboardSize;
        private readonly List<Position> _knightMovements;
        private readonly List<Position> _movesHistory;
        private readonly int[,] _movesBoard;

        public Knight(int chessboardSize)
        {
            _chessboardSize = chessboardSize;
            _knightMovements = InitializeKnightMovements();
            _movesHistory = new List<Position>();
            _movesBoard = new int[chessboardSize, chessboardSize];
        }

        public KnightSolution FindSolution(Position startingPosition)
        {
            const int firstStep = 1;

            ClearMovementsHistory();
            SaveMoveIntoHistory(startingPosition, firstStep);

            bool isFinished = false;

            TryToJump(firstStep + 1, startingPosition, ref isFinished);

            if (isFinished)
            {
                return new KnightSolution(_movesBoard, _movesHistory);
            }

            throw new InvalidOperationException("No solution.");
        }

        public List<KnightSolution> FindAllSolutions(Position startingPosition)
        {
            const int firstStep = 1;

            ClearMovementsHistory();
            SaveMoveIntoHistory(startingPosition, firstStep);

            bool isFinished = false;
            var solutions = new List<KnightSolution>();

            TryToJumpManyTimes(firstStep + 1, startingPosition, ref isFinished, solutions);

            if (isFinished)
            {
                return solutions;
            }

            throw new InvalidOperationException("No solutions.");
        }

        private void TryToJump(int iteration, Position position, ref bool isFinished)
        {
            int movementsIterator = 0;

            do
            {
                var newPosition = GetNewPosition(position, movementsIterator);

                if (MoveIsAvailable(newPosition))
                {
                    SaveMoveIntoHistory(newPosition, iteration);

                    if (ThereAreFreeFieldsOnChessboard(iteration))
                    {
                        TryToJump(iteration + 1, newPosition, ref isFinished);

                        if (!isFinished)
                        {
                            RemoveMoveFromHistory(newPosition);
                        }
                    }
                    else
                    {
                        isFinished = true;
                    }
                }

                movementsIterator++;
            } while (!isFinished && CanMove(movementsIterator));
        }

        private void TryToJumpManyTimes(int iteration, Position position, ref bool isFinished, List<KnightSolution> solutions)
        {
            int movementsIterator = 0;

            do
            {
                var newPosition = GetNewPosition(position, movementsIterator);

                if (MoveIsAvailable(newPosition))
                {
                    SaveMoveIntoHistory(newPosition, iteration);

                    if (ThereAreFreeFieldsOnChessboard(iteration))
                    {
                        TryToJumpManyTimes(iteration + 1, newPosition, ref isFinished, solutions);
                    }
                    else
                    {
                        isFinished = true;

                        solutions.Add(new KnightSolution(_movesBoard, _movesHistory));
                        OnFindingProgressChanged(solutions.Count);
                    }

                    RemoveMoveFromHistory(newPosition);
                }

                movementsIterator++;
            } while (CanMove(movementsIterator));
        }

        private void ClearMovementsHistory()
        {
            _movesHistory.Clear();

            for (int row = 0; row < _movesBoard.GetLength(0); row++)
            {
                for (int col = 0; col < _movesBoard.GetLength(1); col++)
                {
                    _movesBoard[row, col] = 0;
                }
            }
        }

        private List<Position> InitializeKnightMovements()
        {
            return new List<Position>
            {
                new Position(2, 1),
                new Position(1, 2),
                new Position(-1, 2),
                new Position(-2, 1),
                new Position(-2, -1),
                new Position(-1, -2),
                new Position(1, -2),
                new Position(2, -1)
            };
        }

        private Position GetNewPosition(Position position, int movementsIterator)
        {
            return new Position
            {
                X = position.X + _knightMovements[movementsIterator].X,
                Y = position.Y + _knightMovements[movementsIterator].Y
            };
        }

        private bool MoveIsAvailable(Position newPosition)
        {
            return CoordIsOnChessboard(newPosition.X)
                && CoordIsOnChessboard(newPosition.Y)
                && FieldIsFree(newPosition);
        }

        private bool CoordIsOnChessboard(int newPositionCoord)
        {
            return (newPositionCoord >= 0) && (newPositionCoord < _chessboardSize);
        }

        private bool FieldIsFree(Position newPosition)
        {
            return _movesBoard[newPosition.X, newPosition.Y] == 0;
        }

        private void SaveMoveIntoHistory(Position move, int iteration)
        {
            _movesHistory.Add(move);
            _movesBoard[move.X, move.Y] = iteration;
        }

        private bool ThereAreFreeFieldsOnChessboard(int iteration)
        {
            return iteration < _movesBoard.Length;
        }

        private void RemoveMoveFromHistory(Position move)
        {
            _movesHistory.RemoveAt(_movesHistory.Count - 1);
            _movesBoard[move.X, move.Y] = 0;
        }

        private bool CanMove(int movementsIterator)
        {
            return movementsIterator < _knightMovements.Count;
        }

        protected virtual void OnFindingProgressChanged(int solutionsCounter)
        {
            FindingProgressChanged?.Invoke(this, new FindingProgressEventArgs(solutionsCounter));
        }
    }
}

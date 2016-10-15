using System.Collections.Generic;

namespace ChessAlgorithms.Helpers
{
    public class KnightSolution
    {
        public int[,] MovesBoard { get; private set; }
        public List<Position> MovesHistory { get; private set; }

        public KnightSolution(int[,] movesBoard, List<Position> movesHistory)
        {
            MovesBoard = (int[,])movesBoard.Clone();
            MovesHistory = DeepCopyOfList(movesHistory);
        }

        private List<Position> DeepCopyOfList(List<Position> movesHistory)
        {
            var history = new List<Position>();

            foreach (var movePosition in movesHistory)
            {
                history.Add(new Position(movePosition.X, movePosition.Y));
            }

            return history;
        }
    }
}

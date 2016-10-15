using System;

namespace ChessAlgorithms.EventsHelpers
{
    public class FindingProgressEventArgs : EventArgs
    {
        public int FindingProgress { get; private set; }

        public FindingProgressEventArgs(int findingProgress)
        {
            FindingProgress = findingProgress;
        }
    }
}

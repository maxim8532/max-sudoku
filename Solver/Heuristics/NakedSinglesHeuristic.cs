using MaxSudoku.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver.Heuristics
{
    /// <summary>
    /// Implements the Naked Singles heuristic.
    /// </summary>
    public class NakedSinglesHeuristic : Heuristic
    {
        public NakedSinglesHeuristic(SudokuBoard board, MaskManager maskManager, MovesManager movesManager)
            : base(board, maskManager, movesManager)
        {
        }

        /// <summary>
        /// Applies the naked singles heuristic.
        /// It first scans all empty cells, then processes affected cells with a queue.
        /// </summary>
        public override bool Apply()
        {
            bool progressMade = false;

            /* Initial full board scan. */
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (TryMove(row, col))
                        progressMade = true;
                }
            }

            /* Process the cells in the queue. */
            while (cellsToProcess.Count > 0)
            {
                var (row, col) = cellsToProcess.Dequeue();
                if (TryMove(row, col))
                    progressMade = true;
            }

            return progressMade;
        }

        /// <summary>
        /// Tries to make a naked singles move on a specific cell.
        /// </summary>
        private bool TryMove(int row, int col)
        {
            if (board.GetCell(row, col) == 0)
            {
                if (MakeMove(row, col))
                {
                    AddAffectedCells(row, col);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Processes an individual cell: if it is empty and has exactly one available option,
        /// places that digit. 
        /// </summary>
        /// <returns>Returns true if a digit was placed.</returns>
        private bool MakeMove(int row, int col)
        {
            int available = maskManager.GetAvailableDigits(row, col);
            if (available == 0)
                return false;

            if (BitOperations.PopCount((uint)available) == 1)
            /* If there's only 1 available option. */
            {
                int digit = BitOperations.TrailingZeroCount(available) + 1;

                /* Record the move before placing. */
                movesManager.RecordMove(new Move(row, col, 0, digit));

                board.SetCell(row, col, digit);
                maskManager.UpdateMasks(row, col, digit, isPlacing: true);
                return true;
            }
            return false;
        }
    }
}

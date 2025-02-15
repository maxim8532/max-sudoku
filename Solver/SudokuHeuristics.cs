using MaxSudoku.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver
{
    public class SudokuHeuristics
    {
        private readonly SudokuBoard board;
        private readonly MaskManager maskManager;
        private readonly MovesManager movesManager;
        private readonly int boardSize;
        private readonly Queue<(int row, int col)> cellsToProcess;

        public SudokuHeuristics(SudokuBoard board, MaskManager maskManager, MovesManager movesManager)
        {
            this.board = board;
            this.maskManager = maskManager;
            this.movesManager = movesManager;
            boardSize = board.BoardSize;
            cellsToProcess = new Queue<(int row, int col)>();
        }

        /// <summary>
        /// Applies the naked singles heuristic to the board.
        /// It first scans all empty cells, processing each one. Then it processes cells
        /// that are affected by changes (neighbors) using a queue.
        /// For each cell with exactly one candidate, the digit is placed,
        /// and affected neighbors are enqueued for reprocessing.
        /// </summary>
        /// <returns>Returns true if at least one cell was filled.</returns>
        public bool ApplyNakedSingles()
        {
            bool progressMade = false;

            // Initial full board scan.
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (board.GetCell(row, col) == 0)
                    {
                        /* TODO: Add queue to track cells that should be checked.
                         * if (ProcessCell(row, col))
                        {
                            progressMade = true;
                            EnqueueAffectedCells(row, col);
                        } */
                    }
                }
            }
            return progressMade;
        }


        /// <summary>
        /// Processes an individual cell: if it is empty and has exactly one candidate,
        /// places that digit. 
        /// </summary>
        /// <returns>Returns true if a digit was placed.</returns>
        private bool makeMove(int row, int col)
        {
            int available = maskManager.GetAvailableDigits(row, col);
            if (available == 0)
                return false;

            if (BitOperations.PopCount((uint)available) == 1)
            {
                int digit = BitOperations.TrailingZeroCount(available) + 1;
                // Record the move before placing.
                movesManager.RecordMove(new Move(row, col, 0, digit));
                board.SetCell(row, col, digit);
                maskManager.UpdateMasks(row, col, digit, isPlacing: true);
                return true;
            }
            return false;
        }

    }

}

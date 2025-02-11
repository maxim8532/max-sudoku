using MaxSudoku.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver
{
    public class SudokuHeuristics
    {
        private readonly SudokuBoard board;
        private readonly MaskManager maskManager;
        private readonly int boardSize;

        public SudokuHeuristics(SudokuBoard board, MaskManager maskManager)
        {
            this.board = board;
            this.maskManager = maskManager;
            boardSize = board.BoardSize;
        }

        /// <summary>
        /// Applies the naked singles heuristic to the board.
        /// It first scans all empty cells, processing each one. Then it processes cells
        /// that are affected by changes (neighbors) via a queue.
        /// For each cell with exactly one candidate, the digit is placed,
        /// and affected neighbors are enqueued for reprocessing.
        /// Returns true if at least one cell was filled.
        /// </summary>
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
    }
    
}

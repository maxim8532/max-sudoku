using MaxSudoku.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver
{
    /// <summary>
    /// Abstract base class for all Sudoku heuristics.
    /// Provides common fields and helper methods.
    /// </summary>
    public abstract class Heuristic : IHeuristicApplier
    {
        protected readonly SudokuBoard board;
        protected readonly MaskManager maskManager;
        protected readonly MovesManager movesManager;
        protected readonly int boardSize;
        protected readonly Queue<(int row, int col)> cellsToProcess;

        public Heuristic(SudokuBoard board, MaskManager maskManager, MovesManager movesManager)
        {
            this.board = board;
            this.maskManager = maskManager;
            this.movesManager = movesManager;
            boardSize = board.BoardSize;
            cellsToProcess = new Queue<(int row, int col)>();
        }

        /// <summary>
        /// Applies the heuristic to the board.
        /// Returns true if at least one move was made.
        /// </summary>
        public abstract bool Apply();

        /// <summary>
        /// Adds the affected cells to the queue,
        /// by adding cells from the same row, col, and block of the cell that
        /// the digit was placed in.
        /// </summary>
        /// <param name="row">Row of the placed cell.</param>
        /// <param name="col">Col of the placed cell</param>
        protected void AddAffectedCells(int row, int col)
        {
            int blockSize = (int)Math.Sqrt(boardSize);
            int blockStartRow = (row / blockSize) * blockSize;
            int blockStartCol = (col / blockSize) * blockSize;

            /* Add the the affected cells from the same row, col, and block. */

            for (int c = 0; c < boardSize; c++)
            {
                if (c != col && board.GetCell(row, c) == 0)
                    cellsToProcess.Enqueue((row, c));
            }

            for (int r = 0; r < boardSize; r++)
            {
                if (r != row && board.GetCell(r, col) == 0)
                    cellsToProcess.Enqueue((r, col));
            }

            for (int r = 0; r < blockSize; r++)
            {
                for (int c = 0; c < blockSize; c++)
                {
                    int currentRow = blockStartRow + r;
                    int currentCol = blockStartCol + c;
                    if ((currentRow != row || currentCol != col) && board.GetCell(currentRow, currentCol) == 0)
                        cellsToProcess.Enqueue((currentRow, currentCol));
                }
            }

        }
    }
}

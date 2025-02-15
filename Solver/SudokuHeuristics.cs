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

            /* Initial full board scan. */
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (TryMove(row,col))
                        progressMade = true;
                }
            }

            /* Make moves on the cells in the queue. */
            while (cellsToProcess.Count > 0)
            {
                var (row, col) = cellsToProcess.Dequeue();
                if (TryMove(row, col))
                    progressMade = true;

            }
            return progressMade;
        }

        /// <summary>
        /// Tries to make a heuristic move.
        /// </summary>
        /// <param name="row">The row of the cell to check if a move can be made</param>
        /// <param name="col">The column of the cell to check if a move can be made</param>
        /// <returns>Return true if a move attempt was successful.</returns>
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
            // If there's only 1 available option.
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

        /// <summary>
        /// Adds the affected cells to the queue,
        /// by adding cells from the same row, col, and block of the cell that
        /// the digit was placed in.
        /// </summary>
        /// <param name="row">Row of the placed cell.</param>
        /// <param name="col">Col of the placed cell</param>
        private void AddAffectedCells(int row, int col)
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

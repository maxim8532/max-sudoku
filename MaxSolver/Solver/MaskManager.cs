using MaxSudoku.MaxSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.MaxSolver.Solver
{
    /// <summary>
    /// A mask manager that conatins methods that involve masks.
    /// </summary>
    public class MaskManager
    {
        private readonly int boardSize;
        private readonly int blockSize;
        private readonly int fullMask;  // A mask with all the bits set (will be used for complement).

        /* Bit masks for each row, column, and block.
           Bit (n-1) is set if digit n is already used. */
        private readonly int[] rowMask;
        private readonly int[] colMask;
        private readonly int[] blockMask;


        /// <summary>
        /// Constructor for a new instance of the MaskManager class.
        /// </summary>
        /// <param name="board">The Sudoku board to solve.</param>
        /// <exception cref="InvalidInputException">Thrown when board size is not a perfect square.</exception>
        public MaskManager(int boardSize)
        {
            this.boardSize = boardSize;
            blockSize = (int)Math.Sqrt(boardSize);
            fullMask = (1 << boardSize) - 1;

            rowMask = new int[boardSize];
            colMask = new int[boardSize];
            blockMask = new int[boardSize];
        }


        /// <summary>
        /// Calculates the block index for a given cell position.
        /// </summary>
        /// <param name="row">Row index of the cell</param>
        /// <param name="col">Column index of the cell</param>
        /// <returns>Index of the block containing the cell</returns>
        public int GetBlockIndex(int row, int col) =>
            row / blockSize * blockSize + col / blockSize;


        /// <summary>
        /// Updates the masks when placing or removing a digit.
        /// </summary>
        /// <param name="row">Row index of the cell</param>
        /// <param name="col">Column index of the cell</param>
        /// <param name="digit">The digit being placed or removed</param>
        /// <param name="isPlacing">True if placing a digit, false if removing</param>
        public void UpdateMasks(int row, int col, int digit, bool isPlacing)
        {
            int bit = 1 << digit - 1;
            if (isPlacing)
            {
                rowMask[row] |= bit;
                colMask[col] |= bit;
                blockMask[GetBlockIndex(row, col)] |= bit;
            }
            else  // Removing a digit.
            {
                rowMask[row] &= ~bit;
                colMask[col] &= ~bit;
                blockMask[GetBlockIndex(row, col)] &= ~bit;
            }
        }


        /// <summary>
        /// Updates masks from an existing board state.
        /// </summary>
        /// <param name="board">The Sudoku board to initialize the masks from.</param>
        public void UpdateFromBoard(SudokuBoard board)
        {
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    int num = board.GetCell(row, col);
                    if (num != 0)
                    {
                        UpdateMasks(row, col, num, isPlacing: true);
                    }
                }
            }
        }


        /// <summary>
        /// Gets the mask of available digits for a cell.
        /// </summary>
        /// <param name="row">Row index of the cell</param>
        /// <param name="col">Column index of the cell</param>
        /// <returns>Bit mask where each set bit represents an available digit</returns>
        public int GetAvailableDigits(int row, int col)
        {
            int used = rowMask[row] | colMask[col] | blockMask[GetBlockIndex(row, col)];
            return fullMask & ~used;
        }
    }

}

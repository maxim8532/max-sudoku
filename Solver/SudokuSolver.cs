using MaxSudoku.Board;
using MaxSudoku.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver
{
    public class SudokuSolver
    {
        private int boardSize;
        private int blockSize;
        private int fullMask;  // A mask with all the bits set (will be used for complement).

        /* Bit masks for each row, column, and block.
           Bit (n-1) is set if digit n is already used. */
        private int[] rowMask;
        private int[] colMask;
        private int[] blockMask;

        private readonly SudokuBoard board;

        /// <summary>
        /// Constructor for a new instance of the SudokuSolver class.
        /// </summary>
        /// <param name="board">The Sudoku board to solve.</param>
        /// <exception cref="InvalidInputException">Thrown when board size is not a perfect square.</exception>
        public SudokuSolver(SudokuBoard board)
        {
            this.board = board;
            boardSize = board.BoardSize;

            // Validate board size
            blockSize = (int)Math.Sqrt(boardSize);
            if (blockSize * blockSize != boardSize)
                throw new InvalidInputException("Board size must be a perfect square (for example: 9x9, 16x16).");

            fullMask = (1 << boardSize) - 1;

            rowMask = new int[boardSize];
            colMask = new int[boardSize];
            blockMask = new int[boardSize];
        }
    }
}

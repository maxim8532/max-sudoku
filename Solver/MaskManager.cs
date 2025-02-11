﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver
{
    internal class MaskManager
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
    }
    
}

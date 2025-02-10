using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku
{
    internal class SudokuBoard : BoardGame
    {
        private const int MIN_CELL_VALUE = 1;
        private SudokuValidator validator;

        /// <summary>
        /// Gets the size of the Sudoku board (rows = cols = boardSize).
        /// </summary>
        public int BoardSize => rows;


        /// <summary>
        /// Constructor that creates a Sudoku board of size x size, using a SudokuValidator for checks.
        /// </summary>
        /// <param name="size">Dimension of the board.</param>
        /// <param name="validator">An instance of SudokuValidator.</param>
        public SudokuBoard(int boardSize, SudokuValidator validator) : base(boardSize, boardSize)
        {
            this.validator = validator;
        }
    }

}

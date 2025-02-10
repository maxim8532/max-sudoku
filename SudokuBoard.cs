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
        /// Constructor that creates a Sudoku board of boardSize x boardSize, using a SudokuValidator for checks.
        /// </summary>
        /// <param name="boardSize">Dimension of the board.</param>
        /// <param name="validator">An instance of SudokuValidator.</param>
        public SudokuBoard(int boardSize, SudokuValidator validator) : base(boardSize, boardSize)
        {
            this.validator = validator;
        }

        public override void FillBoard(string data)
        {
            throw new NotImplementedException();
        }

        public override bool IsFull()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts '.' or '0' to 0, else gets the numeric ASCII value of the char.
        /// </summary>
        /// <param name="c">char from a string.</param>
        private int ParseChar(char c)
        {
            if (c == '.' || c == '0')
                return 0;
            return c - '0';
        }
    }

}

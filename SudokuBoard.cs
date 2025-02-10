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

        /// <summary>
        /// Fills the board from a data string after parsing it. 
        /// Also checks string length, allowed chars, and numeric range.
        /// </summary>
        /// <param name="data">String of length BoardSize*BoardSize containing Sudoku puzzle chars.</param>
        public override void FillBoard(string data)
        {
            if (!validator.ValidateStringSize(data, BoardSize, BoardSize))
                throw new InvalidInputException($"Input length must be {BoardSize * BoardSize} for a {BoardSize}x{BoardSize} Sudoku board.");

            if (!validator.IsValidString(data))
                throw new InvalidInputException("Input contains invalid characters for a Sudoku puzzle " +
                    "(only digits, ASCII values within range, '.' or '0').");

            int inputIndex = 0;
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    char ch = data[inputIndex++];
                    int cellValue = ParseChar(ch);

                    if (cellValue != 0)
                    {
                        if (!validator.ValidateCellRange(cellValue, MIN_CELL_VALUE, BoardSize))
                            throw new InvalidInputException($"Cell char '{ch}' - {cellValue} is out of [{MIN_CELL_VALUE} - {BoardSize}] range.");
                    }

                    board[row, col] = cellValue;
                }
            }
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

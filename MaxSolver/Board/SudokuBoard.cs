using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MaxSudoku.MaxSolver.CustomExceptions;

namespace MaxSudoku.MaxSolver.Board
{
    /// <summary>
    /// Represents a sudoku board.
    /// </summary>
    public class SudokuBoard : BoardGame
    {
        public const int MIN_CELL_VALUE = 1;
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
        /// <exception cref="InvalidInputException">
        /// Thrown when the data string contains chars that are invalid.
        /// </exception>
        public override void FillBoard(string data)
        {
            if (!validator.ValidateStringSize(data, BoardSize, BoardSize))
                throw new InvalidInputException($"Input length must be {BoardSize * BoardSize} for a {BoardSize}x{BoardSize} Sudoku board.");

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

        /// <summary>
        /// Checks if the board is fully filled (no zero cells).
        /// </summary>
        /// <returns>
        /// True if no cell is zero, otherwise false.
        /// </returns>
        public override bool IsFull()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (board[row, col] == 0)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Validates the board by checking rows, columns, and blocks
        /// throwing InvalidSudokuBoardException if a rule is violated.
        /// </summary>
        /// <exception cref="InvalidBoardException">
        /// Thrown when the board doesn't follow the rules of the Sudoku.
        /// </exception>
        public void ValidateBoard()
        {
            if (!validator.ValidateRows(board, BoardSize, BoardSize))
                throw new InvalidBoardException("Row validation failed, duplicate cell values found within the same row.");

            if (!validator.ValidateColumns(board, BoardSize, BoardSize))
                throw new InvalidBoardException("Column validation failed, duplicate cell values found within the same column.");

            if (!validator.ValidateBlocks(board, BoardSize, BoardSize))
                throw new InvalidBoardException("Block validation failed, duplicate cell values found. within the same block.");
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

        /// <summary>
        /// Overrides ToString and converts the board int cells to char and combines them to a string.
        /// </summary>
        /// <returns>String thar represents the cells in the board</returns>
        public override string ToString()
        {
            string stringBoard = "";
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    int cellValue = board[i, j];
                    stringBoard += (char)(cellValue + '0');
                }
            }
            return stringBoard;
        }
    }

}

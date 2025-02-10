using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku
{
    public class SudokuValidator : BoardValidator
    {
        /// <summary>
        /// Checks if the given data string contains only valid characters (digits, '.' or '0').
        /// </summary>
        /// <param name="data">String representing board data.</param>
        /// <returns>True if all characters are valid for Sudoku. Otherwise, false.</returns>
        public bool IsValidString(string data)
        {
            foreach (char c in data)
            {
                if (!(char.IsDigit(c) || c == '.' || c == '0'))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the given row and column are within the board's valid range.
        /// </summary>
        /// <param name="row">Row index to check.</param>
        /// <param name="col">Column index to check.</param>
        /// <param name="rows">Number of rows in the board.</param>
        /// <param name="cols">Number of columns in the board.</param>
        /// <returns>True if (row, col) is within valid range. Otherwise, false.</returns>
        public bool ValidateCellsRange(int row, int col, int rows, int cols)
        {
            if (row < 0 || row >= rows) return false;
            if (col < 0 || col >= cols) return false;
            return true;
        }

        /// <summary>
        /// Checks if the data string's length is at the right length.
        /// </summary>
        /// <param name="data">String representing board data.</param>
        /// <param name="rows">Number of rows in the board.</param>
        /// <param name="cols">Number of columns in the board.</param>
        public bool ValidateStringSize(string data, int rows, int cols)
        {
            return data.Length == rows * cols;
        }

        /// <summary>
        /// Checks rows to see if they follow Sudoku rules (no duplicate non-zero values).
        /// </summary>
        /// <param name="board">Matrix representing the board.</param>
        /// <param name="rows">Number of rows.</param>
        /// <param name="cols">Number of columns.</param>
        /// <returns>True if no row contains duplicates. Otherwise, false.</returns>
        public bool ValidateRows(int[,] board, int rows, int cols)
        {
            for (int r = 0; r < rows; r++)
            {
                var seen = new HashSet<int>();
                for (int c = 0; c < cols; c++)
                {
                    int val = board[r, c];
                    if (val != 0 && !seen.Add(val))
                        // if a number exists when trying to add it, will return false.
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks columns to see if they follow Sudoku rules (no duplicate non-zero values).
        /// </summary>
        /// <param name="board">Matrix representing the board.</param>
        /// <param name="rows">Number of rows.</param>
        /// <param name="cols">Number of columns.</param>
        /// <returns>True if no column contains duplicates. Otherwise, false.</returns>
        public bool ValidateColumns(int[,] board, int rows, int cols)
        {
            for (int c = 0; c < cols; c++)
            {
                var seen = new HashSet<int>();
                for (int r = 0; r < rows; r++)
                {
                    int val = board[r, c];
                    if (val != 0 && !seen.Add(val))
                        // if a number exists when trying to add it, will return false.
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks sub-squares to see if they follow Sudoku rules (no duplicate non-zero values).
        /// </summary>
        /// <param name="board">2D integer array representing the board.</param>
        /// <param name="rows">Number of rows.</param>
        /// <param name="cols">Number of columns.</param>
        /// <returns>True if all sub-squares have no duplicates; otherwise, false.</returns>
        public bool ValidateBoxes(int[,] board, int rows, int cols)
        {
            // to keep things generic i included both the rows and columns.
            // (if some funny guy will decide to do a non-square sudoku).
            if (rows != cols) return true;
            double sqrt = Math.Sqrt(rows);
            if (sqrt != (int)sqrt) return true;
            int subSize = (int)sqrt;

            for (int boxRow = 0; boxRow < subSize; boxRow++)
            {
                for (int boxCol = 0; boxCol < subSize; boxCol++)
                {
                    var seen = new HashSet<int>();
                    int startRow = boxRow * subSize;
                    int startCol = boxCol * subSize;
                    for (int row = startRow; row < startRow + subSize; row++)
                    {
                        for (int col = startCol; col < startCol + subSize; col++)
                        {
                            int val = board[row, col];
                            if (val != 0 && !seen.Add(val))
                                // if a number exists when trying to add it, will return false.
                                return false;
                        }
                    }
                }
            }
            return true;
        }

    }
}

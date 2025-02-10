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
    }
}

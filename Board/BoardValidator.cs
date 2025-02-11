using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Board
{
    /// <summary>
    /// Defines validation methods for row/column-based boards.
    /// </summary>
    public interface BoardValidator
    {
        /// <summary>
        /// Checks if the given data string contains only valid characters. 
        /// </summary>
        bool IsValidString(string data);

        /// <summary>
        /// Checks if the given cell value are within the board's valid range.
        /// </summary>
        bool ValidateCellRange(int cellValue, int minValue, int maxValue);

        /// <summary>
        /// Checks if the given data string size is correct for filling a board of specified size. 
        /// </summary>
        bool ValidateStringSize(string data, int rows, int cols);

        /// <summary>
        /// Checks rows to see if they follow the game rules.
        /// </summary>
        bool ValidateRows(int[,] board, int rows, int cols);

        /// <summary>
        /// Checks columns to see if they follow the game rules.
        /// </summary>
        bool ValidateColumns(int[,] board, int rows, int cols);
    }
}

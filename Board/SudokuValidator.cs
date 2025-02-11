using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Board
{
    public class SudokuValidator : IBoardValidator
    {

        /// <summary>
        /// Checks if the given cell value are within the board's valid range.
        /// </summary>
        /// <param name="cellValue">Cell value to check.</param>
        /// <param name="minValue">Minimum value of </param>
        /// <param name="maxValue">Number of rows in the board.</param>
        /// <returns>True if cell is within valid range. Otherwise, false.</returns>
        public bool ValidateCellRange(int cellValue, int minValue, int maxValue)
        {
            if (cellValue < minValue || cellValue > maxValue)
            {
                return false;
            }
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
        /// Checks sub-squares(blocks) to see if they follow Sudoku rules (no duplicate non-zero values).
        /// </summary>
        /// <param name="board">2D integer array representing the board.</param>
        /// <param name="rows">Number of rows.</param>
        /// <param name="cols">Number of columns.</param>
        /// <returns>True if all sub-squares have no duplicates; otherwise, false.</returns>
        public bool ValidateBlocks(int[,] board, int rows, int cols)
        {
            // to keep things generic i included both the rows and columns.
            // (if some funny guy will decide to do a non-square sudoku).
            if (rows != cols) return true;
            double sqrt = Math.Sqrt(rows);
            if (sqrt != (int)sqrt) return true;
            int blockSize = (int)sqrt;

            for (int blockRow = 0; blockRow < blockSize; blockRow++)
            {
                for (int blockCol = 0; blockCol < blockSize; blockCol++)
                {
                    var seen = new HashSet<int>();
                    int startRow = blockRow * blockSize;
                    int startCol = blockCol * blockSize;
                    for (int row = startRow; row < startRow + blockSize; row++)
                    {
                        for (int col = startCol; col < startCol + blockSize; col++)
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

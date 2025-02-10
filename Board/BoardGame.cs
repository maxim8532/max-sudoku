using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku
{
    /// <summary>
    /// Represents a generic board game with rows, columns and a 2D array.
    /// </summary>
    public abstract class BoardGame
    {
        protected int rows;
        protected int cols;
        protected int[,] board;

        /* Getters */
        public int Rows => rows;
        public int Cols => cols;

        /* Get and Set for the board data */
        public virtual int GetCell(int row, int col) => board[row, col];
        public virtual void SetCell(int row, int col, int value)
        {
            board[row, col] = value;
        }

        /// <summary>
        /// Constructor for a generic board.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="cols">Number of columns.</param>
        protected BoardGame(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            board = new int[rows, cols];
        }

        /// <summary>
        /// Fills the board using the given data string.
        /// </summary>
        /// <param name="data">String representing the board content.</param>
        public abstract void FillBoard(string data);

        /// <summary>
        /// Checks if the board is fully filled according to its game rules.
        /// </summary>
        public abstract bool IsFull();

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku
{
    internal class SudokuBoard
    {
        private int boardSize;
        private int[,] board;

        public SudokuBoard(int boardSize)
        {
            this.boardSize = boardSize;
        }

        public void FillBoard(string sudokuString)
        {
            int row = 0;
            int col = 0;
            foreach (char c in sudokuString)
            {
                board[row, col] = (byte)(c - '0');
                col++;
                if (col == boardSize)
                {
                    col = 0;
                    row++;
                }
            }
        }

        public int GetCell(int row, int col)
        {
            return board[row, col];
        }

        public void SetCell(int row, int col, int value)
        {
            board[row, col] = value;
        }










    }

}

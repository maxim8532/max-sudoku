﻿using MaxSudoku.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver.Heuristics
{
    public class HiddenSinglesHeuristic : Heuristic
    {
        public HiddenSinglesHeuristic(SudokuBoard board, MaskManager maskManager, MovesManager movesManager)
            : base(board, maskManager, movesManager)
        {
        }

        public override bool Apply()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Applies hidden singles rule to a single row.
        /// For each digit, if it appears as an available digit in exactly one cell in the row, the digit is placed.
        /// </summary>
        /// <returns>True if any progress has been made.</returns>
        private bool ApplyHiddenSinglesRow(int row)
        {
            bool progress = false;
            for (int digit = 1; digit <= boardSize; digit++)
            {
                int count = 0;
                int targetCol = -1;
                int maskDigit = 1 << (digit - 1);
                for (int col = 0; col < boardSize; col++)
                {
                    if (board.GetCell(row, col) == 0)
                    {
                        int available = maskManager.GetAvailableDigits(row, col);
                        if ((available & maskDigit) != 0)
                        {
                            count++;
                            targetCol = col;
                        }
                    }
                }
                if (count == 1)
                {
                    movesManager.RecordMove(new Move(row, targetCol, 0, digit));
                    board.SetCell(row, targetCol, digit);
                    maskManager.UpdateMasks(row, targetCol, digit, isPlacing: true);
                    progress = true;
                }
            }
            return progress;
        }

        /// <summary>
        /// Applies hidden singles rule to a single column.
        /// For each digit, if it appears as an available digit in exactly one cell in the column, the digit is placed.
        /// </summary>
        /// <returns>True if any progress has been made.</returns>
        private bool ApplyHiddenSinglesColumn(int col)
        {
            bool progress = false;
            for (int digit = 1; digit <= boardSize; digit++)
            {
                int count = 0;
                int targetRow = -1;
                int maskDigit = 1 << (digit - 1);
                for (int row = 0; row < boardSize; row++)
                {
                    if (board.GetCell(row, col) == 0)
                    {
                        int available = maskManager.GetAvailableDigits(row, col);
                        if ((available & maskDigit) != 0)
                        {
                            count++;
                            targetRow = row;
                        }
                    }
                }
                if (count == 1)
                {
                    movesManager.RecordMove(new Move(targetRow, col, 0, digit));
                    board.SetCell(targetRow, col, digit);
                    maskManager.UpdateMasks(targetRow, col, digit, isPlacing: true);
                    progress = true;
                }
            }
            return progress;
        }

        /// <summary>
        /// Applies hidden singles rule to a single block.
        /// For each digit, if it appears as an available digit in exactly one cell in the block, the digit is placed.
        /// </summary>
        private bool ApplyHiddenSinglesBlock(int blockRow, int blockCol)
        {
            bool progress = false;
            int blockSize = (int)Math.Sqrt(boardSize);
            int startRow = blockRow * blockSize;
            int startCol = blockCol * blockSize;
            for (int digit = 1; digit <= boardSize; digit++)
            {
                int count = 0;
                int targetRow = -1, targetCol = -1;
                int maskDigit = 1 << (digit - 1);
                for (int r = startRow; r < startRow + blockSize; r++)
                {
                    for (int c = startCol; c < startCol + blockSize; c++)
                    {
                        if (board.GetCell(r, c) == 0)
                        {
                            int available = maskManager.GetAvailableDigits(r, c);
                            if ((available & maskDigit) != 0)
                            {
                                count++;
                                targetRow = r;
                                targetCol = c;
                            }
                        }
                    }
                }
                if (count == 1)
                {
                    movesManager.RecordMove(new Move(targetRow, targetCol, 0, digit));
                    board.SetCell(targetRow, targetCol, digit);
                    maskManager.UpdateMasks(targetRow, targetCol, digit, isPlacing: true);
                    progress = true;
                }
            }
            return progress;
        }
    }
}

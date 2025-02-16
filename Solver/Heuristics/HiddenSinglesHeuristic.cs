using MaxSudoku.Board;
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

    }
}

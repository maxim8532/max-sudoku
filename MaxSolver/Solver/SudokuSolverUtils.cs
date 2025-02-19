using MaxSudoku.MaxSolver.Solver.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MaxSudoku.MaxSolver.Board;

namespace MaxSudoku.MaxSolver.Solver
{
    public class SudokuSolverUtils
    {

        /// <summary>
        /// Checks if every cell has atleast 1 option,
        /// thus reducing the solving time by pruning
        /// the unsolvable branch.
        /// </summary>
        /// <returns>True if every cell has atleast 1 option.</returns>
        public static bool EveryCellHasAvailableDigitsCheck(SudokuBoard board, MaskManager maskManager, int boardSize)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board.GetCell(i, j) == 0)
                    {
                        if (maskManager.GetAvailableDigits(i, j) == 0)
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Determines the default algorithm level based on board size.
        /// </summary>
        /// <param name="boardSize">The dimension of the board.</param>
        /// <returns>The default AlgorithmLevel.</returns>
        public static AlgorithmLevel GetDefaultAlgorithmLevel(int boardSize)
        {
            return boardSize <= 16 ? AlgorithmLevel.Level2 : AlgorithmLevel.Level3;
        }

        /// <summary>
        /// Applies all of the added heuristics in order.
        /// </summary>
        public static void ApplyHeuristics(SudokuHeuristics heuristics)
        {
            bool heuristicProgress;
            do
            {
                heuristicProgress = heuristics.ApplyAll();
            }
            while (heuristicProgress);
        }

        /// <summary>
        /// Places a digit on the board and updates masks.
        /// </summary>
        public static void PlaceDigit(SudokuBoard board, MaskManager maskManager, int row, int col, int digit)
        {
            board.SetCell(row, col, digit);
            maskManager.UpdateMasks(row, col, digit, isPlacing: true);
        }

        /// <summary>
        /// Finds the empty cell with the minimum value
        /// of available digits in the board.
        /// </summary>
        /// <returns>Returns an empty cell if found (tuple). Otherwise, false</returns>
        public static (int row, int col, bool foundEmpty) FindMinimumEmptyCell(SudokuBoard board, MaskManager maskManager, int boardSize)
        {
            {
                int minCount = int.MaxValue;
                int targetRow = -1;
                int targetCol = -1;
                bool foundEmpty = false;

                for (int row = 0; row < boardSize; row++)
                {
                    for (int col = 0; col < boardSize; col++)
                    {
                        if (board.GetCell(row, col) == 0)
                        {
                            foundEmpty = true;
                            int available = maskManager.GetAvailableDigits(row, col);
                            int count = BitOperations.PopCount((uint)available);

                            /* If this cell has fewer options than the current minimum, update.*/
                            if (count < minCount)
                            {
                                minCount = count;
                                targetRow = row;
                                targetCol = col;
                            }

                            /* Exit Early if a cell has only one option.*/
                            if (minCount == 1)
                            {
                                return (targetRow, targetCol, true);
                            }
                        }
                    }
                }
                return (targetRow, targetCol, foundEmpty);
            }
        }

        /// <summary>
        /// Validates the final solution using the board's validation rules.
        /// </summary>
        public static void ValidateSolution(SudokuBoard board)
        {
            if (board is SudokuBoard sudokuBoard)
            {
                sudokuBoard.ValidateBoard();
            }
        }
    }
}

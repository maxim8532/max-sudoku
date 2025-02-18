using MaxSudoku.Board;
using MaxSudoku.CustomExceptions;
using MaxSudoku.Solver.Heuristics;
using static MaxSudoku.Solver.SudokuSolverUtils;
using MaxSudoku.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver
{
    public class SudokuSolver : IGameSolver
    {
        private readonly int boardSize;
        private readonly int blockSize;
        private readonly SudokuBoard board;
        private readonly MaskManager maskManager;
        private readonly MovesManager movesManager;
        private readonly SudokuHeuristics heuristics;

        /// <summary>
        /// Constructor for a new instance of the SudokuSolver class.
        /// </summary>
        /// <param name="board">The Sudoku board to solve.</param>
        /// <exception cref="InvalidInputException">Thrown when board size is not a perfect square.</exception>
        public SudokuSolver(SudokuBoard board)
        {
            this.board = board;
            boardSize = board.BoardSize;

            // Validate board size
            blockSize = (int)Math.Sqrt(boardSize);
            if (blockSize * blockSize != boardSize)
                throw new InvalidInputException("Board size must be a perfect square (for example: 4x4, 9x9, 16x16).");

            maskManager = new MaskManager(boardSize);
            maskManager.UpdateFromBoard(board);  // Initializes board masks.
            movesManager = new MovesManager();
            heuristics = new SudokuHeuristics();
            heuristics.AddHeuristic(new NakedSinglesHeuristic(board, maskManager, movesManager));
            heuristics.AddHeuristic(new HiddenSinglesHeuristic(board, maskManager, movesManager));
        }

        /// <summary>
        /// Attempts to solve the Sudoku puzzle using backtracking.
        /// </summary>
        /// <returns>True if a valid solution is found, false otherwise.</returns>
        public bool Solve()
        {
            ApplyHeuristics(heuristics);
            try
            {
                bool result = SolveRecursive();
                if (result)
                {
                    ValidateSolution(board);
                }
                return result;
            }
            catch (InvalidBoardException)
            {
                return false;
            }
        }

        /// <summary>
        /// Recursive solving method that uses backtracking with bit manipulations.
        /// </summary>
        private bool SolveRecursive()
        {
            if (!EveryCellHasAvailableDigitsCheck(board, maskManager, boardSize))
                return false;

            var (row, col, foundEmpty) = FindMinimumEmptyCell(board, maskManager, boardSize);
            if (!foundEmpty)
                return true;

            int availableDigitis = maskManager.GetAvailableDigits(row, col);
            int checkpoint = movesManager.Count; //Checkpoint when backtracking.

            while (availableDigitis != 0)
            {
                /* Extract least significant 1 bit. */
                int bit = availableDigitis & -availableDigitis;

                /* Clear the least significant 1 bit from mask.*/
                availableDigitis &= availableDigitis - 1;

                /* Counts 0 bits from right till the first 1 bit (trailing zeros),
                   Then adds 1 to get the digit. */
                int digit = BitOperations.TrailingZeroCount(bit) + 1;

                movesManager.RecordMove(new Move(row, col, 0, digit));
                PlaceDigit(board, maskManager, row, col, digit);

                /* Try heuristic moves again. */
                ApplyHeuristics(heuristics);

                if (SolveRecursive())
                    return true;

                /* If the branch fails, backtrack to checkpoint. */
                movesManager.UndoMoves(checkpoint, board, maskManager);
            }
            return false;
        }
    }
}

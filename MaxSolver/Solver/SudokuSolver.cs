﻿using MaxSudoku.MaxSolver.Solver.Heuristics;
using static MaxSudoku.MaxSolver.Solver.SudokuSolverUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MaxSudoku.MaxSolver.CustomExceptions;
using MaxSudoku.MaxSolver.Board;

namespace MaxSudoku.MaxSolver.Solver
{
    public class SudokuSolver : IGameSolver
    {
        private readonly int boardSize;
        private readonly int blockSize;
        private readonly SudokuBoard board;
        private readonly MaskManager maskManager;
        private readonly MovesManager movesManager;
        private SudokuHeuristics? heuristics;

        /// <summary>
        /// Constructor for a new instance of the SudokuSolver class.
        /// </summary>
        /// <param name="board">The Sudoku board to solve.</param>
        /// <exception cref="InvalidInputException">Thrown when board size is not a perfect square.</exception>
        /// Constructs a new SudokuSolver with the given board and algorithm level.
        /// </summary>
        /// <param name="board">The Sudoku board to solve.</param>
        /// <param name="level">The algorithm level to use.</param>
        /// <exception cref="InvalidInputException">Thrown when board size is not a perfect square.</exception>
        public SudokuSolver(SudokuBoard board, AlgorithmLevel level)
        {
            this.board = board;
            boardSize = board.BoardSize;

            blockSize = (int)Math.Sqrt(boardSize);
            if (blockSize * blockSize != boardSize)
                throw new InvalidInputException("Board size must be a perfect square (...4x4, 9x9, 16x16...).");

            maskManager = new MaskManager(boardSize);
            maskManager.UpdateFromBoard(board);
            movesManager = new MovesManager();

            ConfigureAlgorithmLevel(level);
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


        /// <summary>
        /// Configures the heuristics based on the selected algorithm level.
        /// </summary>
        /// <param name="level">The algorithm level to use.</param>
        public void ConfigureAlgorithmLevel(AlgorithmLevel level)
        {
            heuristics = new SudokuHeuristics();
            heuristics.AddHeuristic(new NakedSinglesHeuristic(board, maskManager, movesManager));

            /* Default level (choosed based on the board size) */
            if (level == 0)
            {
                level = GetDefaultAlgorithmLevel(boardSize);
            }

            if (level >= AlgorithmLevel.Level2)
                heuristics.AddHeuristic(new HiddenSinglesHeuristic(board, maskManager, movesManager));

            if (level >= AlgorithmLevel.Level3)
                heuristics.AddHeuristic(new NakedPairsHeuristic(board, maskManager, movesManager));
        }
    }
}

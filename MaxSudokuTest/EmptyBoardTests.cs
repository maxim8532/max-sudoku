using Xunit;
using MaxSudoku.MaxSolver.Board;
using MaxSudoku.MaxSolver.CustomExceptions;
using MaxSudoku.MaxSolver.Solver;

namespace MaxSudokuTest
{
    /// <summary>
    /// Tests for empty boards.
    /// </summary>
    public class EmptyBoardTests
    {
        [Fact]
        public void Solve_Empty9x9Board_ShouldReturnSolve()
        {
            // Arrange
            int boardSize = 9;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            string puzzle = new string('0', boardSize * boardSize);
            board.FillBoard(puzzle);
            var solver = new SudokuSolver(board, AlgorithmLevel.Level2);

            // Act
            bool solved = solver.Solve();

            // Assert
            Assert.True(solved);
        }

        [Fact]
        public void Solve_Empty16x16Board_ShouldSolve()
        {
            // Arrange
            int boardSize = 16;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            string puzzle = new string('0', boardSize * boardSize);
            board.FillBoard(puzzle);
            var solver = new SudokuSolver(board, AlgorithmLevel.Level3);

            // Act
            bool solved = solver.Solve();

            // Assert
            Assert.True(solved);
        }

        [Fact]
        public void Solve_Empty25x25Board_ShouldSolve()
        {
            // Arrange
            int boardSize = 25;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            string puzzle = new string('0', boardSize * boardSize);
            board.FillBoard(puzzle);
            var solver = new SudokuSolver(board, AlgorithmLevel.Level3);

            // Act
            bool solved = solver.Solve();

            // Assert
            Assert.True(solved);
        }
    }
}

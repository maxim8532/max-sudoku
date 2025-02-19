using System;
using Xunit;
using MaxSudoku.MaxSolver.Board;
using MaxSudoku.MaxSolver.CustomExceptions;
using MaxSudoku.MaxSolver.Solver;


namespace MaxSudokuTests
{
    /// <summary>
    /// Tests for validating board input correctness.
    /// </summary>
    public class InputValidationTests
    {
        [Fact]
        public void FillBoard_Valid9x9Input_ShouldFillBoard()
        {
            // Arrange
            int boardSize = 9;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            string puzzle = "530070000600195000098000060800060003400803001700020006060000280000419005000080079";

            // Act
            board.FillBoard(puzzle);

            // Assert – check a few cells
            Assert.Equal(5, board.GetCell(0, 0));
            Assert.Equal(3, board.GetCell(0, 1));
        }

        [Fact]
        public void FillBoard_InvalidLength9x9_ShouldThrowException()
        {
            // Arrange
            int boardSize = 9;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            // 80 characters instead of 81.
            string puzzle = "53007000060019500009800006080006000340080300170002000606000028000041900500008007";

            // Act & Assert
            Assert.Throws<InvalidInputException>(() => board.FillBoard(puzzle));
        }

        [Fact]
        public void FillBoard_InvalidCharacters9x9_ShouldThrowException()
        {
            // Arrange
            int boardSize = 9;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            // Include an invalid character 'X'
            string puzzle = "53007000060019500009800006080006000340080300170002000606000028X000419005000080079";

            // Act & Assert
            Assert.Throws<InvalidInputException>(() => board.FillBoard(puzzle));
        }
    }
}

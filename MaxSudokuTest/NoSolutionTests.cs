using Xunit;
using MaxSudoku.MaxSolver.Board;
using MaxSudoku.MaxSolver.CustomExceptions;
using MaxSudoku.MaxSolver.Solver;


namespace MaxSudokuTest
{
    /// <summary>
    /// Tests for puzzles that are unsolvable.
    /// </summary>
    public class NoSolutionTests
    {
        [Fact]
        public void Solve_Unsolvable9x9Puzzle_ShouldReturnFalse()
        {
            // Arrange
            int boardSize = 9;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            string puzzle = "000030000060000400007050800000406000000900000050010300400000020000300000000000000";
            board.FillBoard(puzzle);
            var solver = new SudokuSolver(board, AlgorithmLevel.Level2);

            // Act
            bool solved = solver.Solve();

            // Assert
            Assert.False(solved);
        }

        [Fact]
        public void Solve_Unsolvable16x16Puzzle_ShouldThrowException()
        {
            // Arrange
            int boardSize = 16;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            string puzzle = ";0?0=>010690000000710000500:?0;4000000<0400070=005<3000800000000500@000:?80>10004<30>?8;00=20000>?8;270060000000000000900000000?0000?00000>0=000?3:0000>0026000000;>61029@0<00000100<0@00:40000800500:0?;>012600800?0;0000090<0@0;07000005<00?8:00003050:4080709";
            board.FillBoard(puzzle);
            var solver = new SudokuSolver(board, AlgorithmLevel.Level2);

            // Act
            bool solved = solver.Solve();

            // Assert
            Assert.False(solved);
        }
    }
}
using MaxSudoku.MaxSolver.Board;
using MaxSudoku.MaxSolver.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudokuTests
{
    /// <summary>
    /// Tests that focus on the algorithm of the solver.
    /// </summary>
    public class SolvingAlgorithmTessts
    {
        [Fact]
        public void Solve_Valid1x1Puzzle_ShouldSolve()
        {
            // Arrange
            int boardSize = 1;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            string puzzle = "0";
            board.FillBoard(puzzle);
            var solver = new SudokuSolver(board, AlgorithmLevel.Level1);

            // Act
            bool solved = solver.Solve();

            // Assert
            Assert.True(solved);
            Assert.True(board.IsFull());
        }

        [Fact]
        public void Solve_Valid4x4Puzzle_ShouldSolve()
        {
            // Arrange
            int boardSize = 4;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            string puzzle = "0000301002040000";
            board.FillBoard(puzzle);
            var solver = new SudokuSolver(board, AlgorithmLevel.Level1);

            // Act
            bool solved = solver.Solve();

            // Assert
            Assert.True(solved);
            Assert.True(board.IsFull());
        }

        [Fact]
        public void Solve_Valid9x9Puzzle_ShouldSolve()
        {
            // Arrange
            int boardSize = 9;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            string puzzle = "530070000600195000098000060800060003400803001700020006060000280000419005000080079";
            board.FillBoard(puzzle);
            var solver = new SudokuSolver(board, AlgorithmLevel.Level1);

            // Act
            bool solved = solver.Solve();

            // Assert
            Assert.True(solved);
            Assert.True(board.IsFull());
        }

        [Fact]
        public void Solve_Valid16x16Puzzle_ShouldSolve()
        {
            // Arrange
            int boardSize = 16;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            string puzzle = "0700080000063000000070<008?0150006010;0070000?4@000?:10003000000050000;307>0@0040000000500300<97<00000006:10002=02=0970<0@0?:050;00=090000080000005030=0000>0@0?804000:000;300<9>00704@056:00;0060000000><00?000@0040560020;970000098?00100000030;02000000400600";
            board.FillBoard(puzzle);
            var solver = new SudokuSolver(board, AlgorithmLevel.Level2);

            // Act
            bool solved = solver.Solve();

            // Assert
            Assert.True(solved);
            Assert.True(board.IsFull());
        }

        [Fact]
        public void Solve_Valid25x25Puzzle_ShouldSolve()
        {
            // Arrange
            int boardSize = 25;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            string puzzle = "0000C000F000000000000000000000000000000000000000000000030000000000000000000000000E00000000000000000000H000000000000450000000000000000000000000000000000000F0G000000000000;00:00000000000000000000000000A0000;000000900000000000000000000000000000000000G0000000000?0000000000000000207000000000000G0000000000000000000000?000000000000000000000000000000000000000000000@000000000000030000000000000000000000000000000000000000000000000000@000000000000000000000000>0000000000000000000000000000000<G000000000000000000000000000000000000600000?000000000000000000000000000000000000000000000000000000000009001>00000000000000000000000000000000H";
            board.FillBoard(puzzle);
            var solver = new SudokuSolver(board, AlgorithmLevel.Level3);

            // Act
            bool solved = solver.Solve();

            // Assert
            Assert.True(solved);
            Assert.True(board.IsFull());
        }

        [Fact]
        public void Already_Solved16x16Puzzle_ShouldSolve()
        {
            // Arrange
            int boardSize = 16;
            var validator = new SudokuValidator();
            var board = new SudokuBoard(boardSize, validator);
            string puzzle = ";412:=6@3<8957>?7>?52;41:=6@93<893<8?57>12;46@:=6@:=<893>?57;412=6@:3<897>?52;412;41@:=693<8?57>57>?12;4@:=6893<893<>?57412;=6@::=6@93<857>?12;412;46@:=893<>?57?57>412;6@:=<893<8937>?5;412:=6@412;=6@:<8937>?5>?57;412=6@:3<893<8957>?2;41@:=6@:=6893<?57>412;";
            board.FillBoard(puzzle);
            var solver = new SudokuSolver(board, AlgorithmLevel.Level3);

            // Act
            bool solved = solver.Solve();

            // Assert
            Assert.True(solved);
            Assert.True(board.IsFull());
        }
    }
}

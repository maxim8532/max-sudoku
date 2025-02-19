using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.MaxSolver.Solver
{
    /// <summary>
    /// Represents a change (move) made to a cell on the board.
    /// </summary>
    public class Move
    {
        public int Row { get; }
        public int Col { get; }
        public int OldValue { get; }
        public int NewValue { get; }

        public Move(int row, int col, int oldValue, int newValue)
        {
            Row = row;
            Col = col;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}

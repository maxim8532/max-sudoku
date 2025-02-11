using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver
{
    /// <summary>
    /// Records moves (both guesses and heuristic placements) so that they can be undone on backtracking.
    /// </summary>
    public class MovesManager
    {
        private readonly Stack<Move> moveStack = new Stack<Move>();

        /// <summary>
        /// Records a move.
        /// </summary>
        public void RecordMove(Move move)
        {
            moveStack.Push(move);
        }
    }
}

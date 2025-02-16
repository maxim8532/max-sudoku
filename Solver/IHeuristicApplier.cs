using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver
{
    public interface IHeuristicApplier
    {
        /// <summary>
        /// Applies the heuristic to the board.
        /// </summary>
        /// <returns>Returns true if at least one cell was filled.</returns>
        bool Apply();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.MaxSolver.Solver
{
    /// <summary>
    /// Specifies the algorithmic level to use.
    /// Default: Uses a board-based default (Level2 for boards up to 16×16, Level3 for larger boards).
    /// Level1: Only Naked Singles.
    /// Level2: Naked Singles and Hidden Singles.
    /// Level3: Naked Singles, Hidden Singles, and Naked Pairs.
    /// </summary>
    public enum AlgorithmLevel
    {
        Default,  // 0
        Level1,   // 1
        Level2,   // 2
        Level3    // 3
    }

}

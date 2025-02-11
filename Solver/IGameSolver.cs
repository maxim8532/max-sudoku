using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver
{
    /// <summary>
    /// Abstract interface for games with solver.
    /// </summary>
    public interface IGameSolver
    {
        bool Solve();
    }
}

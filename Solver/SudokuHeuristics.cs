using MaxSudoku.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver
{
    /// <summary>
    /// Manages a collection of heuristics and applies them.
    /// </summary>
    public class SudokuHeuristics
    {
        private readonly List<Heuristic> heuristics;

        public SudokuHeuristics()
        {
            heuristics = new List<Heuristic>();
        }

        /// <summary>
        /// Adds a heuristic to the list.
        /// </summary>
        public void AddHeuristic(Heuristic heuristic)
        {
            heuristics.Add(heuristic);
        }

        /// <summary>
        /// Applies all heuristics in sequence.
        /// </summary>
        /// <returns>Returns true if any heuristic made progress.</returns>
        public bool ApplyAll()
        {
            bool progressMade = false;
            foreach (var heuristic in heuristics)
            {
                if (heuristic.Apply())
                    progressMade = true;
            }
            return progressMade;
        }
    }
}

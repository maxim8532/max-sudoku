using MaxSudoku.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.Solver.Heuristics
{
    public class NakedPairsHeuristic : Heuristic
    {
        private readonly List<(int row, int col)>[] rowGroups;
        private readonly List<(int row, int col)>[] columnGroups;
        private readonly List<(int row, int col)>[] blockGroups;

        public NakedPairsHeuristic(SudokuBoard board, MaskManager maskManager, MovesManager movesManager)
            : base(board, maskManager, movesManager)
        {
            rowGroups = new List<(int row, int col)>[boardSize];
            columnGroups = new List<(int row, int col)>[boardSize];
            blockGroups = new List<(int row, int col)>[boardSize];
        }

        public override bool Apply()
        {
            throw new NotImplementedException();
        }
    }
}

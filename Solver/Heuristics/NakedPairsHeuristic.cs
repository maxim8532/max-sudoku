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

        /// <summary>
        /// Initializes the groups (lists) for every row, column and block in the board.
        /// </summary>
        private void InitializeGroupCollections()
        {
            /* Initialize row groups. */
            for (int row = 0; row < boardSize; row++)
            {
                rowGroups[row] = new List<(int row, int col)>(boardSize);
                for (int col = 0; col < boardSize; col++)
                    rowGroups[row].Add((row, col));
            }

            /* Initialize column groups. */
            for (int col = 0; col < boardSize; col++)
            {
                columnGroups[col] = new List<(int row, int col)>(boardSize);
                for (int row = 0; row < boardSize; row++)
                    columnGroups[col].Add((row, col));
            }

            /* Initialize block groups. */
            int groupSize = (int)Math.Sqrt(boardSize);
            int groupIndex = 0;
            for (int gr = 0; gr < groupSize; gr++)
            {
                for (int gc = 0; gc < groupSize; gc++)
                {
                    blockGroups[groupIndex] = new List<(int row, int col)>(boardSize);
                    int startRow = gr * groupSize;
                    int startCol = gc * groupSize;
                    for (int r = 0; r < groupSize; r++)
                    {
                        for (int c = 0; c < groupSize; c++)
                        {
                            blockGroups[groupIndex].Add((startRow + r, startCol + c));
                        }
                    }
                    groupIndex++;
                }
            }
        }
    }
}

using MaxSudoku.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            InitializeGroupCollections();
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
            InitializeRowGroups();
            InitializeColumnGroups();
            InitializeblockGroups();
        }

        /// <summary>
        /// Initialize row groups.
        /// </summary>
        private void InitializeRowGroups()
        {
            for (int row = 0; row < boardSize; row++)
            {
                rowGroups[row] = new List<(int row, int col)>(boardSize);
                for (int col = 0; col < boardSize; col++)
                {
                    rowGroups[row].Add((row, col));
                }
            }
        }

        /// <summary>
        /// Initialize column groups.
        /// </summary>
        private void InitializeColumnGroups()
        {
            for (int col = 0; col < boardSize; col++)
            {
                columnGroups[col] = new List<(int row, int col)>(boardSize);
                for (int row = 0; row < boardSize; row++)
                {
                    columnGroups[col].Add((row, col));
                }
            }
        }

        /// <summary>
        /// Initialize block groups.
        /// </summary>
        private void InitializeblockGroups()
        {
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

        /// <summary>
        /// Scans a group and returns a list of naked pairs.
        /// Each naked pair is represented as a tuple containing the available options mask and the two cells that share it.
        /// </summary>
        /// <param name="group">The group of cells to scan.</param>
        /// <returns>A list of naked pair tuples.</returns>
        private List<(int pairMask, (int row, int col) cell1, (int row, int col) cell2)> FindNakedPairs(List<(int row, int col)> group)
        {
            /* detect if a pair has been seen already */
            var pairsByMask = new Dictionary<int, (int row, int col)>();

            /* list that stores complete naked pair entries */
            var nakedPairs = new List<(int pairMask, (int row, int col) cell1, (int row, int col) cell2)>();

            foreach (var (row, col) in group)
            {
                if (board.GetCell(row, col) != 0)
                    continue;

                int avaiableDigitseMask = maskManager.GetAvailableDigits(row, col);
                if (BitOperations.PopCount((uint)avaiableDigitseMask) == 2)
                {
                    if (pairsByMask.TryGetValue(avaiableDigitseMask, out var firstCell))
                    {
                        nakedPairs.Add((avaiableDigitseMask, firstCell, (row, col)));
                    }
                    else
                    {
                        pairsByMask[avaiableDigitseMask] = (row, col);
                    }
                }
            }

            return nakedPairs;

        }
    }
}

using MaxSudoku.Board;
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
        /// <param name="move">A move that the solver did.</param>
        public void RecordMove(Move move)
        {
            moveStack.Push(move);
        }

        /// <summary>
        /// Returns the current count of recorded moves.
        /// </summary>
        public int Count => moveStack.Count;

        /// <summary>
        /// Undos moves until the stack is restored to a given return point of the backtracking.
        /// </summary>
        /// <param name="returnPoint">The point which the backtracking return to (the recursive call)</param>
        /// <param name="board">The Sudoku board.</param>
        /// <param name="maskManager">The mask manager to update the mask for the cells being reverted.</param>
        public void UndoMoves(int returnPoint, SudokuBoard board, MaskManager maskManager)
        {
            while (moveStack.Count > returnPoint)
            {
                Move move = moveStack.Pop();

                board.SetCell(move.Row, move.Col, move.OldValue);

                /* Undo the mask update for the move we are reverting. */
                if (move.NewValue != 0)
                {
                    maskManager.UpdateMasks(move.Row, move.Col, move.NewValue, isPlacing: false);
                }
                /* If the previous value was non-zero, restore it in the mask. */
                if (move.OldValue != 0)
                {
                    maskManager.UpdateMasks(move.Row, move.Col, move.OldValue, isPlacing: true);
                }
            }
        }
    }
}

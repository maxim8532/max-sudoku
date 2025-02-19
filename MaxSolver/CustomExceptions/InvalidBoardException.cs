using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.MaxSolver.CustomExceptions
{
    /// <summary>
    /// An exception for invalid board (doesn't follow Sudoku's rules).
    /// </summary>
    public class InvalidBoardException : Exception
    {
        public InvalidBoardException(string message)
        : base(message)
        {

        }
    }
}

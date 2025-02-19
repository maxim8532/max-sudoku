using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.MaxSolver.CustomExceptions
{
    /// <summary>
    /// An exception for invalid board inputs.
    /// </summary>
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message)
        : base(message)
        {

        }
    }
}

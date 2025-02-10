using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku
{
    /// <summary>
    /// An exception for invalid board inputs.
    /// </summary>
    public class InvalidInputException:Exception
    {
        public InvalidInputException(String message)
        : base(message)
        {

        }
    }
}

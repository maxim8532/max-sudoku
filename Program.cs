using MaxSudoku.MaxSolver.Board;
using MaxSudoku.MaxSolver.Solver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxSudoku.MaxSolver.UI;


namespace MaxSudoku
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            UserInterface ui = new UserInterface();
            ui.Run();
        }
    }
}

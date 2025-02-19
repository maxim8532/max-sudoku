using MaxSudoku.MaxSolver.Board;
using MaxSudoku.MaxSolver.CustomExceptions;
using MaxSudoku.MaxSolver.Solver;

namespace MaxSudoku.MaxSolver.UI
{
    /// <summary>
    /// A console based UI for the Sudoku solver.
    /// </summary>
    public class UserInterface
    {
        private AlgorithmLevel currentLevel = AlgorithmLevel.Default;

        public UserInterface()
        {
            /* For the sneaky people who love to press on the RED BUTTON >:)*/
            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnCancelKeyPress);
        }

        /// <summary>
        /// Runs the main UI loop.
        /// </summary>
        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("░  ░░░░  ░░░      ░░░  ░░░░  ░░░░░░░░░      ░░░  ░░░░  ░░       ░░░░      ░░░  ░░░░  ░░  ░░░░  ░\r\n▒   ▒▒   ▒▒  ▒▒▒▒  ▒▒▒  ▒▒  ▒▒▒▒▒▒▒▒▒  ▒▒▒▒▒▒▒▒  ▒▒▒▒  ▒▒  ▒▒▒▒  ▒▒  ▒▒▒▒  ▒▒  ▒▒▒  ▒▒▒  ▒▒▒▒  ▒\r\n▓        ▓▓  ▓▓▓▓  ▓▓▓▓    ▓▓▓▓▓▓▓▓▓▓▓      ▓▓▓  ▓▓▓▓  ▓▓  ▓▓▓▓  ▓▓  ▓▓▓▓  ▓▓     ▓▓▓▓▓  ▓▓▓▓  ▓\r\n█  █  █  ██        ███  ██  ███████████████  ██  ████  ██  ████  ██  ████  ██  ███  ███  ████  █\r\n█  ████  ██  ████  ██  ████  █████████      ████      ███       ████      ███  ████  ███      ██");
                Console.WriteLine();
                Console.WriteLine();
                Console.ResetColor();
                Console.WriteLine("Current algorithm level: " +
                    (currentLevel == AlgorithmLevel.Default ? "DEFAULT" : currentLevel.ToString()));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1. Solve a board");
                Console.ResetColor();
                Console.WriteLine("2. Change algorithm level (optional)");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice (1 - 3): ");
                string choice = SafeReadLine();
                if (choice == null) { exit = true; break; }
                switch (choice.Trim())
                {
                    case "1":
                        SolveBoard();
                        break;
                    case "2":
                        ChangeAlgorithmLevel();
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
            Console.WriteLine("Thanks for using MAX SUDOKU, Goodbye!");
        }


        private void OnCancelKeyPress(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            Console.WriteLine("\nThanks for using MAX SUDOKU. Goodbye!");
            Environment.Exit(0);
        }

        /// <summary>
        /// Allows the user to change the algorithm level,
        /// and provides information about it.
        /// </summary>
        private void ChangeAlgorithmLevel()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("░░      ░░░  ░░░░░░░░░      ░░░░      ░░░       ░░░        ░░        ░░  ░░░░  ░░  ░░░░  ░\r\n▒  ▒▒▒▒  ▒▒  ▒▒▒▒▒▒▒▒  ▒▒▒▒▒▒▒▒  ▒▒▒▒  ▒▒  ▒▒▒▒  ▒▒▒▒▒  ▒▒▒▒▒▒▒▒  ▒▒▒▒▒  ▒▒▒▒  ▒▒   ▒▒   ▒\r\n▓  ▓▓▓▓  ▓▓  ▓▓▓▓▓▓▓▓  ▓▓▓   ▓▓  ▓▓▓▓  ▓▓       ▓▓▓▓▓▓  ▓▓▓▓▓▓▓▓  ▓▓▓▓▓        ▓▓        ▓\r\n█        ██  ████████  ████  ██  ████  ██  ███  ██████  ████████  █████  ████  ██  █  █  █\r\n█  ████  ██        ███      ████      ███  ████  ██        █████  █████  ████  ██  ████  █\r\n                                                                                          \r\n░  ░░░░░░░░        ░░  ░░░░  ░░        ░░  ░░░░░░░                                        \r\n▒  ▒▒▒▒▒▒▒▒  ▒▒▒▒▒▒▒▒  ▒▒▒▒  ▒▒  ▒▒▒▒▒▒▒▒  ▒▒▒▒▒▒▒                                        \r\n▓  ▓▓▓▓▓▓▓▓      ▓▓▓▓▓  ▓▓  ▓▓▓      ▓▓▓▓  ▓▓▓▓▓▓▓                                        \r\n█  ████████  ██████████    ████  ████████  ███████                                        \r\n█        ██        █████  █████        ██        █");
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("Default: Uses board-based default (Level 2 for boards up to 16x16, Level 3 for larger boards).");
            Console.WriteLine("Level 1: Only Naked Singles. (May be better for easy boards and some unsolvable ones, can sometimes be surprisingly fast!)");
            Console.WriteLine("Level 2: Naked Singles and Hidden Singles (Good for most boards)");
            Console.WriteLine("Level 3: Naked Singles, Hidden Singles, and Naked Pairs (Makes a difference for harder boards, " +
                "\nuse for board size 16x16 minimum).");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nChanging the algorithm level is optional.");
            Console.ResetColor();
            Console.WriteLine("Press M at any time to return to the main menu.\n");

            while (true)
            {
                Console.Write("Enter algorithm level (0 [Default], 1, 2, or 3) or press M to return: ");
                string input = SafeReadLine();
                if (input == null || input.Trim().ToUpper() == "M")
                    return;
                string option = input.Trim().ToUpper();
                if (option == "0")
                {
                    currentLevel = AlgorithmLevel.Default;
                    break;
                }
                else if (option == "1")
                {
                    currentLevel = AlgorithmLevel.Level1;
                    break;
                }
                else if (option == "2")
                {
                    currentLevel = AlgorithmLevel.Level2;
                    break;
                }
                else if (option == "3")
                {
                    currentLevel = AlgorithmLevel.Level3;
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid selection, Try selecting again.");
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Algorithm level set to: " +
                (currentLevel == AlgorithmLevel.Default ? "DEFAULT" : currentLevel.ToString()));
            Console.ResetColor();
            Console.WriteLine("Press M to return to the main menu.");
            while (SafeReadLine()?.Trim().ToUpper() != "M") { }
        }

        /// <summary>
        /// Prompts the user for board size, input method, and then solves the board.
        /// Displays the solution in the console and writes the solution to file (only if input came from a file).
        /// </summary>
        private void SolveBoard()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Clear();
                Console.WriteLine("░░      ░░░░      ░░░  ░░░░░░░░  ░░░░  ░░        ░░       ░░\r\n▒  ▒▒▒▒▒▒▒▒  ▒▒▒▒  ▒▒  ▒▒▒▒▒▒▒▒  ▒▒▒▒  ▒▒  ▒▒▒▒▒▒▒▒  ▒▒▒▒  ▒\r\n▓▓      ▓▓▓  ▓▓▓▓  ▓▓  ▓▓▓▓▓▓▓▓▓  ▓▓  ▓▓▓      ▓▓▓▓       ▓▓\r\n███████  ██  ████  ██  ██████████    ████  ████████  ███  ██\r\n██      ████      ███        █████  █████        ██  ████  █\r\n                                                            ");
                Console.WriteLine();
                Console.WriteLine();
                Console.ResetColor();
                Console.Write("Enter board size (must be a perfect square like: 9, 16, or 25) or press M to return: ");
                string sizeInput = SafeReadLine();
                if (sizeInput == null || sizeInput.Trim().ToUpper() == "M")
                    return;
                int size = int.Parse(sizeInput.Trim());

                /* Validate board size is a perfect square. */
                int sqrt = (int)Math.Sqrt(size);
                if (sqrt * sqrt != size || size < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: board size must be a perfect square like: 9, 16, or 25.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to return to the main menu...");
                    Console.ReadKey();
                    return;
                }

                /* Create board. */
                SudokuValidator validator = new SudokuValidator();
                SudokuBoard board = new SudokuBoard(size, validator);

                Console.WriteLine("\nSelect input method:");
                Console.WriteLine("1. CONSOLE");
                Console.WriteLine("2. FILE");
                Console.Write("Enter your choice (1 or 2) or press M to return: ");
                string methodChoice = SafeReadLine();
                if (methodChoice == null || methodChoice.Trim().ToUpper() == "M")
                    return;

                string boardString = "";
                bool fileInput = false;
                if (methodChoice.Trim() == "1")
                {
                    boardString = UserInterfaceUtils.ReadBoardFromConsole();
                }
                else if (methodChoice.Trim() == "2")
                {
                    boardString = UserInterfaceUtils.ReadBoardFromFile();
                    if (string.IsNullOrEmpty(boardString))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Board file is empty or could not be read. Returning to menu...");
                        Console.ResetColor();
                        Console.ReadKey();
                        return;
                    }
                    fileInput = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input method. Press any key to return to the menu.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                board.FillBoard(boardString);

                /* Determine algorithm level. */
                AlgorithmLevel effectiveLevel = currentLevel == AlgorithmLevel.Default
                    ? size <= 16 ? AlgorithmLevel.Level2 : AlgorithmLevel.Level3
                    : currentLevel;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nAlgorithm level in use: " + effectiveLevel);
                Console.ResetColor();

                SudokuSolver solver = new SudokuSolver(board, effectiveLevel);

                Console.WriteLine("\nSolving board...");
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                bool solved = solver.Solve();
                stopwatch.Stop();

                /* Display solving time. */
                Console.Write("Solving time: ");
                Console.ForegroundColor = stopwatch.ElapsedMilliseconds < 1000
                    ? ConsoleColor.Green
                    : ConsoleColor.Red;
                Console.WriteLine("{0} ms", stopwatch.ElapsedMilliseconds);
                Console.ResetColor();

                /* Display solution. */
                Console.WriteLine("\nSolution:");
                if (solved)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(UserInterfaceUtils.FormatBoard(board.ToString(), size));
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(board.ToString());
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("No solution found :(");
                    Console.WriteLine();
                    Console.WriteLine(UserInterfaceUtils.FormatBoard(board.ToString(), size));
                    Console.ResetColor();
                }

                if (fileInput && solved)
                {
                    Console.Write("Write solved board to file? (Y/N): ");
                    string writeChoice = SafeReadLine();
                    if (writeChoice != null && writeChoice.Trim().ToUpper() == "Y")
                    {
                        UserInterfaceUtils.WriteBoardToFile(board.ToString());
                    }
                }

                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
            }
            catch (InvalidInputException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input error: " + e.Message);
                Console.ResetColor();
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
            }
            catch (InvalidBoardException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Board error: " + e.Message);
                Console.ResetColor();
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
            }
            catch (FormatException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Format error: " + e.Message);
                Console.ResetColor();
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
            }
            catch (FileNotFoundException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File error: " + e.Message);
                Console.ResetColor();
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unexpected error (IF YOU SEE IT, CONGRATS Ω I MISSED SOMETHING): " + ex.Message);
                Console.ResetColor();
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Safely reads a line from the console.
        /// </summary>
        /// <returns>Returns null if end-of-input (Ctrl+Z)</returns>
        private string SafeReadLine()
        {
            try
            {
                return Console.ReadLine();
            }
            catch (IOException)
            {
                return null;
            }
        }
    }
}

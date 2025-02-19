using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku.UI
{
    /// <summary>
    /// Provides utility methods for reading and writing Sudoku board strings and formatting boards.
    /// </summary>
    public static class UserInterfaceUtils
    {
        /// <summary>
        /// Reads a board string from the console.
        /// </summary>
        /// <returns>The board string entered by the user.</returns>
        public static string ReadBoardFromConsole()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Enter the board string :");
            Console.ResetColor();
            string input = Console.ReadLine()?.Trim();
            return input ?? "";
        }

        /// <summary>
        /// Reads a board string from a text file.
        /// </summary>
        /// <returns>The board string read from the file, or an empty string if an error occurs.</returns>
        public static string ReadBoardFromFile()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.Write("Enter the name/path of the board file: ");
            Console.ResetColor();
            string filePath = Console.ReadLine()?.Trim();
            try
            {
                string content = File.ReadAllText(filePath);

                /* Incase there are spaces or new lines */
                return content.Replace("\r", "").Replace("\n", "").Trim();
            }
            catch (FileNotFoundException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File not found: " + e.Message);
                Console.ResetColor();
                return "";
            }
            catch (IOException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("I/O error reading file: " + e.Message);
                Console.ResetColor();
                return "";
            }
        }

            /// <summary>
            /// Writes the solved board string to a text file.
            /// </summary>
            /// <param name="boardString">The solved board string.</param>
            public static void WriteBoardToFile(string boardString)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter an existing name/path of file to write in it, or a new one: ");
            Console.ResetColor();
            string outputPath = Console.ReadLine()?.Trim();
            try
            {
                File.WriteAllText(outputPath, boardString);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Solved board written to: " + outputPath);
                Console.ResetColor();
            }
            catch (UnauthorizedAccessException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Access denied: " + e.Message);
                Console.ResetColor();
            }
            catch (IOException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("I/O error writing file: " + e.Message);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Formats a board string into a grid with block separators.
        /// </summary>
        /// <param name="boardString">The board string.</param>
        /// <param name="boardSize">The board dimension.</param>
        /// <returns>A formatted board string.</returns>
        public static string FormatBoard(string boardString, int boardSize)
        {
            int cellWidth = 2;
            int groupSize = (int)Math.Sqrt(boardSize);
            string horizontalLine = new string('-', cellWidth * boardSize + groupSize - 1);
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    char ch = boardString[row * boardSize + col];
                    sb.Append(ch);
                    sb.Append(" ");
                    if ((col + 1) % groupSize == 0 && col != boardSize - 1)
                    {
                        sb.Append("| ");
                    }
                }
                sb.AppendLine();
                if ((row + 1) % groupSize == 0 && row != boardSize - 1)
                {
                    sb.AppendLine(horizontalLine);
                }
            }
            return sb.ToString();
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CommandSurvivalAdventure
{
    namespace IO
    {
        // This class handles and manages the output.  Pretty simple.
        class Output : CSABehaviour
        {
            // This enum lets you specify colors when outputing
            public enum OutputColor
            {
                BLACK = (int)ConsoleColor.Black,                // a
                DARK_BLUE = (int)ConsoleColor.DarkBlue,         // b
                DARK_GREEN = (int)ConsoleColor.DarkGreen,       // c
                DARK_CYAN = (int)ConsoleColor.DarkCyan,         // d
                DARK_RED = (int)ConsoleColor.DarkRed,           // e
                DARK_MAGENTA = (int)ConsoleColor.DarkMagenta,   // f
                DARK_YELLOW = (int)ConsoleColor.DarkYellow,     // g
                GRAY = (int)ConsoleColor.Gray,                  // h
                DARK_GRAY = (int)ConsoleColor.DarkGray,         // i
                BLUE = (int)ConsoleColor.Blue,                  // j
                GREEN = (int)ConsoleColor.Green,                // k
                CYAN = (int)ConsoleColor.Cyan,                  // l
                RED = (int)ConsoleColor.Red,                    // m
                MAGENTA = (int)ConsoleColor.Magenta,            // n
                YELLOW = (int)ConsoleColor.Yellow,              // o
                WHITE = (int)ConsoleColor.White                 // p
            }
            // Initialize 
            public Output(Application newApplication)
            {
                // Initialize the application
                attachedApplication = newApplication;
            }
            // Outputs a string to the console with an enter character at the end
            public void PrintLine(string stringToPrint)
            {
                #region Clear out the input buffer that was written previously
                Console.CursorLeft = 0;
                Console.Write(new string(' ', Console.BufferWidth) + new string('\b', Console.BufferWidth));
                #endregion

                #region Go word by word and output each one, coloring them if necessary
                // Break the line by words
                List<string> words = stringToPrint.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                // Loop through all the words, and color them if necessary as they are being outputted
                foreach(string word in words)
                {
                    // Check for $  
                    if (word[0] == '$')
                    {
                        if (word.Length > 2)
                        {
                            // Make sure the color code given is valid
                            if(word[1] - 97 >= 0 && word[1] - 97 <= 15 && word[2] - 97 >= 0 && word[2] - 97 <= 15)
                            {
                                // Change the color accordingly
                                Console.ForegroundColor = (ConsoleColor)(word[1] - 97);
                                Console.BackgroundColor = (ConsoleColor)(word[2] - 97);
                                // If there is actually string to print
                                if(word.Substring(3) != "")
                                    // Print everything past the characters specifying the color
                                    Console.Write(word.Substring(3) + " ");
                            }
                        }
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.Write(word + " ");
                    }
                }
                #endregion

                #region Add spaces to clear out the rest of the line
                Console.ResetColor();
                // Add spaces to clear out the rest of the line
                int width = Console.BufferWidth - Console.CursorLeft - 1;
                // Clear the rest of the console
                Console.Write(new string(' ', width) + new string('\b', width) + '\n');
                // Re-echo the input buffer
                attachedApplication.input.RefreshInputEchoed();
                #endregion
            }
            // Outputs the given string like normal, but does not refresh the input buffer.
            public static void NoInputBufferRefreshPrint(string stringToPrint)
            {
                Console.ResetColor();
                Console.Write(stringToPrint);
            }
            // Outputs the given string like normal, but does not refresh the input buffer and reprints over the previous line
            public static void NoInputBufferRefreshReprint(string stringToPrint)
            {
                // Set the cursor position to overwrite the input buffer that was displayed previously
                Console.SetCursorPosition(2, Console.CursorTop);
                int width = Console.BufferWidth - Console.CursorLeft - 1;
                // Clear the rest of the console
                Console.Write(new string(' ', width) + new string('\b', width) + '\n');
                // Now, rewrite
                Console.SetCursorPosition(2, (Console.CursorTop == 0 ? 0 : Console.CursorTop - 1));
                Console.Write(stringToPrint);
            }
            // Outputs the given string with and enter character like normal, but does not refresh the input buffer.
            public static void NoInputBufferRefreshPrintLine(string stringToPrint)
            {
                Console.ResetColor();
                Console.WriteLine(stringToPrint);
            }
            // Outputs a blank character to represent a pixel
            public static void PrintPixel(int x, int y, OutputColor color)
            {
                // Move the cursor to the position, set the color, and print the whitespace
                Console.SetCursorPosition(x, y);
                Console.BackgroundColor = (ConsoleColor)(int)color;
                Console.Write(' ');
            }
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace CommandSurvivalAdventure
{
    namespace IO
    {
        // This class handles and manages the output.  Pretty simple.
        class Output : CSABehaviour
        {
            // This enum lets you specify colors when outputing
            public Color[] outputColor =
            {
                Color.Black,                // a
                Color.DarkBlue,       // b
                Color.DarkGreen,       // c
                Color.DarkCyan,         // d
                Color.DarkRed,           // e
                Color.DarkMagenta,   // f
                Color.Brown,     // g
                Color.Gray,                  // h
                Color.DarkGray,         // i
                Color.Blue,                  // j
                Color.Green,                // k
                Color.Cyan,                  // l
                Color.Red,                    // m
                Color.Magenta,            // n
                Color.Yellow,              // o
                Color.White                 // p
            };
            // Initialize 
            public Output(CSACore newApplication)
            {
                // Initialize the application
                attachedApplication = newApplication;
            }
            // Outputs a string to the console with an enter character at the end
            public void PrintLine(string stringToPrint)
            {
                #region Go word by word and output each one, coloring them if necessary
                // Break the line by words
                List<string> words = stringToPrint.Split(' ').ToList();
                for(int i = 0; i < words.Count; i++)
                {
                    if (words[i] == "")
                        words.RemoveAt(i);
                }
                // Loop through all the words, and color them if necessary as they are being outputted
                foreach (string word in words)
                {
                    // Check for $  
                    if (word[0] == '$')
                    {
                        if (word.Length > 2)
                        {
                            // Make sure the color code given is valid
                            if (word[1] - 97 >= 0 && word[1] - 97 <= 15 && word[2] - 97 >= 0 && word[2] - 97 <= 15)
                            {
                                // If there is actually string to print
                                if (word.Substring(3) != "")
                                    // Print everything past the characters specifying the color
                                    attachedApplication.mainWindow.OutputBox.AppendText(word.Substring(3) + " ");
                                // Select the new word
                                attachedApplication.mainWindow.OutputBox.Select(attachedApplication.mainWindow.OutputBox.TextLength - word.Substring(3).Length - 1, attachedApplication.mainWindow.OutputBox.TextLength);
                                // Change the color accordingly
                                attachedApplication.mainWindow.OutputBox.SelectionColor = outputColor[(int)word[1] - 97];
                            }
                        }
                    }
                    else
                    {
                        attachedApplication.mainWindow.OutputBox.SelectionColor = Color.Empty;
                        attachedApplication.mainWindow.OutputBox.AppendText(word + " ");
                    }
                }
                attachedApplication.mainWindow.OutputBox.AppendText("\n");
                attachedApplication.mainWindow.OutputBox.ScrollToCaret();
                #endregion

            }


        }
    }
}
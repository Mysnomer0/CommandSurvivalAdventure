using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace CommandSurvivalAdventure
{
    namespace IO
    {
        // This class manages all input for the program
        class Input : CSABehaviour
        {
            // Flip this flag to true to stop the ReadInputThread
            public bool STOP_READING_INPUT = false;
            // Flip this flag to true to pause the reading input
            public bool PAUSE_READING_INPUT = false;
            // The current input buffer, that is, the current inputted string that hasn't been entered yet
            private string inputBuffer = "";
            // List of commands that the user has entered so we can retrieve them.
            private List<string> prevCommands = new List<string>();
            // Num of commands that have been used
            private int numOfCommands = 0;
            // The number of the active element in prevCommnads
            private int activeCommand = -1;

            // Initialize
            public Input(Application newApplication)
            {
                // Initialize the application
                attachedApplication = newApplication;
                // Start the input thread
                Thread inputThread = new Thread(ReadInputThread);
                inputThread.Start();
            }
            // Use this to refresh the input echoed so far; This would usually be called when something has just async been outputted.
            public void RefreshInputEchoed()
            {
                Output.NoInputBufferRefreshPrint("> " + inputBuffer);
            }
            // This thread continuously reads input and feeds it to the parser
            public void ReadInputThread()
            {
                // Show that we are taking input
                RefreshInputEchoed();
                // Init cursor position
                int cursorPos = 0;

                // Read input until stopped
                while (!STOP_READING_INPUT)
                {
                    // Add this command to the log of commands
                    prevCommands.Add(inputBuffer);
                    numOfCommands++;
                    activeCommand++;

                    // Keep asking for a key until the enter character is inputted
                    while (true)
                    {
                        // If we're pausing the input, just stop the code here
                        if (PAUSE_READING_INPUT)
                            break;
                        // Read in the new key
                        var newKey = Console.ReadKey(true);
                        // As soon as the user hits enter, stop the loop
                        if (newKey.Key == ConsoleKey.Enter)
                        {
                            if (activeCommand != numOfCommands - 1)
                            {
                                string tempString = prevCommands[activeCommand];
                                // Move the active element of the backlog to the front
                                prevCommands[numOfCommands - 1] = tempString;
                                prevCommands.RemoveAt(activeCommand);
                                // Set active command back to the front of the list
                                numOfCommands--;
                                activeCommand = numOfCommands - 1;
                            }
                            break;
                        }
                        // Otherwise, if the user hits the backspace, remove a character
                        else if (newKey.Key == ConsoleKey.Backspace)
                        {
                            // Loop through and copy everything but the last character into a temporary string
                            string tempString = "";
                            for(int i = 0; i < inputBuffer.Length; i++)
                                if(i != cursorPos - 1) tempString += inputBuffer[i];
                            // Now, set the temp string, which is just the input buffer missing the last character, to the new input buffer
                            inputBuffer = tempString;
                            // And update the command stored in the backlog
                            prevCommands[activeCommand] = inputBuffer;
                            // Reprint the input buffer
                            Output.NoInputBufferRefreshReprint(inputBuffer);
                            if (cursorPos > 0) cursorPos--;
                            Console.SetCursorPosition(cursorPos + 2, Console.CursorTop);
                        }
                        else if (newKey.Key == ConsoleKey.Delete)
                        {
                            // Loop through and copy everything but the last character into a temporary string
                            string tempString = "";
                            for (int i = 0; i < inputBuffer.Length; i++)
                                if (i != cursorPos) tempString += inputBuffer[i];
                            // Now, set the temp string, which is just the input buffer missing the last character, to the new input buffer
                            inputBuffer = tempString;
                            // And update the command stored in the backlog
                            prevCommands[activeCommand] = inputBuffer;
                            // Reprint the input buffer
                            Output.NoInputBufferRefreshReprint(inputBuffer);
                            Console.SetCursorPosition(cursorPos + 2, Console.CursorTop);
                        }
                        else if (newKey.Key == ConsoleKey.LeftArrow)
                        {
                            if (cursorPos > 0) cursorPos--;
                            Console.SetCursorPosition(cursorPos + 2, Console.CursorTop);
                        }
                        else if (newKey.Key == ConsoleKey.RightArrow)
                        {
                            if (cursorPos < inputBuffer.Length) cursorPos++;
                            Console.SetCursorPosition(cursorPos + 2, Console.CursorTop);
                        }
                        else if (newKey.Key == ConsoleKey.UpArrow)
                        {
                            // If there is another element in storage then change inputBuffer to be the command from storage
                            if (activeCommand > 0)
                            {
                                activeCommand--;
                                inputBuffer = prevCommands[activeCommand];
                            }
                            // Reload the console
                            Output.NoInputBufferRefreshReprint(inputBuffer);
                            // Set the cursor position to be the length of the inputBuffer
                            cursorPos = inputBuffer.Length;
                            Console.SetCursorPosition(cursorPos + 2, Console.CursorTop);
                        }
                        else if (newKey.Key == ConsoleKey.DownArrow)
                        {
                            // If there is another element in storage then change inputBuffer to be the command from storage
                            if (activeCommand != numOfCommands - 1)
                            {
                                activeCommand++;
                                inputBuffer = prevCommands[activeCommand];
                            }
                            // Reload the console
                            Output.NoInputBufferRefreshReprint(inputBuffer);
                            // Set the cursor position to be the length of the inputBuffer
                            cursorPos = inputBuffer.Length;
                            Console.SetCursorPosition(cursorPos + 2, Console.CursorTop);
                        }
                        else
                        {
                            // New string with char to insert
                            string tempChar = "";
                            tempChar += newKey.KeyChar;
                            // Otherwise, just keep adding the new keys to the input buffer
                            inputBuffer = inputBuffer.Insert(cursorPos, tempChar);
                            // And update the command stored in the backlog
                            prevCommands[activeCommand] = inputBuffer;
                            // Move the cursor
                            cursorPos++;
                            // Echo the input as well
                            Output.NoInputBufferRefreshReprint(inputBuffer);
                            Console.SetCursorPosition(cursorPos + 2, Console.CursorTop);
                            //Output.NoInputBufferRefreshPrint(newKey.KeyChar.ToString());
                        }
                    }
                    // Once we've gotten input, parse the command
                    attachedApplication.parser.Parse(inputBuffer);
                    // Reset the buffer and the loop will continue
                    inputBuffer = "";
                    // Reprint the input buffer
                    Output.NoInputBufferRefreshReprint(inputBuffer);
                    // And set the cursor position
                    cursorPos = 0;
                    Console.SetCursorPosition(cursorPos + 2, Console.CursorTop);
                }
            }
        }
    }
}

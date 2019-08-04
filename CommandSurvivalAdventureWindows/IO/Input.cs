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

            // Initialize
            public Input(CSACore newApplication)
            {
                // Initialize the application
                attachedApplication = newApplication;
            }

            public void OnReceiveInput(string input)
            {
                if (STOP_READING_INPUT)
                    return;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    // Once we've gotten input, parse the command
                    attachedApplication.parser.Parse(input);
                }).Start();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    class CommandExit : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // Exit the program
            attachedApplication.input.STOP_READING_INPUT = true;
        }
        // Initialize the command
        public CommandExit(Application application)
        {
            attachedApplication = application;
        }
    }
}

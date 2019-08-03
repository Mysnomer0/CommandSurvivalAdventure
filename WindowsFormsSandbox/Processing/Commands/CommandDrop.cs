using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This is the client command that allows you to say things in game
    class CommandDrop : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: <nameOfObjectToDrop>

            // Make sure we are connected to a client
            if(!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            if (arguments.Count > 0)
            {
                // Create a new server command
                Support.Networking.ServerCommands.ServerCommandDrop serverCommand = new Support.Networking.ServerCommands.ServerCommandDrop(attachedApplication.client.clientID);
                // Get the full name of the object to drop
                string fullNameOfObject = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, 0, new List<string>() { } ));
                // Set the name of the object we want to drop
                serverCommand.arguments.Add(fullNameOfObject);
                // Send the request to the server
                attachedApplication.client.SendServerCommand(serverCommand);
            }
            else
                attachedApplication.output.PrintLine("$maUsage: $madrop $ma<nameOfObjectToDrop>");
        }
        // Initialize the command
        public CommandDrop(CSACore application)
        {
            attachedApplication = application;
        }
    }
}

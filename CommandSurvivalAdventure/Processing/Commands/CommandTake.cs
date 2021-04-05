using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This is the client command parses the take command
    class CommandTake : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: <nameOfObjectToPickUp> <OPTIONAL:nameOfObjectToTakeWith>

            // Make sure we are connected to a client
            if(!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            if (arguments.Count > 0)
            {
                // Create a new server command
                Support.Networking.ServerCommands.ServerCommandTake serverCommand = new Support.Networking.ServerCommands.ServerCommandTake(attachedApplication.client.clientID);

                // The index of the name of the object to pick up
                int indexOfObjectToPickUp = 0;
                // The index of the second
                int indexOfObjectToTakeWith = 0;
                // If there is an "up", skip past it
                if (arguments[0] == "up")
                    indexOfObjectToPickUp = 1;
                // Get the full name of the object to pick up
                string fullNameOfObjectToPickUp = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, indexOfObjectToPickUp, new List<string> { "with", "using" }, ref indexOfObjectToTakeWith));
                // After the with/using, get the name of the object to take with
                string fullNameOfObjectToTakeWith = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, indexOfObjectToTakeWith, new List<string> { }));

                // Set the name of the object we want to pick up
                serverCommand.arguments.Add(fullNameOfObjectToPickUp);
                // If there actually was an object specified to use, add it to the arguments
                if (fullNameOfObjectToTakeWith != "")
                    serverCommand.arguments.Add(fullNameOfObjectToTakeWith);
                // Send the request to the server
                attachedApplication.client.SendServerCommand(serverCommand);
            }
            else
                attachedApplication.output.PrintLine("$maUsage: $matake/pick $maup $ma<nameOfObjectToPickUp> with/using $ma<nameOfObjectToUse>");
        }
        // Initialize the command
        public CommandTake(Application application)
        {
            attachedApplication = application;
        }
    }
}

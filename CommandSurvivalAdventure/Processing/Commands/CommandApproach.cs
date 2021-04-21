using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This is the client command that allows you to say things in game
    class CommandApproach : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // Make sure we are connected to a client
            if(!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            // Check args
            if(arguments.Count == 0)
            {
                attachedApplication.output.PrintLine("$maUsage $ma<objectToApproach>");
                return;
            }
            // Create a new server command
            Support.Networking.ServerCommands.ServerCommandApproach serverCommand = new Support.Networking.ServerCommands.ServerCommandApproach(attachedApplication.client.clientID);
            // Get just the name of the object to approach
            string fullNameOfObjectToTakeWith = Parser.ScrubArticles(arguments);
            // Set the argument
            serverCommand.arguments.Add(fullNameOfObjectToTakeWith);
            // Send the request
            attachedApplication.client.SendServerCommand(serverCommand);
        }
        // Initialize the command
        public CommandApproach(Application application)
        {
            attachedApplication = application;
        }
    }
}

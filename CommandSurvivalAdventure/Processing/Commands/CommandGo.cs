using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This is the client command that allows you to say things in game
    class CommandGo : Command
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
            if(arguments.Count != 1)
            {
                attachedApplication.output.PrintLine("$maUsage $ma<direction>");
                return;
            }
            // Create a new server command
            Support.Networking.ServerCommands.ServerCommandGo serverCommand = new Support.Networking.ServerCommands.ServerCommandGo(attachedApplication.client.clientID);
            // Set the direction we want to move
            serverCommand.arguments.Add(arguments[0]);
            // Send a look request to the server
            attachedApplication.client.SendServerCommand(serverCommand);
        }
        // Initialize the command
        public CommandGo(Application application)
        {
            attachedApplication = application;
        }
    }
}

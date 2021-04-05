using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This command parses the stand command
    class CommandCrouch : Command
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

            // Create a new server command
            Support.Networking.ServerCommands.ServerCommandCrouch serverCommand = new Support.Networking.ServerCommands.ServerCommandCrouch(attachedApplication.client.clientID);
            // Send a the request to the server
            attachedApplication.client.SendServerCommand(serverCommand);
        }
        // Initialize the command
        public CommandCrouch(Application application)
        {
            attachedApplication = application;
        }
    }
}

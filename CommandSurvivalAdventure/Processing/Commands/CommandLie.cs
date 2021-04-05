using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This command parses the lie command
    class CommandLie : Command
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
            Support.Networking.ServerCommands.ServerCommandLie serverCommand = new Support.Networking.ServerCommands.ServerCommandLie(attachedApplication.client.clientID);
            // Send a the request to the server
            attachedApplication.client.SendServerCommand(serverCommand);
        }
        // Initialize the command
        public CommandLie(Application application)
        {
            attachedApplication = application;
        }
    }
}

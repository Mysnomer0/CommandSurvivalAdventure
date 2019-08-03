using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This command parses the stand command
    class CommandStand : Command
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
            Support.Networking.ServerCommands.ServerCommandStand serverCommand = new Support.Networking.ServerCommands.ServerCommandStand(attachedApplication.client.clientID);
            // Send a the request to the server
            attachedApplication.client.SendServerCommand(serverCommand);
        }
        // Initialize the command
        public CommandStand(CSACore application)
        {
            attachedApplication = application;
        }
    }
}

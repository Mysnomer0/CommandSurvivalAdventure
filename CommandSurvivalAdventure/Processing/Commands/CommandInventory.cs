using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This command parses the command to display the inventory
    class CommandInventory : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: none 

            // Make sure we are connected to a client
            if(!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            if (arguments.Count == 0)
            {
                // Create a new server command
                Support.Networking.ServerCommands.ServerCommandInventory serverCommand = new Support.Networking.ServerCommands.ServerCommandInventory(attachedApplication.client.clientID);
                // Send the request to the server
                attachedApplication.client.SendServerCommand(serverCommand);
            }
        }
        // Initialize the command
        public CommandInventory(Application application)
        {
            attachedApplication = application;
        }
    }
}

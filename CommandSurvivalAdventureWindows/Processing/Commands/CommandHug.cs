using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This command parses the hug command
    class CommandHug : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: <playerToHugsName>

            // Make sure we are connected to a client
            if (!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            // Show help
            if(arguments.Count != 1)
            {
                attachedApplication.output.PrintLine("$maUsage: $ma<playerToHugsName>");
                return;
            }
            // Create a new server command to send to the server
            Support.Networking.ServerCommands.ServerCommandHug serverCommand = new Support.Networking.ServerCommands.ServerCommandHug(attachedApplication.client.clientID);
            serverCommand.arguments.Add(arguments[0]);
            // Send it up to the server
            attachedApplication.client.SendServerCommand(serverCommand);
        }
        // Initialize the command
        public CommandHug(CSACore application)
        {
            attachedApplication = application;
        }
    }
}

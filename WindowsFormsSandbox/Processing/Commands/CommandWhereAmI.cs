using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This is the client command that allows you see your chords
    class CommandWhereAmI : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: none

            // Make sure we are connected to a client
            if (!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            // Create a new server command to send to the server
            Support.Networking.ServerCommands.ServerCommandWhereAmI serverCommand = new Support.Networking.ServerCommands.ServerCommandWhereAmI(attachedApplication.client.clientID);   
            // Send it up to the server
            attachedApplication.client.SendServerCommand(serverCommand);
        }
        // Initialize the command
        public CommandWhereAmI(CSACore application)
        {
            attachedApplication = application;
        }
    }
}

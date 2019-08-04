using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This is the client command that allows you to say things in game
    class CommandLook : Command
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
            Support.Networking.ServerCommands.ServerCommandLook serverCommand = new Support.Networking.ServerCommands.ServerCommandLook(attachedApplication.client.clientID);
            // Send a look request to the server
            attachedApplication.client.SendServerCommand(serverCommand);
        }
        // Initialize the command
        public CommandLook(CSACore application)
        {
            attachedApplication = application;
        }
    }
}

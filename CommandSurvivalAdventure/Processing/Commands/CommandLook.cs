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
            // Check args
            if (arguments.Count == 1)
            {
                attachedApplication.output.PrintLine("$maUsage look at $ma<typeOfObjectToLookFor>");
                return;
            }
            // Create a new server command
            Support.Networking.ServerCommands.ServerCommandLook serverCommand = new Support.Networking.ServerCommands.ServerCommandLook(attachedApplication.client.clientID);
            if(arguments.Count > 0)
            {
                serverCommand.arguments.Add(arguments[0]);
                serverCommand.arguments.Add(arguments[1]);
            }           
            // Send a look request to the server
            attachedApplication.client.SendServerCommand(serverCommand);
        }
        // Initialize the command
        public CommandLook(Application application)
        {
            attachedApplication = application;
        }
    }
}

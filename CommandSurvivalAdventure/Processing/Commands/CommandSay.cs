using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This is the client command that allows you to say things in game
    class CommandSay : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: <stringToSay>

            // Make sure we are connected to a client
            if (!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            // Build the string to say from the arguments
            string stringToSay = "";
            foreach (string word in arguments)
                stringToSay += word + " ";
            // Create a new server command to send to the server
            Support.Networking.ServerCommands.ServerCommandSay serverCommand = new Support.Networking.ServerCommands.ServerCommandSay(attachedApplication.client.clientID);       
            serverCommand.arguments.Add(stringToSay);
            // Send it up to the server
            attachedApplication.client.SendServerCommand(serverCommand);
        }
        // Initialize the command
        public CommandSay(Application application)
        {
            attachedApplication = application;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This command parses the block command
    class CommandBlock : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: block <(l)eft|(r)ight|(h)ead>

            // Make sure we are connected to a client
            if(!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            if (arguments.Count > 0)
            {
                // Create a new server command
                Support.Networking.ServerCommands.ServerCommandBlock serverCommand = new Support.Networking.ServerCommands.ServerCommandBlock(attachedApplication.client.clientID);
                // The name of the tool we want to use
                string directionToBlock = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, 0, new List<string>() { }));
                // Make sure we have all the arguments
                if (directionToBlock == "")
                {
                    attachedApplication.output.PrintLine(Describer.ToColor("$ma", "block <(l)eft|(r)ight|(h)ead>"));
                    return;
                }
                // Send the parsed arguments
                serverCommand.arguments.Add(directionToBlock);
                // Send the request to the server
                attachedApplication.client.SendServerCommand(serverCommand);
            }
            else
                attachedApplication.output.PrintLine(Describer.ToColor("$ma", "block <(l)eft|(r)ight|(h)ead>"));
        }
        // Initialize the command
        public CommandBlock(CSACore application)
        {
            attachedApplication = application;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This command parses the stand command
    class CommandLunge : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: lunge at <nameOfObjectToLungeAt>

            // Make sure we are connected to a client
            if (!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }

            // Create a new server command
            Support.Networking.ServerCommands.ServerCommandLunge serverCommand = new Support.Networking.ServerCommands.ServerCommandLunge(attachedApplication.client.clientID);
            // The index of the first argument
            int indexOfFirstArgument = 0;
            // Ignore the "at" if there is one
            if (arguments[0] == "at")
                indexOfFirstArgument = 1;
            // The name of the tool we want to use
            string nameOfObjectToLungeAt = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, indexOfFirstArgument, new List<string>() { }));
            // Make sure we have all the arguments
            if (nameOfObjectToLungeAt == "")
            {
                attachedApplication.output.PrintLine(Describer.ToColor("lunge at <nameOfObjectToLungeAt>", "$ma"));
                return;
            }
            // Send the parsed arguments
            serverCommand.arguments.Add(nameOfObjectToLungeAt);
            // Send the request to the server
            attachedApplication.client.SendServerCommand(serverCommand);
        }
        // Initialize the command
        public CommandLunge(Application application)
        {
            attachedApplication = application;
        }
    }
}

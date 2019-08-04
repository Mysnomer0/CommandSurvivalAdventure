using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This client command parses the put command
    class CommandPut : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: put <nameOfObject> in/into <nameOfContainer>

            // Make sure we are connected to a client
            if(!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            if (arguments.Count > 0)
            {
                // Create a new server command
                Support.Networking.ServerCommands.ServerCommandPut serverCommand = new Support.Networking.ServerCommands.ServerCommandPut(attachedApplication.client.clientID);
                // The index of the second argument
                int indexOfSecondArgument = 0;
                // Get the name of the object
                string nameOfObject = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, 0, new List<string>() { "in", "into" }, ref indexOfSecondArgument));
                // The name of the container
                string nameOfContainer = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, indexOfSecondArgument, new List<string>() { }));
                // Make sure we have all the arguments
                if (nameOfObject == "" || nameOfContainer == "")
                {
                    attachedApplication.output.PrintLine(Describer.ToColor("$ma", "put <nameOfObject> in/into <nameOfContainer>"));
                    return;
                }
                // Send the parsed arguments
                serverCommand.arguments.Add(nameOfObject);
                serverCommand.arguments.Add(nameOfContainer);
                // Send the request to the server
                attachedApplication.client.SendServerCommand(serverCommand);
            }
            else
                attachedApplication.output.PrintLine(Describer.ToColor("$ma", "put <nameOfObject> in/into <nameOfContainer>"));
        }
        // Initialize the command
        public CommandPut(CSACore application)
        {
            attachedApplication = application;
        }
    }
}

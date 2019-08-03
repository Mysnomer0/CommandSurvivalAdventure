using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This is the client command that allows you to say things in game
    class CommandSharpen : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: sharpen <nameOfObject> with/using <nameOfTool>

            // Make sure we are connected to a client
            if(!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            if (arguments.Count > 0)
            {
                // Create a new server command
                Support.Networking.ServerCommands.ServerCommandSharpen serverCommand = new Support.Networking.ServerCommands.ServerCommandSharpen(attachedApplication.client.clientID);
                // The index of the second argument
                int indexOfSecondArgument = 0;
                // Get the name of the object
                string nameOfObject = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, 0, new List<string>() { "with", "using" }, ref indexOfSecondArgument));
                // The name of the tool we want to use
                string nameOfTool = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, indexOfSecondArgument, new List<string>() { }));
                // Make sure we have all the arguments
                if (nameOfObject == "" || nameOfTool == "")
                {
                    attachedApplication.output.PrintLine(Describer.ToColor("$ma", "sharpen <nameOfObject> with/using <nameOfTool>"));
                    return;
                }
                // Send the parsed arguments
                serverCommand.arguments.Add(nameOfObject);
                serverCommand.arguments.Add(nameOfTool);
                // Send the request to the server
                attachedApplication.client.SendServerCommand(serverCommand);
            }
            else
                attachedApplication.output.PrintLine(Describer.ToColor("$ma", "sharpen <nameOfObject> with/using <nameOfTool>"));
        }
        // Initialize the command
        public CommandSharpen(CSACore application)
        {
            attachedApplication = application;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This command processes the arguments for what you want to attach
    class CommandAttach : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: fasten/attach <nameOfObjectToAttach> to <nameOfObjectToAttachTo> with/using <nameOfObjectToAttachWith>

            // Make sure we are connected to a client
            if(!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            if (arguments.Count > 0)
            {
                // Create a new server command
                Support.Networking.ServerCommands.ServerCommandAttach serverCommand = new Support.Networking.ServerCommands.ServerCommandAttach(attachedApplication.client.clientID);

                // The index of the object to attach
                int indexOfObjectToAttach = 0;
                // The index of the object to attach to
                int indexOfObjectToAttachTo = 0;
                // The index of the object to attach with
                int indexOfObjectToAttachWith = 0;
                // Get the full name of the object to attach
                string fullNameOfObjectToAttach = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, indexOfObjectToAttach, new List<string> { "to" }, ref indexOfObjectToAttachTo));
                // After the to, get the full name of the object to attach to
                string fullNameOfObjectToAttachTo = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, indexOfObjectToAttachTo, new List<string> { "with", "using" }, ref indexOfObjectToAttachWith));
                // Finally, get the object to attach with
                string fullNameOfObjectToAttachWith = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, indexOfObjectToAttachWith, new List<string> { }));

                // If we're missing any objects, throw an error
                if (fullNameOfObjectToAttach == ""
                    || fullNameOfObjectToAttachTo == ""
                    || fullNameOfObjectToAttachWith == "")
                {
                    attachedApplication.output.PrintLine(Describer.ToColor("$ma", "Usage: fasten/attach <nameOfObjectToAttach> to <nameOfObjectToAttachTo> with/using <nameOfObjectToAttachWith>"));
                    return;
                }
                // Add on the arguments to the server command
                serverCommand.arguments.Add(fullNameOfObjectToAttach);
                serverCommand.arguments.Add(fullNameOfObjectToAttachTo);
                serverCommand.arguments.Add(fullNameOfObjectToAttachWith);
                // Send the request to the server
                attachedApplication.client.SendServerCommand(serverCommand);
            }
            else
                attachedApplication.output.PrintLine(Describer.ToColor("$ma","Usage: fasten/attach <nameOfObjectToAttach> to <nameOfObjectToAttachTo> with/using <nameOfObjectToAttachWith>"));
        }
        // Initialize the command
        public CommandAttach(CSACore application)
        {
            attachedApplication = application;
        }
    }
}

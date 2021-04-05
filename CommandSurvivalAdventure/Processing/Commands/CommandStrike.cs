using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // Parses the strike command
    class CommandStrike : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: strike/hit <nameOfObjectToStrike> with/using <nameOfObjectToUse> from <(l)eft/(r)ight>

            // Make sure we are connected to a client
            if (!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            if (arguments.Count > 0)
            {
                // Create a new server command
                Support.Networking.ServerCommands.ServerCommandStrike serverCommand = new Support.Networking.ServerCommands.ServerCommandStrike(attachedApplication.client.clientID);
                // The index of the second argument
                int indexOfSecondArgument = 0;
                // The index of the third argument
                int indexOfThirdArgument = 0;
                // Get the name of the object to strike
                string nameOfObjectToStrike = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, 0, new List<string>() { "with", "using" }, ref indexOfSecondArgument));
                // The name of the object to use
                string nameOfObjectToUse = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, indexOfSecondArgument, new List<string>() { "from" }, ref indexOfThirdArgument));
                // The direction to strike
                string directionToStrike = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, indexOfThirdArgument, new List<string>() { }));
                // Make sure we aren't missing any arguments
                if (nameOfObjectToStrike == "" || nameOfObjectToUse == "" || directionToStrike == "")
                {
                    attachedApplication.output.PrintLine(Describer.ToColor("Usage: strike/hit <nameOfObjectToStrike> with/using <nameOfObjectToUse> from <(l)eft/(r)ight>", "$ma"));
                    return;
                }
                // Send the parsed arguments
                serverCommand.arguments.Add(nameOfObjectToStrike);
                serverCommand.arguments.Add(nameOfObjectToUse);
                serverCommand.arguments.Add(directionToStrike);
                // Send the request to the server
                attachedApplication.client.SendServerCommand(serverCommand);
            }
            else
                attachedApplication.output.PrintLine(Describer.ToColor("Usage: strike/hit <nameOfObjectToStrike> with/using <nameOfObjectToUse> from <(l)eft/(r)ight>", "$ma"));
        }
        // Initialize the command
        public CommandStrike(Application application)
        {
            attachedApplication = application;
        }
    }
}

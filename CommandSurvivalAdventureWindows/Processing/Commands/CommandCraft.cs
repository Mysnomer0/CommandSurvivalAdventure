using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This is the client command that allows you to say things in game
    class CommandCraft : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // ARGS: craft/make <nameOfCraftingRecipe> with/using <nameOfIngredient>

            // Make sure we are connected to a client
            if (!attachedApplication.client.isConnected)
            {
                attachedApplication.output.PrintLine("$maNot $maconnected $mato $maany $maserver. $maMake $masure $mayour $maclient $mais $maconnected.");
                return;
            }
            if (arguments.Count > 0)
            {
                // Create a new server command
                Support.Networking.ServerCommands.ServerCommandCraft serverCommand = new Support.Networking.ServerCommands.ServerCommandCraft(attachedApplication.client.clientID);
                // The index of the second argument
                int indexOfSecondArgument = 0;
                // Get the crafting recipe name
                string craftingRecipe = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, 0, new List<string>() { "with", "using", "from" }, ref indexOfSecondArgument));
                // The name of the ingredient to use
                string ingredient = Parser.ScrubArticles(Parser.GetSubStringUpToWord(arguments, indexOfSecondArgument, new List<string>() { }));
                // Make sure we aren't missing any arguments
                if (craftingRecipe == "" || ingredient == "")
                {
                    attachedApplication.output.PrintLine(Describer.ToColor("$ma","Usage: craft/make <nameOfCraftingRecipe> with/using <nameOfIngredient>"));
                    return;
                }
                // Send the parsed arguments
                serverCommand.arguments.Add(craftingRecipe);
                serverCommand.arguments.Add(ingredient);
                // Send the request to the server
                attachedApplication.client.SendServerCommand(serverCommand);
            }
            else
                attachedApplication.output.PrintLine(Describer.ToColor("$ma", "Usage: craft/make <nameOfCraftingRecipe> with/using <nameOfIngredient>"));
        }
        // Initialize the command
        public CommandCraft(CSACore application)
        {
            attachedApplication = application;
        }
    }
}

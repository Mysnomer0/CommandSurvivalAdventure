using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using Humanizer;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandCraft : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <nameOfCraftingRecipe> <nameOfIngredient>

            // Translate the name of the ingredient to a singular version, in case someone says "craft rope from vines"
            string nameOfIngredient = givenArguments[1].Singularize(false);
            // The avaliable ingredients
            List<World.GameObject> avaliableIngredients = server.world.FindGameObjects(nameOfIngredient, sender.position);
            // The ingredients that we are going to try to use
            List<World.GameObject> ingredientsToUse = new List<World.GameObject>();
            
            #region Make sure the recipe exists
            // First off, make sure the crafting recipe exists
            if(!server.attachedApplication.recipeDatabase.CheckRecipe(givenArguments[0]))
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add(givenArguments[0] + " is not a valid crafting recipe. :(");
                server.SendRPC(error, nameOfSender);
                return;
            }
            #endregion

            #region Get the ingredients to use
            // Make sure that the necessary ingredients are avaliable

            // Refine the avaliable ingredients down to the ones that match the recipe
            foreach (World.GameObject ingredient in avaliableIngredients)
            {
                // If the current ingredient is one that can be used for this recipe, add it
                if(server.attachedApplication.recipeDatabase.GetRecipe(givenArguments[0]).ingredients.ContainsKey(ingredient.type))
                    ingredientsToUse.Add(ingredient);
            }
            #endregion

            #region Make sure the ingredients can be used to craft the recipe
            if (ingredientsToUse.Count == 0)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("You can't use " + givenArguments[1].Pluralize(false) + " to craft " + Processing.Describer.GetArticle(givenArguments[0]) + " " + givenArguments[0] + ", or else there aren't any nearby.");
                server.SendRPC(error, nameOfSender);
                return;
            }
            #endregion

            #region Consume the ingredients
            // Get the amount of ingredients we need
            int amountOfIngredients = server.attachedApplication.recipeDatabase.GetRecipe(givenArguments[0]).ingredients[ingredientsToUse[0].type];
            // If there are enough ingredients, consume them
            if (ingredientsToUse.Count >= amountOfIngredients)
            {
                // Loop through the ingredients and consume the proper amount
                for(int i = 0; i < amountOfIngredients; i++)
                {
                    ingredientsToUse[i].parent.RemoveChild(ingredientsToUse[i]);
                    server.world.RemoveChild(ingredientsToUse[i]);
                }
            }
            // Otherwise, throw an error saying there aren't enough avaliable ingredients
            else
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("There aren't enough " + nameOfIngredient.Pluralize(false)
                    + " avaliable.  It takes " + amountOfIngredients.ToString() + " and only " + ingredientsToUse.Count.ToString() + " avaliable.");
                server.SendRPC(error, nameOfSender);
                return;
            }
            #endregion

            #region Create the output object
            // With the ingredients consumed, now create the new object to output
            dynamic objectToOutput = Activator.CreateInstance(server.attachedApplication.recipeDatabase.GetRecipe(givenArguments[0]).output);
            // Let it inherit the adjectives, like "long acacia"
            objectToOutput.identifier.classifierAdjectives.Add(ingredientsToUse[0].identifier.name);
            objectToOutput.identifier.classifierAdjectives.InsertRange(0, ingredientsToUse[0].identifier.classifierAdjectives);
            objectToOutput.identifier.descriptiveAdjectives.InsertRange(0, ingredientsToUse[0].identifier.descriptiveAdjectives);

            // Tell the player in the present tense that they are completing the action
            string presentTenseConfirmationString = "You are crafting " + Processing.Describer.GetArticle(objectToOutput.identifier.fullName) + " " + objectToOutput.identifier.fullName + "...";
            RPCs.RPCSay presentTenseConfirmation = new RPCs.RPCSay();
            presentTenseConfirmation.arguments.Add(presentTenseConfirmationString);
            server.SendRPC(presentTenseConfirmation, nameOfSender);
            // Loop the elipsis three times and send it to the player, implying work being done
            for (int i = 0; i < 5000 / 1000; i++)
            {
                Thread.Sleep(1000);
                RPCs.RPCSay elipsis = new RPCs.RPCSay();
                elipsis.arguments.Add("...");
                server.SendRPC(elipsis, nameOfSender);
            }

            // Attach it to our chunk
            server.world.GetChunkOrGenerate(sender.position).AddChild(objectToOutput);
            #endregion

            #region Confirm that the object was crafted with a message
            // Confirm to the sender that they crafted the object
            RPCs.RPCSay confirmation = new RPCs.RPCSay();
            if(amountOfIngredients == 1)
                confirmation.arguments.Add("You crafted " + Processing.Describer.GetArticle(objectToOutput.identifier.fullName) + " " + objectToOutput.identifier.fullName + " from " + amountOfIngredients.ToString() + " " + nameOfIngredient + ".");
            else
                confirmation.arguments.Add("You crafted " + Processing.Describer.GetArticle(objectToOutput.identifier.fullName) + " " + objectToOutput.identifier.fullName + " from " + amountOfIngredients.ToString() + " " + nameOfIngredient.Pluralize(false) + ".");
            server.SendRPC(confirmation, nameOfSender);
            // Let everyone else in the chunk know that this player crafted something
            RPCs.RPCSay information = new RPCs.RPCSay();
            information.arguments.Add(nameOfSender + " crafted " + Processing.Describer.GetArticle(objectToOutput.identifier.fullName) + " " + objectToOutput.identifier.fullName + ".");
            // Find everyone in the same chunk and let them know
            foreach (KeyValuePair<string, Core.Player> playerEntry in server.world.players)
            {
                // If this player is at the same position but doesn't have the same name as the sender
                if (playerEntry.Key != nameOfSender && playerEntry.Value.controlledGameObject.position == sender.position)
                    // Send an informational RPC to them letting them know
                    server.SendRPC(information, playerEntry.Key);
            }
            #endregion
        }
        public ServerCommandCraft(string nameOfSender)
        {
            type = typeof(ServerCommandCraft);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandCraft()
        {
            type = typeof(ServerCommandCraft);
        }
    }
}

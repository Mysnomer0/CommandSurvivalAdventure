using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Attaches the given object to the other one using the other given object
    class ServerCommandAttach : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <objectToAttach> <objectToAttachTo> <objectToAttachWith>

            // If dead
            if (sender.specialProperties["isDeceased"] == "TRUE")
            {
                RPCs.RPCSay failure = new RPCs.RPCSay();
                failure.arguments.Add("You are deceased.");
                server.SendRPC(failure, nameOfSender);
                return;
            }

            // Get each of the objects necessary to do the attaching
            World.GameObject objectToAttach = server.world.FindFirstGameObject(givenArguments[0], sender.position);
            World.GameObject objectToAttachTo = server.world.FindFirstGameObject(givenArguments[1], sender.position);
            World.GameObject objectToAttachWith = server.world.FindFirstGameObject(givenArguments[2], sender.position);

            // Make sure the objects exist
            if (objectToAttach == null)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("There is no nearby " + givenArguments[0] + ".");
                server.SendRPC(error, nameOfSender);
                return;
            }
            if (objectToAttachTo == null)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("There is no nearby " + givenArguments[1] + ".");
                server.SendRPC(error, nameOfSender);
                return;
            }
            if (objectToAttachWith == null)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("There is no nearby " + givenArguments[2] + ".");
                server.SendRPC(error, nameOfSender);
                return;
            }
            // If the object we're trying to attach with can't be used to fasten with, throw an error
            if (!objectToAttachWith.specialProperties.ContainsKey("isFastenable"))
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("You can't use " + Processing.Describer.GetArticle(objectToAttachWith.identifier.fullName) + " " + objectToAttachWith.identifier.fullName + " to fasten with.");
                server.SendRPC(error, nameOfSender);
                return;
            }

            // Tell the player in the present tense that they are completing the action
            string presentTenseConfirmationString = "You are attaching " + Processing.Describer.GetArticle(objectToAttach.identifier.fullName) + " " + objectToAttach.identifier.fullName
                + " to " + Processing.Describer.GetArticle(objectToAttachTo.identifier.fullName) + " " + objectToAttachTo.identifier.fullName
                + " using " + Processing.Describer.GetArticle(objectToAttachWith.identifier.fullName) + " " + objectToAttachWith.identifier.fullName + "...";
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
            // Attach the objects together
            objectToAttachWith.AddChild(objectToAttach);
            objectToAttachTo.AddChild(objectToAttachWith);
            // The string we will send back as confirmation
            string confirmationString = "You attached " + Processing.Describer.GetArticle(objectToAttach.identifier.fullName) + " " + objectToAttach.identifier.fullName
                + " to " + Processing.Describer.GetArticle(objectToAttachTo.identifier.fullName) + " " + objectToAttachTo.identifier.fullName
                + " using " + Processing.Describer.GetArticle(objectToAttachWith.identifier.fullName) + " " + objectToAttachWith.identifier.fullName + ".";
            // The string we may or may not add
            string additionalConfirmation = "";
            // Check to see whether or not the object becomes a crafting combination
            World.Crafting.CraftingCombination possibleCombination = server.attachedApplication.recipeDatabase.CheckObjectForCombination(objectToAttachTo);
            // If so, rename the new object and have it inherit the classifier adjectives of the object we are attaching to it
            if (possibleCombination != null)
            {
                objectToAttachTo.identifier.classifierAdjectives.Clear();
                objectToAttachTo.identifier.classifierAdjectives = objectToAttach.identifier.classifierAdjectives;
                objectToAttachTo.identifier.name = possibleCombination.newName;
                additionalConfirmation = "\nYou completed " + Processing.Describer.GetArticle(objectToAttachTo.identifier.fullName) + " " + objectToAttachTo.identifier.fullName + ".";
            }
            // Confirm to the sender that they successfully attached the object
            RPCs.RPCSay confirmation = new RPCs.RPCSay();
            confirmation.arguments.Add(confirmationString + additionalConfirmation);
            server.SendRPC(confirmation, nameOfSender);
            // Let everyone else in the chunk know that this player picked up something
            RPCs.RPCSay information = new RPCs.RPCSay();
            information.arguments.Add(nameOfSender + confirmationString.Substring(1));

            // Find everyone in the same chunk and let them know
            foreach (KeyValuePair<string, Core.Player> playerEntry in server.world.players)
            {
                // If this player is not the one that picked something up, and it's position is the same as the original player's position
                if (playerEntry.Key != nameOfSender && playerEntry.Value.controlledGameObject.position == sender.position)
                    // Send an informational RPC to them letting them know
                    server.SendRPC(information, playerEntry.Key);
            }
        }
        public ServerCommandAttach(string nameOfSender)
        {
            type = typeof(ServerCommandAttach);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandAttach()
        {
            type = typeof(ServerCommandAttach);
        }
    }
}

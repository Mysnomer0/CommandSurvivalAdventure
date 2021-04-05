using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandDull : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <nameOfObjectToDull> <nameOfObjectToDullWith>

            // Get the object to dull
            World.GameObject objectToDull = server.world.FindFirstGameObject(givenArguments[0], sender.position);
            // Get the object to dull with
            World.GameObject objectToDullWith = server.world.FindFirstGameObject(givenArguments[1], sender.position);

            // Make sure both the objects exist
            if(objectToDull == null)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("There is no nearby " + givenArguments[0] + ".");
                server.SendRPC(error, nameOfSender);
                return;
            }
            if (objectToDullWith == null)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("There is no nearby " + givenArguments[1] + ".");
                server.SendRPC(error, nameOfSender);
                return;
            }
            // Check if the object to dull with can be used
            if (!objectToDullWith.specialProperties.ContainsKey("canBeDulledWith") || objectToDull == objectToDullWith)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("You can't use the " + objectToDullWith.identifier.fullName + " to dull the " + objectToDull.identifier.fullName + ".");
                server.SendRPC(error, nameOfSender);
                return;
            }

            // Tell the player in the present tense that they are completing the action
            string presentTenseConfirmationString = "You are Dulling " + Processing.Describer.GetArticle(objectToDull.identifier.fullName) + " " + objectToDull.identifier.fullName + "...";
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

            // Make the object dull
            objectToDull.identifier.descriptiveAdjectives.Add("blunt");
            // If the object was sharp make shure its not
            if (objectToDull.identifier.descriptiveAdjectives.Contains("sharp"))
                objectToDull.identifier.descriptiveAdjectives.Remove("sharp");

            // Confirm to the sender that they dulled the object
            RPCs.RPCSay confirmation = new RPCs.RPCSay();
            confirmation.arguments.Add("You dulled the " + objectToDull.identifier.fullName + " with the " + objectToDullWith.identifier.fullName + ".");
            server.SendRPC(confirmation, nameOfSender);
            // Let everyone else in the chunk know that this player dulled something
            RPCs.RPCSay information = new RPCs.RPCSay();
            information.arguments.Add(nameOfSender + " sharpened a " + objectToDull.identifier.fullName + " with a " + objectToDullWith.identifier.fullName + ".");
            // Find everyone in the same chunk and let them know
            foreach (KeyValuePair<string, Core.Player> playerEntry in server.world.players)
            {
                // If this player is not the one that picked something up, and it's position is the same as the original player's position
                if (playerEntry.Key != nameOfSender && playerEntry.Value.controlledGameObject.position == sender.position)
                    // Send an informational RPC to them letting them know
                    server.SendRPC(information, playerEntry.Key);
            }
        }
        public ServerCommandDull(string nameOfSender)
        {
            type = typeof(ServerCommandDull);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandDull()
        {
            type = typeof(ServerCommandDull);
        }
    }
}

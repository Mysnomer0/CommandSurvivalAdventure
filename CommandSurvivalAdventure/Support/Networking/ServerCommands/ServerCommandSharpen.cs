using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandSharpen : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <nameOfObjectToSharpen> <nameOfObjectToSharpenWith>

            // Get the object to sharpen
            World.GameObject objectToSharpen = server.world.FindFirstGameObject(givenArguments[0], sender.position);
            // Get the object to sharpen with
            World.GameObject objectToSharpenWith = server.world.FindFirstGameObject(givenArguments[1], sender.position);

            // Make sure both the objects exist
            if(objectToSharpen == null)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("There is no nearby " + givenArguments[0] + ".");
                server.SendRPC(error, nameOfSender);
                return;
            }
            if (objectToSharpenWith == null)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("There is no nearby " + givenArguments[1] + ".");
                server.SendRPC(error, nameOfSender);
                return;
            }
            // Check if the object to sharpen with can be used
            if (!objectToSharpenWith.specialProperties.ContainsKey("canBeSharpenedWith") || objectToSharpen == objectToSharpenWith)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("You can't use the " + objectToSharpenWith.identifier.fullName + " to sharpen the " + objectToSharpen.identifier.fullName + ".");
                server.SendRPC(error, nameOfSender);
                return;
            }

            // Tell the player in the present tense that they are completing the action
            string presentTenseConfirmationString = "You are sharpening " + Processing.Describer.GetArticle(objectToSharpen.identifier.fullName) + " " + objectToSharpen.identifier.fullName + "...";
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

            // Make the object sharp
            objectToSharpen.identifier.descriptiveAdjectives.Add("sharp");
            // If the object was blunt make shure its not
            if (objectToSharpen.identifier.descriptiveAdjectives.Contains("blunt"))
                objectToSharpen.identifier.descriptiveAdjectives.Remove("blunt");

            // Confirm to the sender that they sharpend the object
            RPCs.RPCSay confirmation = new RPCs.RPCSay();
            confirmation.arguments.Add("You sharpened the " + objectToSharpen.identifier.fullName + " with the " + objectToSharpenWith.identifier.fullName + ".");
            server.SendRPC(confirmation, nameOfSender);
            // Let everyone else in the chunk know that this player dropped something
            RPCs.RPCSay information = new RPCs.RPCSay();
            information.arguments.Add(nameOfSender + " sharpened a " + objectToSharpen.identifier.fullName + " with a " + objectToSharpenWith.identifier.fullName + ".");
            // Find everyone in the same chunk and let them know
            foreach (KeyValuePair<string, Core.Player> playerEntry in server.world.players)
            {
                // If this player is not the one that picked something up, and it's position is the same as the original player's position
                if (playerEntry.Key != nameOfSender && playerEntry.Value.controlledGameObject.position == sender.position)
                    // Send an informational RPC to them letting them know
                    server.SendRPC(information, playerEntry.Key);
            }
        }
        public ServerCommandSharpen(string nameOfSender)
        {
            type = typeof(ServerCommandSharpen);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandSharpen()
        {
            type = typeof(ServerCommandSharpen);
        }
    }
}

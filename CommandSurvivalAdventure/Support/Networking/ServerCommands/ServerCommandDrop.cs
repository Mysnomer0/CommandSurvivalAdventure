using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandDrop : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <nameOfObjectToDrop>

            // If dead
            if (sender.specialProperties["isDeceased"] == "TRUE")
            {
                RPCs.RPCSay failure = new RPCs.RPCSay();
                failure.arguments.Add("You are deceased.");
                server.SendRPC(failure, nameOfSender);
                return;
            }

            // Get the hashset of the utilizable parts of the player, like the hands, tail, etc
            List<World.GameObject> utilizableParts = sender.FindChildrenWithSpecialProperty("isUtilizable");
            // The gameObject that we will drop the object from, which will be determined
            World.GameObject utilizablePart = null;
            // The gameObject that we will drop
            World.GameObject gameObjectToDrop = null;

            #region Determine the utilizablePart to drop the object
            // First off, check if we even have a hand that can hold an object
            if (utilizableParts.Count == 0)
                return;
            // Find the first utilizable part that is holding the object with the name
            foreach(World.GameObject part in utilizableParts)
            {
                if(part.children.Count == 1 && part.children.First().identifier.DoesStringPartiallyMatchFullName(givenArguments[0]))
                {
                    utilizablePart = part;
                    break;
                }
            }
            // If it fails, send an appropriate message back
            if (utilizablePart == null)
            {
                RPCs.RPCSay failure = new RPCs.RPCSay();
                failure.arguments.Add("You are not holding " + Processing.Describer.GetArticle(givenArguments[0]) + " " + givenArguments[0] + ".");
                server.SendRPC(failure, nameOfSender);
                return;
            }
            #endregion

            #region Drop the object and send appripriate messages to the sender and everyone else in the chunk
            // Get the object to drop
            gameObjectToDrop = utilizablePart.children.First();
            // Add the object to the chunk
            server.world.GetChunkOrGenerate(sender.position).AddChild(gameObjectToDrop);
            // Remove it from the utilizable part
            utilizablePart.RemoveChild(gameObjectToDrop);
            // Confirm to the sender that they dropped the object
            RPCs.RPCSay confirmation = new RPCs.RPCSay();
            confirmation.arguments.Add("You dropped the " + gameObjectToDrop.identifier.fullName + " in your " + utilizablePart.identifier.fullName + ".");
            server.SendRPC(confirmation, nameOfSender);
            // Let everyone else in the chunk know that this player dropped something
            RPCs.RPCSay information = new RPCs.RPCSay();
            information.arguments.Add(nameOfSender + " dropped " + Processing.Describer.GetArticle(gameObjectToDrop.identifier.fullName) + " " + gameObjectToDrop.identifier.fullName + ".");
            // Find everyone in the same chunk and let them know
            foreach (KeyValuePair<string, Core.Player> playerEntry in server.world.players)
            {
                // If this player is not the one that picked something up, and it's position is the same as the original player's position
                if (playerEntry.Key != nameOfSender && playerEntry.Value.controlledGameObject.position == sender.position)
                    // Send an informational RPC to them letting them know
                    server.SendRPC(information, playerEntry.Key);
            }            
            #endregion
        }
        public ServerCommandDrop(string nameOfSender)
        {
            type = typeof(ServerCommandDrop);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandDrop()
        {
            type = typeof(ServerCommandDrop);
        }
    }
}

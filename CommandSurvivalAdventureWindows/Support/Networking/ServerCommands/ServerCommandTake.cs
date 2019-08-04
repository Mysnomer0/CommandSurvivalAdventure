using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandTake : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <nameOfObjectToPickUp> <OPTIONAL:nameOfObjectToPickUpWith>

            // Get the hashset of the utilizable parts of the player, like the hands, tail, etc
            List<World.GameObject> utilizableParts = sender.FindChildrenWithSpecialProperty("isUtilizable");
            // The gameObject that we will take the object with, which will be determined
            World.GameObject utilizablePart = null;

            #region Determine the object to take the object
            // First off, check if we even have a hand that can hold an object
            if (utilizableParts.Count == 0)
                return;
            // If there was a specified part we were asked to take the object with, use that
            if(givenArguments.Count == 3)
            {
                // Loop through and find the part with the name
                foreach(World.GameObject part in utilizableParts)
                {
                    if(part.identifier.DoesStringPartiallyMatchFullName(givenArguments[1]))
                    {
                        utilizablePart = part;
                        break;
                    }
                }
                // If it fails, send an appropriate message back
                if(utilizablePart == null)
                {
                    RPCs.RPCSay confirmation = new RPCs.RPCSay();
                    confirmation.arguments.Add("You have no " + givenArguments[1] + ".");
                    server.SendRPC(confirmation, nameOfSender);
                    return;
                }
                else if(utilizablePart.children.Count > 0)
                {
                    RPCs.RPCSay confirmation = new RPCs.RPCSay();
                    confirmation.arguments.Add("Your " + givenArguments[1] + " is being used.");
                    server.SendRPC(confirmation, nameOfSender);
                    return;
                }
            }
            // Otherwise
            else
            {
                // Find the first utilizable part that is not holding something already and go with that
                foreach(World.GameObject part in utilizableParts)
                {
                    if(part.children.Count == 0)
                    {
                        utilizablePart = part;
                        break;
                    }
                }
                // If it fails, send an appropriate message back
                if (utilizablePart == null)
                {
                    RPCs.RPCSay confirmation = new RPCs.RPCSay();
                    confirmation.arguments.Add("You have nothing to hold the " + givenArguments[0] + " with.");
                    server.SendRPC(confirmation, nameOfSender);
                    return;
                }
            }
            #endregion

            #region Find the object, pick it up, and send appripriate messages to the sender and everyone else in the chunk

            // Find possible gameObjects to pick up
            List<World.GameObject> gameObjectToPickUp = server.world.GetChunk(sender.position).FindChildrenWithName(givenArguments[0]);
            // If no gameObject was found, send back an error
            if(gameObjectToPickUp.Count == 0)
            {
                // If this fails, let the player know
                RPCs.RPCSay failure = new RPCs.RPCSay();
                failure.arguments.Add("There is no nearby " + givenArguments[0] + ".");
                server.SendRPC(failure, nameOfSender);
                return;
            }
            // If the first gameObject we found that is a canidate to pick up isn't holding us or anything weird like that
            if (gameObjectToPickUp.First().ContainsChild(nameOfSender) == null && gameObjectToPickUp.First() != sender)
            {
                // Put the object in the players utilizable part, usually a hand
                utilizablePart.AddChild(gameObjectToPickUp.First());
                // Remove it from the chunk
                server.world.GetChunk(sender.position).RemoveChild(gameObjectToPickUp.First());
                // Confirm to the sender that they picked up the object
                RPCs.RPCSay confirmation = new RPCs.RPCSay();
                confirmation.arguments.Add("You picked up " + Processing.Describer.GetArticle(gameObjectToPickUp.First().identifier.fullName) + " " + gameObjectToPickUp.First().identifier.fullName + " in your " + utilizablePart.identifier.fullName + ".");
                server.SendRPC(confirmation, nameOfSender);
                // Let everyone else in the chunk know that this player picked up something
                RPCs.RPCSay information = new RPCs.RPCSay();
                information.arguments.Add(nameOfSender + " picked up " + Processing.Describer.GetArticle(gameObjectToPickUp.First().identifier.fullName) + " " + gameObjectToPickUp.First().identifier.fullName + ".");
                // Find everyone in the same chunk and let them know
                foreach (KeyValuePair<string, Core.Player> playerEntry in server.world.players)
                {
                    // If this player is not the one that picked something up, and it's position is the same as the original player's position
                    if (playerEntry.Key != nameOfSender && playerEntry.Value.controlledGameObject.position == sender.position)
                        // Send an informational RPC to them letting them know
                        server.SendRPC(information, playerEntry.Key);
                }
            }
            
            #endregion
        }
        public ServerCommandTake(string nameOfSender)
        {
            type = typeof(ServerCommandTake);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandTake()
        {
            type = typeof(ServerCommandTake);
        }
    }
}

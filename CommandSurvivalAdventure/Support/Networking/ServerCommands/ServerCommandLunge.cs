using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the sender to assume a standing stance
    class ServerCommandLunge : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <objectToLungeAt>

            // Get the object to lunge at
            World.GameObject objectToLungeAt = server.world.FindFirstGameObject(givenArguments[1], sender.position);
            /*
            // Make sure there is an object to lunge at
            if(objectToLungeAt == null)
            {
                RPCs.RPCSay rPC = new RPCs.RPCSay();
                rPC.arguments.Add(response);
                server.SendRPC(rPC, nameOfSender);
            }*/
            // The message we're going to send back
            string response = "You lunge at ";
            // If the sender is already standing, change the response accordingly
            if (sender.specialProperties["stance"] == World.Creature.StanceToString(World.Creature.Stances.STANDING))
                response = "You are already standing.";
            // Otherwise, if you aren't in a stance that can transition
            else if (!World.Creature.CheckStanceTransition(World.Creature.StringToStance(sender.specialProperties["stance"]), World.Creature.Stances.STANDING))
                response = "You can't stand while " + sender.specialProperties["stance"].ToLower() + ".";
            // Send info back to the sender
            RPCs.RPCSay rPC = new RPCs.RPCSay();
            rPC.arguments.Add(response);
            server.SendRPC(rPC, nameOfSender);
            // If we actually were able to perform the action
            if(response == "You stand up.")
            {
                // Change the stance of the sender
                sender.specialProperties["stance"] = World.Creature.StanceToString(World.Creature.Stances.STANDING);
                // Notify everyone in the chunk
                foreach (World.GameObject gameObject in server.world.GetChunk(sender.position).children)
                {
                    if (gameObject.specialProperties.ContainsKey("isPlayer") && gameObject.identifier.name != sender.identifier.name)
                    {
                        // Send a message to this player saying that we left the chunk
                        RPCs.RPCSay newRPC = new RPCs.RPCSay();
                        newRPC.arguments.Add(nameOfSender + " stood up.");
                        server.SendRPC(newRPC, gameObject.identifier.name);
                    }
                }
            }
        }
        public ServerCommandLunge(string nameOfSender)
        {
            type = typeof(ServerCommandLunge);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandLunge()
        {
            type = typeof(ServerCommandLunge);
        }
    }
}

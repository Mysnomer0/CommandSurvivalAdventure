using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the sender to assume a crouching stance
    class ServerCommandLie : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: none

            // The message we're going to send back
            string response = "You lie down.";
            // If the sender is already in the stance, change the response accordingly
            if (sender.specialProperties["stance"] == World.Creature.StanceToString(World.Creature.Stances.LAYING))
                response = "You are already laying down.";
            // Otherwise, if you aren't in a stance that can transition
            else if (!World.Creature.CheckStanceTransition(World.Creature.StringToStance(sender.specialProperties["stance"]), World.Creature.Stances.LAYING))
                response = "You can't lie down while " + sender.specialProperties["stance"].ToLower() + ".";
            else
            {
                string lieDownstring = "You are lying down...";
                RPCs.RPCSay lieDownRPC = new RPCs.RPCSay();
                lieDownRPC.arguments.Add(lieDownstring);
                server.SendRPC(lieDownRPC, nameOfSender);
                Thread.Sleep(1000);
            }
            // Send info back to the sender
            RPCs.RPCSay rPC = new RPCs.RPCSay();
            rPC.arguments.Add(response);
            server.SendRPC(rPC, nameOfSender);
            // If we actually were able to perform the action
            if(response == "You lie down.")
            {
                // Change the stance of the sender
                sender.specialProperties["stance"] = World.Creature.StanceToString(World.Creature.Stances.LAYING);
                // Notify everyone in the chunk
                foreach (World.GameObject gameObject in server.world.GetChunkOrGenerate(sender.position).children)
                {
                    if (gameObject.specialProperties.ContainsKey("isPlayer") && gameObject.identifier.name != sender.identifier.name)
                    {
                        // Send a message to this player saying that we left the chunk
                        RPCs.RPCSay newRPC = new RPCs.RPCSay();
                        newRPC.arguments.Add(nameOfSender + " lays down.");
                        server.SendRPC(newRPC, gameObject.identifier.name);
                    }
                }
            }
        }
        public ServerCommandLie(string nameOfSender)
        {
            type = typeof(ServerCommandLie);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandLie()
        {
            type = typeof(ServerCommandLie);
        }
    }
}

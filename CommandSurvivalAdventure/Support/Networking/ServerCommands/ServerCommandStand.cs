﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the sender to assume a standing stance
    class ServerCommandStand : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: none

            // If dead
            if (sender.specialProperties["isDeceased"] == "TRUE")
            {
                RPCs.RPCSay failure = new RPCs.RPCSay();
                failure.arguments.Add("You are deceased.");
                server.SendRPC(failure, nameOfSender);
                return;
            }

            // The message we're going to send back
            string response = "You stood up.";
            // If the sender is already standing, change the response accordingly
            if (sender.specialProperties["stance"] == World.Creature.StanceToString(World.Creature.Stances.STANDING))
                response = "You are already standing.";
            // Otherwise, if you aren't in a stance that can transition
            else if (!World.Creature.CheckStanceTransition(World.Creature.StringToStance(sender.specialProperties["stance"]), World.Creature.Stances.STANDING))
                response = "You can't stand while " + sender.specialProperties["stance"].ToLower() + ".";
            else
            {
                string standUp = "You are standing up...";
                RPCs.RPCSay standUpRPC = new RPCs.RPCSay();
                standUpRPC.arguments.Add(standUp);
                server.SendRPC(standUpRPC, nameOfSender);
                Thread.Sleep(1000);
            }
                
            
            // Send info back to the sender
            RPCs.RPCSay rPC = new RPCs.RPCSay();
            rPC.arguments.Add(response);
            server.SendRPC(rPC, nameOfSender);
            // If we actually were able to perform the action
            if(response == "You stood up.")
            {
                // Change the stance of the sender
                sender.specialProperties["stance"] = World.Creature.StanceToString(World.Creature.Stances.STANDING);
                // Notify everyone in the chunk
                foreach (World.GameObject gameObject in server.world.GetChunkOrGenerate(sender.position).children)
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
        public ServerCommandStand(string nameOfSender)
        {
            type = typeof(ServerCommandStand);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandStand()
        {
            type = typeof(ServerCommandStand);
        }
    }
}

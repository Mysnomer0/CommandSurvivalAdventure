using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandClientConnectRequest : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: none

            // First off, if there is someone already connected with the same name, refuse the connection
            if (server.world.players.ContainsKey(arguments[0]))
            {
                // Send back an error saying someone with the same name is already online
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("Connection refused. :(  Someone with the name " + givenArguments[0] + " is already connected.");
                // Send the RPCSay to all clients
                server.SendRPC(error, nameOfSender);

                return;
            }
            // If the player is not yet on the world, add them
            else
            {
                // Create the creature to play as
                World.Creatures.CreatureHuman newPlayer = new World.Creatures.CreatureHuman(server.attachedApplication);
                newPlayer.specialProperties.Add("isPlayer", "");
                newPlayer.Generate(server.world.seed);
                // Add the creature into the spawn chunk.  This method may change later, depending on how we want to spawn in stuff.
                server.world.AddPlayer(givenArguments[0], newPlayer, new World.Position(0, 0, 0));
                // Send the confirmation back to the sender
                RPCs.RPCClientConnect rPCClientConnect = new RPCs.RPCClientConnect();
                server.SendRPC(rPCClientConnect);
                // Create a new RPC
                RPCs.RPCSay newRPC = new RPCs.RPCSay();
                newRPC.arguments.Add(givenArguments[0] + " just connected!");
                // Send the RPCSay to all clients
                server.SendRPC(newRPC);
            }
        }
        public ServerCommandClientConnectRequest(string nameOfSender)
        {
            type = typeof(ServerCommandClientConnectRequest);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandClientConnectRequest()
        {
            type = typeof(ServerCommandClientConnectRequest);
        }
    }
}

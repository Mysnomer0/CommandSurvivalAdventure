using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandLook : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS:

            // Describe the current chunk that the sender is on
            string description = Processing.Describer.Describe(server.world.chunks[sender.position.x][sender.position.y][sender.position.z], sender);
            // Send this new string back to the sender
            RPCs.RPCSay newRPC = new RPCs.RPCSay();
            newRPC.arguments.Add(description);
            server.SendRPC(newRPC, nameOfSender);
        }
        public ServerCommandLook(string nameOfSender)
        {
            type = typeof(ServerCommandLook);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandLook()
        {
            type = typeof(ServerCommandLook);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Send this to a server and it will respond back saying it is online
    class ServerCommandPing : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS:

            // Create a new RPC
            RPCs.RPCSay newRPC = new RPCs.RPCSay();
            newRPC.arguments.Add(server.sendCommandsTopic + " is online!");
            server.SendRPC(newRPC);
        }
        public ServerCommandPing(string nameOfSender)
        {
            type = typeof(ServerCommandPing);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandPing()
        {
            type = typeof(ServerCommandPing);
        }
    }
}

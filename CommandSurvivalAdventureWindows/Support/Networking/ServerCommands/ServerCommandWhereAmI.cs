using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandWhereAmI : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS:

            // Create a new RPC
            RPCs.RPCSay newRPC = new RPCs.RPCSay();
            // Send back the position
            newRPC.arguments.Add("Your position is " + sender.position.x + " | " + sender.position.y + " | " + sender.position.z + ".");
            // Send the Rpc back to the sender
            server.SendRPC(newRPC, sender.identifier.name);
        }
        public ServerCommandWhereAmI(string nameOfSender)
        {
            type = typeof(ServerCommandWhereAmI);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandWhereAmI()
        {
            type = typeof(ServerCommandWhereAmI);
        }
    }
}

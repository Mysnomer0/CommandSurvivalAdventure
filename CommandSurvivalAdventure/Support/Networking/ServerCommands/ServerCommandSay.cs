using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandSay : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <stringToSay> <OPTIONAL:nameOfReciever>

            // Create a new RPC
            RPCs.RPCSay newRPC = new RPCs.RPCSay();
            newRPC.arguments = givenArguments;
            // Send to the reciever if necessary
            if (givenArguments.Count == 2)
                server.SendRPC(newRPC, givenArguments[1]);
            else
                // Send the RPCSay to all clients
                server.SendRPC(newRPC);
        }
        public ServerCommandSay(string nameOfSender)
        {
            type = typeof(ServerCommandSay);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandSay()
        {
            type = typeof(ServerCommandSay);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Linq;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // This server command let's you hug people
    class ServerCommandHug : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <nameOfPersonToHug>

            // If dead
            if (sender.specialProperties["isDeceased"] == "TRUE")
            {
                RPCs.RPCSay failure = new RPCs.RPCSay();
                failure.arguments.Add("You are deceased.");
                server.SendRPC(failure, nameOfSender);
                return;
            }

            // Make sure the object is a player
            if (!server.world.players.ContainsKey(givenArguments[0]))
            {
                // Make a message back to the sender
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add(givenArguments[0] + " is not a person.");
                server.SendRPC(error, nameOfSender);

                return;
            }
            // Make a message back to the sender
            RPCs.RPCSay rpcBackToSender = new RPCs.RPCSay();
            rpcBackToSender.arguments.Add("You hugged " + givenArguments[0]);
            server.SendRPC(rpcBackToSender, nameOfSender);

            // Make a message back to the reciever
            RPCs.RPCSay rpcToReciever = new RPCs.RPCSay();
            rpcToReciever.arguments.Add(nameOfSender + " hugged you.");
            server.SendRPC(rpcToReciever, givenArguments[0]);

            // Make a message back to the sender
            RPCs.RPCSay rpcToEveryoneElse = new RPCs.RPCSay();
            rpcBackToSender.arguments.Add(nameOfSender + " hugged " + givenArguments[0]);
            server.SendRPC(rpcBackToSender, sender.position, new List<string>() { nameOfSender, givenArguments[0] } );
        }
        public ServerCommandHug(string nameOfSender)
        {
            type = typeof(ServerCommandHug);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandHug()
        {
            type = typeof(ServerCommandHug);
        }
    }
}

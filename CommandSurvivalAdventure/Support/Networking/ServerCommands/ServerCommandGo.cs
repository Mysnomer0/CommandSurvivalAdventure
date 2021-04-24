using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandGo : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <direction> 

            // Make sure the direction is correct
            if(World.Direction.StringToDirection(givenArguments[0]) == null)
            {
                RPCs.RPCSay newRPC = new RPCs.RPCSay();
                newRPC.arguments.Add("Invalid direction.  north | northeast | east...");
                server.SendRPC(newRPC, nameOfSender);
                return;
            }
            // Make sure the player is standing up
            if (sender.specialProperties["stance"] != "STANDING")
            {
                RPCs.RPCSay newRPC = new RPCs.RPCSay();
                newRPC.arguments.Add("You must be standing to go in a direction.");
                server.SendRPC(newRPC, nameOfSender);
                return;
            }
            // Get the direction that the player wants to go
            World.Direction desiredDirection = World.Direction.StringToDirection(givenArguments[0]);
            // Notify everyone in the current chunk that we left
            foreach (World.GameObject gameObject in server.world.GetChunkOrGenerate(sender.position).children)
            {
                if(gameObject.specialProperties.ContainsKey("isPlayer") && gameObject.identifier.name != sender.identifier.name)
                {
                    // Send a message to this player saying that we left the chunk
                    RPCs.RPCSay newRPC = new RPCs.RPCSay();
                    newRPC.arguments.Add(nameOfSender + " went " + givenArguments[0] + ".");
                    server.SendRPC(newRPC, gameObject.identifier.name);
                }
            }
            // Tell the player in the present tense that they are completing the action
            string presentTenseConfirmationString = "You are going " + givenArguments[0] + "...";
            RPCs.RPCSay presentTenseConfirmation = new RPCs.RPCSay();
            presentTenseConfirmation.arguments.Add(presentTenseConfirmationString);
            server.SendRPC(presentTenseConfirmation, nameOfSender);
            // Loop the elipsis three times and send it to the player, implying work being done
            for (int i = 0; i < 1; i++)
            {
                Thread.Sleep(1000);
                RPCs.RPCSay elipsis = new RPCs.RPCSay();
                elipsis.arguments.Add("...");
                server.SendRPC(elipsis, nameOfSender);
            }
            // Move the player in the desired direction
            server.world.MovePlayer(nameOfSender, new World.Position(sender.position.x + desiredDirection.x, sender.position.y + desiredDirection.y, sender.position.z + desiredDirection.z));
            // Notify everyone in the chunk we just arrived at that we have come
            foreach (World.GameObject gameObject in server.world.GetChunkOrGenerate(sender.position).children)
            {
                if (gameObject.specialProperties.ContainsKey("isPlayer") && gameObject.identifier.name != sender.identifier.name)
                {
                    // Send a message to this player saying that we left the chunk
                    RPCs.RPCSay newRPC = new RPCs.RPCSay();
                    newRPC.arguments.Add(nameOfSender + " came from the " + World.Direction.DirectionToString(World.Direction.GetOpposite(desiredDirection)) + ".");
                    server.SendRPC(newRPC, gameObject.identifier.name);
                }
            }
            // Send a look command

            // Describe the current chunk that the sender is on
            string description = Processing.Describer.Describe(server.world.GetChunkOrGenerate(sender.position), sender);
            // Send this new string back to the sender
            RPCs.RPCSay lookRPC = new RPCs.RPCSay();
            lookRPC.arguments.Add(description);
            server.SendRPC(lookRPC, nameOfSender);
        }
        public ServerCommandGo(string nameOfSender)
        {
            type = typeof(ServerCommandGo);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandGo()
        {
            type = typeof(ServerCommandGo);
        }
    }
}

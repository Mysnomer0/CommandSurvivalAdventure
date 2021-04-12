using System;
using System.Collections.Generic;
using System.Text;
using Humanizer;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandLook : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <typeOfObjectToLookFor>

            // If the person said "look at", get the following string and go find all objects with that string in it's name.
            if(givenArguments.Count == 2)
            {
                if(givenArguments[0] == "at")
                {
                    // Return a list of all objects on this chunk with givenArgument[1] keyword in it's full name
                    // Get the keyword the player was asking for
                    string keyword = givenArguments[1].Singularize();
                    if (keyword == null)
                        keyword = givenArguments[1];
                    string description = "You see:\n";
                    // Get all the children of this chunk
                    List<World.GameObject> children = sender.parent.GetAllChildren();
                    // Go through the children and find all the ones with givenArgument[1] in it's full name
                    foreach(World.GameObject child in children)
                    {
                        if(child.identifier.fullName.Contains(keyword))
                        {
                            description += Processing.Describer.GetArticle(child.identifier.fullName) + " " + child.identifier.fullName + "\n";
                        }
                    }
                    // Send this new string back to the sender
                    RPCs.RPCSay newRPC = new RPCs.RPCSay();
                    newRPC.arguments.Add(description);
                    server.SendRPC(newRPC, nameOfSender);
                }
            }
            // If the person gave no arguments, just describe the chunk they are in
            if(givenArguments.Count == 0)
            {
                // Describe the current chunk that the sender is on
                string description = Processing.Describer.Describe(server.world.chunks[sender.position.x][sender.position.y][sender.position.z], sender);
                // Send this new string back to the sender
                RPCs.RPCSay newRPC = new RPCs.RPCSay();
                newRPC.arguments.Add(description);
                server.SendRPC(newRPC, nameOfSender);
            }   
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

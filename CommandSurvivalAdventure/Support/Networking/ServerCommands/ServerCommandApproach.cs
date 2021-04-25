using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the sender to assume a standing stance
    class ServerCommandApproach : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <nameOfObjectToApproach>

            // If dead
            if(sender.specialProperties["isDeceased"] == "TRUE")
            {
                RPCs.RPCSay failure = new RPCs.RPCSay();
                failure.arguments.Add("You are deceased.");
                server.SendRPC(failure, nameOfSender);
                return;
            }
            // Find the gameObject we are going to approach

            // Check if name is a player's name
            World.GameObject gameObjectToApproach = null;
            foreach (World.GameObject gameObject in server.world.GetChunk(sender.position).children)
            {
                if (gameObject.specialProperties.ContainsKey("isPlayer") && gameObject.identifier.name != sender.identifier.name && gameObject.identifier.name == givenArguments[0])
                {
                    // Send a message to this player saying that we left the chunk
                    gameObjectToApproach = gameObject;
                }
            }
            // If not a player, find the first object with the name
            if(gameObjectToApproach == null)
            {
                List<World.GameObject> listOfPotentialObjects = server.world.GetChunkOrGenerate(sender.position).FindChildrenWithName(givenArguments[0]);
                if (listOfPotentialObjects.Count > 0)
                    gameObjectToApproach = listOfPotentialObjects.First();
            }
                
            // If no gameObject was found, send back an error
            if (gameObjectToApproach == null)
            {
                // If this fails, let the player know
                RPCs.RPCSay failure = new RPCs.RPCSay();
                failure.arguments.Add("There is no nearby " + givenArguments[0] + ".");
                server.SendRPC(failure, nameOfSender);
                return;
            }

            // Now that we have the game object we are going to approach, make sure it isn't already in our proximity
            if(sender.gameObjectsInProximity.Contains(gameObjectToApproach))
            {
                // If this fails, let the player know
                RPCs.RPCSay failure = new RPCs.RPCSay();
                failure.arguments.Add("You are already near the " + gameObjectToApproach.identifier.fullName + ".");
                server.SendRPC(failure, nameOfSender);
                return;
            }
            
            // The message we're going to send back           
            string intermediateResponse = "You walk up to the " + gameObjectToApproach.identifier.fullName + "...";
            RPCs.RPCSay intermediateRPC = new RPCs.RPCSay();
            intermediateRPC.arguments.Add(intermediateResponse);
            server.SendRPC(intermediateRPC, nameOfSender);

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Thread.Sleep(1000);

                // Add it to the list of object's in our proximity
                sender.gameObjectsInProximity.Add(gameObjectToApproach);
                gameObjectToApproach.gameObjectsInProximity.Add(sender);
                gameObjectToApproach.OnEnterProximity(sender);

                string response = "You approached the " + gameObjectToApproach.identifier.fullName + ".";
                // Send info back to the sender
                RPCs.RPCSay rPC = new RPCs.RPCSay();
                rPC.arguments.Add(response);
                server.SendRPC(rPC, nameOfSender);

                // Notify everyone in the chunk
                foreach (World.GameObject gameObject in server.world.GetChunkOrGenerate(sender.position).children)
                {
                    if (gameObject.specialProperties.ContainsKey("isPlayer") && gameObject.identifier.name != sender.identifier.name)
                    {
                        // Send a message to this player saying that we left the chunk
                        RPCs.RPCSay newRPC = new RPCs.RPCSay();
                        if (gameObjectToApproach.specialProperties.ContainsKey("isPlayer"))
                            newRPC.arguments.Add(nameOfSender + " approached " + gameObjectToApproach.identifier.fullName + ".");
                        else
                            newRPC.arguments.Add(nameOfSender + " approached " + Processing.Describer.GetArticle(gameObjectToApproach.identifier.fullName) + gameObjectToApproach.identifier.fullName + ".");
                        server.SendRPC(newRPC, gameObject.identifier.name);
                    }
                }
            }).Start();                     
        }
        public ServerCommandApproach(string nameOfSender)
        {
            type = typeof(ServerCommandApproach);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandApproach()
        {
            type = typeof(ServerCommandApproach);
        }
    }
}

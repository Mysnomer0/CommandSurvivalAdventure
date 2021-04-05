using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandExamine : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <nameOfObjectToExamine>

            // The gameObject that we will be examined
            World.GameObject gameObjectToExamine = server.world.FindFirstGameObject(givenArguments[0], sender.position);

            #region Find all the children of the ObjectToExamine and describe them

            string descriptionOfGameObject = "";

            // Make room for the children sorted by type
            Dictionary<Type, List<World.GameObject>> childrenSortedByType = new Dictionary<Type, List<World.GameObject>>();
            foreach (World.GameObject child in gameObjectToExamine.children)
            {
                // If the dictionary dose not have the child type then make a new type
                if (!childrenSortedByType.ContainsKey(child.type))
                    childrenSortedByType.Add(child.type, new List<World.GameObject>());
                childrenSortedByType[child.type].Add(child);
            }

            RPCs.RPCSay response = new RPCs.RPCSay();
            response.arguments.Add(descriptionOfGameObject);
            server.SendRPC(response, nameOfSender);
            // Let everyone else in the chunk know that this player dropped something
            RPCs.RPCSay information = new RPCs.RPCSay();
            information.arguments.Add(nameOfSender + " examined " + Processing.Describer.GetArticle(gameObjectToExamine.identifier.fullName) + " " + gameObjectToExamine.identifier.fullName + ".");
            // Find everyone in the same chunk and let them know
            foreach (KeyValuePair<string, Core.Player> playerEntry in server.world.players)
            {
                // If this player is not the one that picked something up, and it's position is the same as the original player's position
                if (playerEntry.Key != nameOfSender && playerEntry.Value.controlledGameObject.position == sender.position)
                    // Send an informational RPC to them letting them know
                    server.SendRPC(information, playerEntry.Key);
            }
            #endregion
        }
        public ServerCommandExamine(string nameOfSender)
        {
            type = typeof(ServerCommandExamine);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandExamine()
        {
            type = typeof(ServerCommandExamine);
        }
    }
}


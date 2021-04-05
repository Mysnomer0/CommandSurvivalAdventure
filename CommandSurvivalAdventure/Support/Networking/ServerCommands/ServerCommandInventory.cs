using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Displays the inventory of the sender, essentially what the sender is holding, wearing, and carrying
    class ServerCommandInventory : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: none

            // The summary of the inventory that we'll send back to the player
            string inventorySummary = "You are is possession of:\n";
            // Get the list of utilisable parts
            List<World.GameObject> list = sender.FindChildrenWithSpecialProperty("isUtilizable");

            // Loop through the game objects to sumarize them
            foreach (World.GameObject gameObject in list)
            {
                if (gameObject.children.Count != 0)
                {
                    string article = Processing.Describer.GetArticle(gameObject.children.First().identifier.fullName);
                    inventorySummary += "\t" + char.ToUpper(article[0]) + article.Substring(1) + " " + gameObject.children.First().identifier.fullName + " in your " + gameObject.identifier.fullName + ".\n";

                    // If the item is a container, show it's contents
                    if (gameObject.children.First().specialProperties.ContainsKey("isContainer"))
                    {
                        // Loop through the game objects to sumarize them
                        foreach (World.GameObject containerGameObjects in gameObject.children.First().children)
                        {
                            string nextArticle = Processing.Describer.GetArticle(containerGameObjects.identifier.fullName);
                            inventorySummary += "\t\t|- " + char.ToUpper(nextArticle[0]) + nextArticle.Substring(1) + " " + containerGameObjects.identifier.fullName + ".\n";                        
                        }
                    }
                }
            }

            // Check if any children are being described and change the output acordingly
            if (inventorySummary == "You are is possession of:\n")
            {
                inventorySummary = "You are not in possession of anything.";
            }

            // Send info back to the sender
            RPCs.RPCSay rPC = new RPCs.RPCSay();
            rPC.arguments.Add(inventorySummary);
            server.SendRPC(rPC, nameOfSender);
        }
        public ServerCommandInventory(string nameOfSender)
        {
            type = typeof(ServerCommandInventory);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandInventory()
        {
            type = typeof(ServerCommandInventory);
        }
    }
}

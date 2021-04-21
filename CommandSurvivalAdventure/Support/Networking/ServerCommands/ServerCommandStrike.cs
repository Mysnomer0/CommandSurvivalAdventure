using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Linq;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandStrike : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <nameOfObjectToStrike> <nameOfObjectToUse> <(l)eft|(r)ight|(h)ead>
        
            // The object to strike
            World.GameObject objectToStrike = server.world.FindFirstGameObject(givenArguments[0], sender.position);
            // The object to use
            World.GameObject objectToUse = null; 

            #region Get the object to use
            // Loop through all the utilizable objects on the sender
            foreach(World.GameObject potentialObjectToUse in sender.FindChildrenWithSpecialProperty("isUtilizable"))
            {
                // If this potential object or what it's holding matches, use it
                if(potentialObjectToUse.identifier.DoesStringPartiallyMatchFullName(givenArguments[1]))
                {
                    objectToUse = potentialObjectToUse;
                    break;
                }
                // Otherwise, if this potential object's first child, the thing it's holding, matches, use it
                else if(potentialObjectToUse.children.Count > 0)
                {
                    if (potentialObjectToUse.children.First().identifier.DoesStringPartiallyMatchFullName(givenArguments[1]))
                    {
                        objectToUse = potentialObjectToUse.children.First();
                        break;
                    }
                }
            }
            #endregion

            #region Make sure the arguments are valid
            // Make sure the object to strike exists
            if (objectToStrike == null)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("\"" + givenArguments[0] + "\" is not nearby.");
                server.SendRPC(error, nameOfSender);
                return;
            }
            // Make sure the object to use exists
            else if (objectToUse == null)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("\"" + givenArguments[1] + "\" is not something you are holding.");
                server.SendRPC(error, nameOfSender);
                return;
            }
            // Make sure the direction to strike is valid
            else if (givenArguments[2] != "left" 
                && givenArguments[2] != "right"
                && givenArguments[2] != "head"
                && givenArguments[2] != "l"
                && givenArguments[2] != "r"
                && givenArguments[2] != "h")
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("\"" + givenArguments[2] + "\" is not a direction to strike. Use \"left\", \"right\", or \"head\".");
                server.SendRPC(error, nameOfSender);
                return;
            }
            // Translate the short hand of the direction to block if one was used
            if (givenArguments[2] == "l")
                givenArguments[2] = "left";
            else if (givenArguments[2] == "r")
                givenArguments[2] = "right";
            else if (givenArguments[2] == "h")
                givenArguments[2] = "head";
            #endregion

            #region Check the stance
            if (sender.specialProperties["stance"] != "STANDING" && sender.specialProperties["stance"] != "CROUCHING")
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("You can't strike from the stance you're in.");
                server.SendRPC(error, nameOfSender);
                return;
            }
            #endregion

            #region Check the object is in proximity
            if (!sender.gameObjectsInProximity.Contains(objectToStrike))
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("You are not close enough to the " + objectToStrike.identifier.fullName + ". You must approach it first.");
                server.SendRPC(error, nameOfSender);
                return;
            }
            #endregion

            #region Calculate the time to wait
            // Calculate the time to wait, which is just the sender's reaction time plus the object's weight over our strength
            // reactionTime + (objectWeight/senderStrength)
            float timeToWait =
                float.Parse(sender.specialProperties["reactionTime"], CultureInfo.InvariantCulture.NumberFormat) +
                (float.Parse(objectToUse.specialProperties["weight"], CultureInfo.InvariantCulture.NumberFormat) /
                float.Parse(sender.specialProperties["strength"], CultureInfo.InvariantCulture.NumberFormat));
            #endregion

            #region Tell everyone that the object is about to be struck
            // Create unique messages for the sender, reciever, and everyone else
            RPCs.RPCSay rpcToSender = new RPCs.RPCSay();
            RPCs.RPCSay rpcToReciever = new RPCs.RPCSay();
            RPCs.RPCSay rpcToEveryoneElse = new RPCs.RPCSay();
            // Build the messages to each person
            rpcToReciever.arguments.Add(nameOfSender + " is about to $mastrike your " +
                objectToStrike.identifier.fullName +
                " from the $oa" + givenArguments[2] + " with " +
                Processing.Describer.GetArticle(objectToUse.identifier.fullName) + " " +
                objectToUse.identifier.fullName + " in $oa" + timeToWait.ToString() + " seconds!");

            // Build the message back to the sender
            // If the object to strike was attached to a player, change the messages accordingly
            if (objectToStrike.FindParentsWithSpecialProperty("isPlayer").Count > 0)
            {
                rpcToSender.arguments.Add("You swing at " + objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name + "'s " + objectToStrike.identifier.fullName + " from the " + givenArguments[2] + " with your " +
                objectToUse.identifier.fullName + "!");

                rpcToEveryoneElse.arguments.Add(nameOfSender + " swings at " +
                    objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name + "'s " +
                    objectToStrike.identifier.fullName + " with " +
                    Processing.Describer.GetArticle(objectToUse.identifier.fullName) + " " +
                    objectToUse.identifier.fullName + "!");
            }
            else
            {
                rpcToSender.arguments.Add("You swing at the " + objectToStrike.identifier.fullName + " from the " + givenArguments[2] + " with your " +
                objectToUse.identifier.fullName + "!");

                rpcToEveryoneElse.arguments.Add(nameOfSender + " swings at " +
                    Processing.Describer.GetArticle(objectToStrike.identifier.fullName) + " " +
                    objectToStrike.identifier.fullName + " with " +
                    Processing.Describer.GetArticle(objectToUse.identifier.fullName) + " " +
                    objectToUse.identifier.fullName + "!");
            }
            // Send the confirmation back to the sender
            server.SendRPC(rpcToSender, nameOfSender);
            // If the object to strike was attached to a player, send a message to the reciever and ignore the sender and reciever of the strike command
            if (objectToStrike.FindParentsWithSpecialProperty("isPlayer").Count > 0)
            {
                server.SendRPC(rpcToReciever, objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name);
                server.SendRPC(rpcToEveryoneElse, sender.position, new List<string>() {
                        objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name,
                        nameOfSender
                    });
            }
            // Otherwise, just send the message to everyone else saying we struck something
            else
                server.SendRPC(rpcToEveryoneElse, sender.position, new List<string>() { nameOfSender });
            #endregion

            #region Wait for the appropriate amount of time based off of the sender strength and the object to use
            // TODO: Output ellipses in increments

            // Send back to the player how long it's going to take
            RPCs.RPCSay info = new RPCs.RPCSay();
            info.arguments.Add("Striking in " + timeToWait.ToString() + " seconds.");
            server.SendRPC(info, nameOfSender);
            // Sleep this thread for the time to wait
            Thread.Sleep((int)(timeToWait * 1000.0f));

            #endregion
          
            #region Check for blocking
            // Get the object that's being used to block, if any
            World.GameObject objectBeingUsedToBlock = null;
            // If we're hitting something that is a player
            if(objectToStrike.FindParentsWithSpecialProperty("isPlayer").Count > 0)
            {
                if (objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().FindChildrenWithSpecialPropertyAndValue("blocking", givenArguments[2]).Count > 0)
                {
                    // Set the object being used to block
                    objectBeingUsedToBlock = objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().FindChildrenWithSpecialPropertyAndValue("blocking", givenArguments[2]).First();
                    // Create unique messages for the sender, reciever, and everyone else
                    RPCs.RPCSay rpcToSenderForBlocking = new RPCs.RPCSay();
                    RPCs.RPCSay rpcToRecieverForBlocking = new RPCs.RPCSay();
                    RPCs.RPCSay rpcToEveryoneElseForBlocking = new RPCs.RPCSay();

                    // Build the messages to each person

                    // Build the message for the person recieving the block
                    rpcToSenderForBlocking.arguments.Add(objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.fullName + " blocked your " +
                        objectToUse.identifier.fullName + " with " +
                        Processing.Describer.GetArticle(objectBeingUsedToBlock.identifier.fullName) + " " +
                        objectBeingUsedToBlock.identifier.fullName + "!");

                    // Build the message for the sender and everyone else.  The if is to determine whether or not we hit another player.  Change the output a little if we hit another player
                    if (objectToStrike.FindParentsWithSpecialProperty("isPlayer").Count > 0)
                    {
                        rpcToRecieverForBlocking.arguments.Add("You blocked " +
                            nameOfSender + "'s " +
                            objectToUse.identifier.fullName + " with your " +
                            objectBeingUsedToBlock.identifier.fullName + "!");

                        rpcToEveryoneElseForBlocking.arguments.Add(objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name + " blocked " +
                            nameOfSender + "'s " +
                            objectToUse.identifier.fullName + " with " +
                            Processing.Describer.GetArticle(objectToStrike.identifier.fullName) + " " +
                            objectToStrike.identifier.fullName + "!");
                    }
                    else
                    {
                        rpcToEveryoneElseForBlocking.arguments.Add(objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name + " blocked the " +
                            nameOfSender + "'s " +
                            objectToUse.identifier.fullName + " with " +
                            Processing.Describer.GetArticle(objectToStrike.identifier.fullName) + " " +
                            objectToStrike.identifier.fullName + "!");
                    }
                    // Send the confirmation back to the sender
                    server.SendRPC(rpcToSenderForBlocking, nameOfSender);
                    // If the object to strike was attached to a player, send a message to the reciever and ignore the sender and reciever of the strike command
                    if (objectToStrike.FindParentsWithSpecialProperty("isPlayer").Count > 0)
                    {
                        server.SendRPC(rpcToRecieverForBlocking, objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name);
                        server.SendRPC(rpcToEveryoneElseForBlocking, sender.position, new List<string>() {
                        objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name,
                        nameOfSender
                    });
                    }
                    // Otherwise, just send the message to everyone else saying we struck something
                    else
                        server.SendRPC(rpcToEveryoneElseForBlocking, sender.position, new List<string>() { nameOfSender });
                    // Stop the function here
                    return;
                }
            }
            #endregion

            #region Apply damage to the object
            // Get the object that will actually do damage, which could be the head of an axe, or just the stick or hand
            World.GameObject objectToDoDamage = objectToUse;
            // If the object to use has children, use the bottommost child
            if (objectToUse.children.Count > 0)
                objectToDoDamage = objectToUse.GetAllChildren().Last();
            // Calculate the damage
            float damage = float.Parse(objectToDoDamage.specialProperties["weight"], CultureInfo.InvariantCulture.NumberFormat);

            #region Calculate damage multipliers
            // If the weapon is long, do a damage multiplier
            if (objectToUse.identifier.descriptiveAdjectives.Contains("long"))
            {
                damage *= 2;
            }
            if (objectToDoDamage.identifier.descriptiveAdjectives.Contains("sharp"))
            {
                damage *= 3;
            }
            #endregion

            #region Apply damage to objects
            // If the object to strike has a health property
            if (objectToStrike.specialProperties.ContainsKey("health"))
            {
                // Deduct the health from the object that is being struck
                objectToStrike.specialProperties["health"] = (
                    (float.Parse(objectToStrike.specialProperties["health"], CultureInfo.InvariantCulture.NumberFormat) - damage) < 0.0f ? 0.0f :
                    float.Parse(objectToStrike.specialProperties["health"], CultureInfo.InvariantCulture.NumberFormat) - damage
                    ).ToString();
            }
            #endregion

            #region Physically change the object being struck, such as knock the player over, etc

            #region If it's a player, knock them over if necessary
            // If we're hitting a player
            if(objectToStrike.FindParentsWithSpecialProperty("isPlayer").Count > 0 && objectToStrike.identifier.name == "leg" 
                || objectToStrike.FindParentsWithSpecialProperty("isPlayer").Count > 0 && objectToStrike.identifier.name == "head")
            {
                if(objectToStrike.FindParentsWithSpecialProperty("isPlayer").Last().specialProperties["stance"] != "LAYING")
                {
                    #region Confirm to everyone that the player has been knocked over
                    // Create unique messages for the sender, reciever, and everyone else
                    RPCs.RPCSay rpcToSenderForBeingKnocked = new RPCs.RPCSay();
                    RPCs.RPCSay rpcToRecieverForBeingKnocked = new RPCs.RPCSay();
                    RPCs.RPCSay rpcToEveryoneElseForBeingKnocked = new RPCs.RPCSay();

                    rpcToSenderForBeingKnocked.arguments.Add("You knocked " + objectToStrike.FindParentsWithSpecialProperty("isPlayer").Last().identifier.fullName + " over with your " + objectToUse.identifier.fullName + " from a strike to the " + objectToStrike.identifier.fullName + "!");
                    rpcToRecieverForBeingKnocked.arguments.Add(nameOfSender + " knocked you over with " + Processing.Describer.GetArticle(objectToUse.identifier.fullName) + " " + objectToUse.identifier.fullName + " from a strike to the " + objectToStrike.identifier.fullName + "!");
                    rpcToEveryoneElseForBeingKnocked.arguments.Add(nameOfSender + " knocked " + objectToStrike.FindParentsWithSpecialProperty("isPlayer").Last().identifier.fullName + " over with " + Processing.Describer.GetArticle(objectToUse.identifier.fullName) + " " + objectToUse.identifier.fullName + " from a strike to the " + objectToStrike.identifier.fullName + "!");

                    server.SendRPC(rpcToSenderForBeingKnocked, nameOfSender);
                    server.SendRPC(rpcToRecieverForBeingKnocked, objectToStrike.FindParentsWithSpecialProperty("isPlayer").Last().identifier.name);
                    server.SendRPC(rpcToEveryoneElseForBeingKnocked, sender.position, new List<string>() { nameOfSender, objectToStrike.FindParentsWithSpecialProperty("isPlayer").Last().identifier.name });
                    #endregion

                    #region Make the person who has been knocked fall over
                    RPCs.RPCSay rpcToRecieverForFalling = new RPCs.RPCSay();
                    rpcToRecieverForFalling.arguments.Add("You are falling...");
                    server.SendRPC(rpcToRecieverForFalling, objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name);
                    // Put them in the falling stance
                    objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().specialProperties["stance"] = "FALLING";
                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        Thread.Sleep(2000);

                        RPCs.RPCSay rpcToRecieverForLaying = new RPCs.RPCSay();
                        rpcToRecieverForLaying.arguments.Add("You have fallen.");
                        server.SendRPC(rpcToRecieverForLaying, objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name);
                        // Put them in the lying stance
                        objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().specialProperties["stance"] = "LAYING";
                    }).Start();
                
                    #endregion
                }
                
            }
            #endregion

            #endregion

            #endregion

            #region Confirm to everyone that the object was struck
            // Create unique messages for the sender, reciever, and everyone else
            RPCs.RPCSay rpcToSenderForBeingStruck = new RPCs.RPCSay();
            RPCs.RPCSay rpcToRecieverForBeingStruck = new RPCs.RPCSay();
            RPCs.RPCSay rpcToEveryoneElseForBeingStruck = new RPCs.RPCSay();
            // Build the messages to each person
            rpcToRecieverForBeingStruck.arguments.Add(Processing.Describer.ToColor(nameOfSender + " struck your " +
                objectToStrike.identifier.fullName + " with " +
                Processing.Describer.GetArticle(objectToUse.identifier.fullName) + " " +
                objectToUse.identifier.fullName + "!", "$ma"));

            // Build the message back to the sender
            // If the object to strike was attached to a player, change the messages accordingly
            if (objectToStrike.FindParentsWithSpecialProperty("isPlayer").Count > 0)
            {
                rpcToSenderForBeingStruck.arguments.Add(Processing.Describer.ToColor("You struck " + objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name + "'s " + objectToStrike.identifier.fullName + " with your " +
                objectToUse.identifier.fullName + "!\nYou did " + damage.ToString() + " damage!", "$ka"));

                rpcToEveryoneElseForBeingStruck.arguments.Add(nameOfSender + " struck " + 
                    objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name + "'s " +
                    objectToStrike.identifier.fullName + " with " +
                    Processing.Describer.GetArticle(objectToUse.identifier.fullName) + " " +
                    objectToUse.identifier.fullName + "!");
            }
            else
            {
                rpcToSenderForBeingStruck.arguments.Add("You struck the " + objectToStrike.identifier.fullName + " with your " +
                objectToUse.identifier.fullName + "!\nYou did " + damage.ToString() + " damage!");

                rpcToEveryoneElseForBeingStruck.arguments.Add(nameOfSender + " struck " +
                    Processing.Describer.GetArticle(objectToStrike.identifier.fullName) + " " +
                    objectToStrike.identifier.fullName + " with a " +
                    Processing.Describer.GetArticle(objectToUse.identifier.fullName) + " " +
                    objectToUse.identifier.fullName + "!");
            }     
            // Send the confirmation back to the sender
            server.SendRPC(rpcToSenderForBeingStruck, nameOfSender);
            // If the object to strike was attached to a player, send a message to the reciever and ignore the sender and reciever of the strike command
            if (objectToStrike.FindParentsWithSpecialProperty("isPlayer").Count > 0)
            {
                server.SendRPC(rpcToRecieverForBeingStruck, objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name);
                server.SendRPC(rpcToEveryoneElseForBeingStruck, sender.position, new List<string>() {
                        objectToStrike.FindParentsWithSpecialProperty("isPlayer").First().identifier.name,
                        nameOfSender
                    });
            }
            // Otherwise, just send the message to everyone else saying we struck something
            else
                server.SendRPC(rpcToEveryoneElseForBeingStruck, sender.position, new List<string>() { nameOfSender });
            #endregion
        }
        public ServerCommandStrike(string nameOfSender)
        {
            type = typeof(ServerCommandStrike);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandStrike()
        {
            type = typeof(ServerCommandStrike);
        }
    }
}

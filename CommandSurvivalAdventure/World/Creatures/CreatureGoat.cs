using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Creatures
{
    class CreatureGoat : Creature
    {
        // Initialize
        public override void Start()
        {
            base.Start();           
        }
        // Update
        public override void Update()
        {
            base.Update();

            // Decide whether to find food
            if (random.Next(1, 20) == 1)
            {
                // Look if there is any nearby food
                bool foundFood = false;
                string nameOfFood = "grass";
                // Get all the children of this chunk
                List<GameObject> children = parent.GetAllChildren();
                // Go through the children and find all the ones with grass in it's full name
                foreach (GameObject child in children)
                {
                    if (child.identifier.fullName.Contains(nameOfFood))
                    {
                        foundFood = true;
                        break;
                    }
                }

                // If so, graze
                if (foundFood)
                {
                    // Create a message entailing our action and send it to nearby players
                    Support.Networking.RPCs.RPCSay actionMessage = new Support.Networking.RPCs.RPCSay();
                    actionMessage.arguments.Add(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " grazes on the grass...");

                    // Get the nearby players to notify them our action
                    foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                    {
                        // If this player is in our chunk
                        if (playerEntry.Value.controlledGameObject.position == position)
                            // Send an informational RPC to them letting them know
                            attachedApplication.server.SendRPC(actionMessage, playerEntry.Key);
                    }
                }
                //*
                // If not, go to a random nearby chunk to find food
                else if(attachedApplication.server.world.players.Count > 0)
                {
                    // Decide a direction to go
                    Direction desiredDirection = Direction.IntToDirection(random.Next(0, 7));
                    Position newPosition = new Position(position.x + desiredDirection.x, position.y + desiredDirection.y, position.z + desiredDirection.z);
                    // Choose a random direction until we get one where there is a valid chunk at our destination
                    while(attachedApplication.server.world.GetChunk(newPosition) == null)
                    {
                        desiredDirection = Direction.IntToDirection(random.Next(0, 7));
                        newPosition = new Position(position.x + desiredDirection.x, position.y + desiredDirection.y, position.z + desiredDirection.z);
                    }
                          
                    // Create a message entailing our action and send it to nearby players
                    Support.Networking.RPCs.RPCSay leavingMessage = new Support.Networking.RPCs.RPCSay();
                    leavingMessage.arguments.Add(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " trots " + Direction.DirectionToString(desiredDirection) + " in search of food...");
                    Support.Networking.RPCs.RPCSay arrivingMessage = new Support.Networking.RPCs.RPCSay();
                    arrivingMessage.arguments.Add(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " trots along from the " + Direction.DirectionToString(Direction.GetOpposite(desiredDirection)) + " in search of food...");
                    // Get the nearby players to notify them our action
                    foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                    {
                        // If this player is in our chunk
                        if (playerEntry.Value.controlledGameObject.position == position)
                            // Send an informational RPC to them letting them know
                            attachedApplication.server.SendRPC(leavingMessage, playerEntry.Key);
                        // If this player is in the destination chunk
                        if (playerEntry.Value.controlledGameObject.position == newPosition)
                            // Send an informational RPC to them letting them know
                            attachedApplication.server.SendRPC(arrivingMessage, playerEntry.Key);
                    }
                    attachedApplication.server.world.MoveObject(ID, newPosition);                                                                
                                   
                }//*/
            }
            // Goat bleating
            if (random.Next(1, 60) == 1)
            {
                // Create a message entailing our action and send it to nearby players
                Support.Networking.RPCs.RPCSay actionMessage = new Support.Networking.RPCs.RPCSay();
                actionMessage.arguments.Add(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " bleats softly...");

                // Get the nearby players to notify them our action
                foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                {
                    // If this player is in our chunk
                    if (playerEntry.Value.controlledGameObject.position == position)
                        // Send an informational RPC to them letting them know
                        attachedApplication.server.SendRPC(actionMessage, playerEntry.Key);
                }
            }
            // Goat looks at random nearby player
            else if (random.Next(1, 60) == 1)
            {
                // List of nearby players
                List<Core.Player> nearbyPlayers = new List<Core.Player>();
                // Get the nearby players
                foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                {
                    // If this player is in our chunk
                    if (playerEntry.Value.controlledGameObject.position == position)
                    {
                        nearbyPlayers.Add(playerEntry.Value);
                    }                       
                }
                // If someone is nearby, look at them
                if(nearbyPlayers.Count > 0)
                {
                    // Choose a random person nearby to look at
                    Core.Player playerToLookAt = nearbyPlayers.ToArray()[random.Next(0, nearbyPlayers.Count)];
                    // Create a message entailing our action and send it to nearby players
                    Support.Networking.RPCs.RPCSay lookAtYouMessage = new Support.Networking.RPCs.RPCSay();
                    lookAtYouMessage.arguments.Add(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " looks at you cautiously...");
                    Support.Networking.RPCs.RPCSay lookAtPlayerMessage = new Support.Networking.RPCs.RPCSay();
                    lookAtPlayerMessage.arguments.Add(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " looks at " + playerToLookAt.name + " cautiously...");

                    // Get the nearby players to notify them our action
                    foreach (Core.Player player in nearbyPlayers)
                    {
                        // If this player is in our chunk
                        if (player == playerToLookAt)
                            // Send an informational RPC to them letting them know
                            attachedApplication.server.SendRPC(lookAtYouMessage, player.name);
                        else
                            // Send an informational RPC to them letting them know
                            attachedApplication.server.SendRPC(lookAtPlayerMessage, player.name);
                    }
                }              
            }
            // Goat panting from the heat
            else if (random.Next(1, 60) == 1)
            {
                if(attachedApplication.server.world.GetChunk(position).temperature > 80)
                {
                    // Create a message entailing our action and send it to nearby players
                    Support.Networking.RPCs.RPCSay actionMessage = new Support.Networking.RPCs.RPCSay();
                    actionMessage.arguments.Add(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " pants from the heat...");

                    // Get the nearby players to notify them our action
                    foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                    {
                        // If this player is in our chunk
                        if (playerEntry.Value.controlledGameObject.position == position)
                            // Send an informational RPC to them letting them know
                            attachedApplication.server.SendRPC(actionMessage, playerEntry.Key);
                    }
                }                
            }
            else
            {
                /*
                // Create a message entailing our action and send it to nearby players
                Support.Networking.RPCs.RPCSay actionMessage = new Support.Networking.RPCs.RPCSay();
                actionMessage.arguments.Add("Goat tick!");

                // Get the nearby players to notify them our action
                foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                {
                    // If this player is in our chunk
                    if (playerEntry.Value.controlledGameObject.position == position)
                        // Send an informational RPC to them letting them know
                        attachedApplication.server.SendRPC(actionMessage, playerEntry.Key);
                }//*/
            }
        }
        public CreatureGoat(Application newApplication)
        {
            attachedApplication = newApplication;
            // Set the type
            type = typeof(CreatureGoat);
            identifier.name = "goat";
            random = new Random();
            // Generate the stats for the minotaur
            specialProperties.Add("stance", "STANDING");
            specialProperties.Add("blocking", "NULL");
            specialProperties.Add("isDeceased", "NULL");
            specialProperties.Add("isStunned", "NULL");
            specialProperties.Add("strength", random.Next(30, 50).ToString());
            specialProperties.Add("weight", random.Next(44, 310).ToString());
            specialProperties.Add("reactionTime", (random.NextDouble() * 2.0f + 3f).ToString());
            specialProperties.Add("speed", (random.NextDouble() * 2.0f).ToString());
            // Generate the body parts of the goat
            #region Front Right Leg
            CreatureParts.CreaturePartGoat.CreaturePartGoatLeg frontRightLeg = new CreatureParts.CreaturePartGoat.CreaturePartGoatLeg();
            frontRightLeg.identifier.name = "leg";
            frontRightLeg.identifier.classifierAdjectives.Add("front");
            frontRightLeg.identifier.classifierAdjectives.Add("right");
            frontRightLeg.identifier.classifierAdjectives.Add("goat");
            frontRightLeg.weight = (10 * (random.Next(9, 11) / 10));
            frontRightLeg.health = frontRightLeg.weight;
            frontRightLeg.muscleContent = frontRightLeg.weight * (random.Next(8, 12) / 10);
            frontRightLeg.fatContent = frontRightLeg.weight * (random.Next(8, 12) / 10);
            frontRightLeg.isBleeding = false;
            frontRightLeg.isCooked = false;
            frontRightLeg.isUnclean = false;
            #endregion

            #region Front Left Leg
            CreatureParts.CreaturePartGoat.CreaturePartGoatLeg frontLeftLeg = new CreatureParts.CreaturePartGoat.CreaturePartGoatLeg();
            frontLeftLeg.identifier.name = "leg";
            frontLeftLeg.identifier.classifierAdjectives.Add("front");
            frontLeftLeg.identifier.classifierAdjectives.Add("left");
            frontLeftLeg.identifier.classifierAdjectives.Add("goat");
            frontLeftLeg.weight = (10 * (random.Next(9, 11) / 10));
            frontLeftLeg.health = frontLeftLeg.weight;
            frontLeftLeg.muscleContent = frontLeftLeg.weight * (random.Next(8, 12) / 10);
            frontLeftLeg.fatContent = frontLeftLeg.weight * (random.Next(8, 12) / 10);
            frontLeftLeg.isBleeding = false;
            frontLeftLeg.isCooked = false;
            frontLeftLeg.isUnclean = false;
            #endregion

            #region Rear Left Leg
            CreatureParts.CreaturePartGoat.CreaturePartGoatLeg rearLeftLeg = new CreatureParts.CreaturePartGoat.CreaturePartGoatLeg();
            rearLeftLeg.identifier.name = "leg";
            rearLeftLeg.identifier.classifierAdjectives.Add("rear");
            rearLeftLeg.identifier.classifierAdjectives.Add("left");
            rearLeftLeg.identifier.classifierAdjectives.Add("goat");
            rearLeftLeg.weight = (15 * (random.Next(9, 11) / 10));
            rearLeftLeg.health = rearLeftLeg.weight;
            rearLeftLeg.muscleContent = rearLeftLeg.weight * (random.Next(8, 12) / 10);
            rearLeftLeg.fatContent = rearLeftLeg.weight * (random.Next(8, 12) / 10);
            rearLeftLeg.isBleeding = false;
            rearLeftLeg.isCooked = false;
            rearLeftLeg.isUnclean = false;
            #endregion

            #region Rear Right Leg
            CreatureParts.CreaturePartGoat.CreaturePartGoatLeg rearRightLeg = new CreatureParts.CreaturePartGoat.CreaturePartGoatLeg();
            rearRightLeg.identifier.name = "leg";
            rearRightLeg.identifier.classifierAdjectives.Add("rear");
            rearRightLeg.identifier.classifierAdjectives.Add("right");
            rearRightLeg.identifier.classifierAdjectives.Add("goat");
            rearRightLeg.weight = (15 * (random.Next(9, 11) / 10));
            rearRightLeg.health = rearRightLeg.weight;
            rearRightLeg.muscleContent = rearRightLeg.weight * (random.Next(8, 12) / 10);
            rearRightLeg.fatContent = rearRightLeg.weight * (random.Next(8, 12) / 10);
            rearRightLeg.isBleeding = false;
            rearRightLeg.isCooked = false;
            rearRightLeg.isUnclean = false;
            #endregion

            #region Head
            CreatureParts.CreaturePartGoat.CreaturePartGoatHead head = new CreatureParts.CreaturePartGoat.CreaturePartGoatHead();
            head.identifier.name = "head";
            head.weight = (8 * (random.Next(9, 11) / 10));
            head.health = head.weight;
            head.muscleContent = head.weight * (random.Next(8, 12) / 10);
            head.fatContent = head.weight * (random.Next(8, 12) / 10);
            head.isBleeding = false;
            head.isCooked = false;
            head.isUnclean = false;
            #endregion

            #region Body
            CreatureParts.CreaturePartGoat.CreaturePartGoatBody body = new CreatureParts.CreaturePartGoat.CreaturePartGoatBody();
            body.identifier.name = "body";
            body.identifier.classifierAdjectives.Add("goat");
            body.weight = (30 * (random.Next(9, 11) / 10));
            body.health = body.weight;
            body.muscleContent = body.weight * (random.Next(8, 12) / 10);
            body.fatContent = body.weight * (random.Next(8, 12) / 10);
            body.isBleeding = false;
            body.isCooked = false;
            body.isUnclean = false;
            #endregion
            // Add all the creature parts
            AddChild(frontRightLeg);
            AddChild(frontLeftLeg);
            AddChild(rearLeftLeg);
            AddChild(rearRightLeg);
            AddChild(head);
            AddChild(body);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace CommandSurvivalAdventure.World.Creatures
{
    class CreatureGoat : Creature
    {
        // The AI state of the goat, from which the AI decides actions
        public AIStates AIState = AIStates.HUNGRY;
        // The sleeping timer
        public int sleepingTimer = 0;
        // Whether or not the goat is in the middle of an action
        public bool currentlyInTheMiddleOfPerformingAction = false;
        // The list of game objects percieved to be a threat
        public HashSet<GameObject> gameObjectsPercievedAsThreat = new HashSet<GameObject>();
        // The gameObject that is known to be hostile
        // TODO: Build this as a list soon so that creatures can handle fighting multiple enemies, and flee if too many enemies
        public GameObject gameObjectKnownToBeHostile;
        // The head of the goat
        public CreatureParts.CreaturePartGoat.CreaturePartGoatHead head;
        // The body of the goat
        public CreatureParts.CreaturePartGoat.CreaturePartGoatBody body;

        public CreatureGoat(Application newApplication)
        {
            attachedApplication = newApplication;
            // Set the type
            type = typeof(CreatureGoat);
            identifier.name = "goat";
            random = new Random();


            // Generate the stats            
            speed = 4f + (float)random.NextDouble() * 2f;
            specialProperties.Add("stance", "STANDING");
            /*
            specialProperties.Add("blocking", "NULL");
            specialProperties.Add("isDeceased", "NULL");
            specialProperties.Add("isStunned", "NULL");
            specialProperties.Add("strength", random.Next(30, 50).ToString());
            specialProperties.Add("weight", random.Next(44, 310).ToString());
            specialProperties.Add("reactionTime", (random.NextDouble() * 2.0f + 3f).ToString());
            specialProperties.Add("speed", (random.NextDouble() * 2.0f).ToString());
            specialProperties.Add("health", (weight + strength).ToString());
            */
            // Generate the body parts of the goat
            // TODO: Go through and change all the health, isBleeding, etc attributes to special properties
            #region Front Right Leg
            CreatureParts.CreaturePartGoat.CreaturePartGoatLeg frontRightLeg = new CreatureParts.CreaturePartGoat.CreaturePartGoatLeg();
            frontRightLeg.identifier.name = "leg";
            frontRightLeg.identifier.classifierAdjectives.Add("front");
            frontRightLeg.identifier.classifierAdjectives.Add("right");
            frontRightLeg.identifier.classifierAdjectives.Add("goat");
            frontRightLeg.specialProperties["weight"] = (25 + random.Next(-5, 5)).ToString();
            frontRightLeg.specialProperties["health"] = frontRightLeg.specialProperties["weight"];
            frontRightLeg.specialProperties["isBleeding"] = "FALSE";
            frontRightLeg.specialProperties["isCooked"] = "FALSE";
            frontRightLeg.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Front Left Leg
            CreatureParts.CreaturePartGoat.CreaturePartGoatLeg frontLeftLeg = new CreatureParts.CreaturePartGoat.CreaturePartGoatLeg();
            frontLeftLeg.identifier.name = "leg";
            frontLeftLeg.identifier.classifierAdjectives.Add("front");
            frontLeftLeg.identifier.classifierAdjectives.Add("left");
            frontLeftLeg.identifier.classifierAdjectives.Add("goat");
            frontLeftLeg.specialProperties["weight"] = (25 + random.Next(-5, 5)).ToString();
            frontLeftLeg.specialProperties["health"] = frontLeftLeg.specialProperties["weight"];
            frontLeftLeg.specialProperties["isBleeding"] = "FALSE";
            frontLeftLeg.specialProperties["isCooked"] = "FALSE";
            frontLeftLeg.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Rear Left Leg
            CreatureParts.CreaturePartGoat.CreaturePartGoatLeg rearLeftLeg = new CreatureParts.CreaturePartGoat.CreaturePartGoatLeg();
            rearLeftLeg.identifier.name = "leg";
            rearLeftLeg.identifier.classifierAdjectives.Add("rear");
            rearLeftLeg.identifier.classifierAdjectives.Add("left");
            rearLeftLeg.identifier.classifierAdjectives.Add("goat");
            rearLeftLeg.specialProperties["weight"] = (30 + random.Next(-5, 5)).ToString();
            rearLeftLeg.specialProperties["health"] = rearLeftLeg.specialProperties["weight"];
            rearLeftLeg.specialProperties["isBleeding"] = "FALSE";
            rearLeftLeg.specialProperties["isCooked"] = "FALSE";
            rearLeftLeg.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Rear Right Leg
            CreatureParts.CreaturePartGoat.CreaturePartGoatLeg rearRightLeg = new CreatureParts.CreaturePartGoat.CreaturePartGoatLeg();
            rearRightLeg.identifier.name = "leg";
            rearRightLeg.identifier.classifierAdjectives.Add("rear");
            rearRightLeg.identifier.classifierAdjectives.Add("right");
            rearRightLeg.identifier.classifierAdjectives.Add("goat");
            rearRightLeg.specialProperties["weight"] = (30 + random.Next(-5, 5)).ToString();
            rearRightLeg.specialProperties["health"] = rearRightLeg.specialProperties["weight"];
            rearRightLeg.specialProperties["isBleeding"] = "FALSE";
            rearRightLeg.specialProperties["isCooked"] = "FALSE";
            rearRightLeg.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Head
            head = new CreatureParts.CreaturePartGoat.CreaturePartGoatHead();
            head.identifier.name = "head";
            head.identifier.classifierAdjectives.Add("goat");
            head.specialProperties["weight"] = (8 + random.Next(-2, 2)).ToString();
            head.specialProperties["health"] = head.specialProperties["weight"];
            head.specialProperties["isBleeding"] = "FALSE";
            head.specialProperties["isCooked"] = "FALSE";
            head.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Body
            body = new CreatureParts.CreaturePartGoat.CreaturePartGoatBody();
            body.identifier.name = "body";
            body.identifier.classifierAdjectives.Add("goat");
            body.specialProperties["weight"] = (100 + random.Next(-20, 20)).ToString();
            body.specialProperties["health"] = body.specialProperties["weight"];
            body.specialProperties["isBleeding"] = "FALSE";
            body.specialProperties["isCooked"] = "FALSE";
            body.specialProperties["isUnclean"] = "FALSE";
            #endregion
            // Add all the creature parts
            AddChild(frontRightLeg);
            AddChild(frontLeftLeg);
            AddChild(rearLeftLeg);
            AddChild(rearRightLeg);
            AddChild(head);
            AddChild(body);

            // Generate random female or male
            if (random.Next(1, 3) == 2)
            {
                //identifier.descriptiveAdjectives.Add("male");

                #region Horn1
                CreatureParts.CreaturePartGoat.CreaturePartGoatHorn horn1 = new CreatureParts.CreaturePartGoat.CreaturePartGoatHorn();
                horn1.identifier.name = "horn";
                horn1.identifier.classifierAdjectives.Add("goat");
                horn1.identifier.descriptiveAdjectives.Add("sharp");
                horn1.specialProperties["weight"] = (4 + random.Next(-2, 2)).ToString();
                horn1.specialProperties["health"] = horn1.specialProperties["weight"];
                horn1.specialProperties["isBleeding"] = "FALSE";
                horn1.specialProperties["isCooked"] = "FALSE";
                horn1.specialProperties["isUnclean"] = "FALSE";
                #endregion

                #region Horn2
                CreatureParts.CreaturePartGoat.CreaturePartGoatHorn horn2 = new CreatureParts.CreaturePartGoat.CreaturePartGoatHorn();
                horn2.identifier.name = "horn";
                horn2.identifier.classifierAdjectives.Add("goat");
                horn2.identifier.descriptiveAdjectives.Add("sharp");
                horn2.specialProperties["weight"] = (4 + random.Next(-2, 2)).ToString();
                horn2.specialProperties["health"] = horn2.specialProperties["weight"];
                horn2.specialProperties["isBleeding"] = "FALSE";
                horn2.specialProperties["isCooked"] = "FALSE";
                horn2.specialProperties["isUnclean"] = "FALSE";
                #endregion

                head.AddChild(horn1);
                head.AddChild(horn2);
            }
            else
            {
                //identifier.descriptiveAdjectives.Add("female");
            }            
        }

        // Initialize
        public override void Start()
        {
            base.Start();
        }

        // Update
        public override void Update()
        {
            base.Update();

            #region If dead, die
            if (isDeceased)
                return;
            // If out of health, die
            if (float.Parse(body.specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) <= 0f || float.Parse(head.specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) <= 0f)
            {
                isDeceased = true;
                // Create a message entailing our action and send it to nearby players
                Support.Networking.RPCs.RPCSay message = new Support.Networking.RPCs.RPCSay();
                message.arguments.Add("The " + identifier.fullName + " falls to the ground, dead!");
                identifier.descriptiveAdjectives.Add("dead");
                specialProperties["stance"] = StanceToString(Stances.LAYING);
                // Get the nearby players to notify them our action
                foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                {
                    // If this player is in our chunk
                    if (playerEntry.Value.controlledGameObject.position == position)
                        // Send an informational RPC to them letting them know
                        attachedApplication.server.SendRPC(message, playerEntry.Key);
                }
                return;
            }
            #endregion

            #region Look around for threats

            // Add players
            foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
            {
                // If this player is in our chunk
                if (playerEntry.Value.controlledGameObject.position == position)
                {
                    gameObjectsPercievedAsThreat.Add(playerEntry.Value.controlledGameObject);
                }
            }

            #endregion

            #region Run state based AI
            // Use a while true loop so we can break the code when needed
            while (true)
            {
                if (AIState == AIStates.HUNGRY)
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
                        // If content and has food, possibly sleep
                        if (random.Next(0, 120) == 0)
                        {
                            // Change the stance to laying down
                            specialProperties["stance"] = StanceToString(Stances.LAYING);
                            // Create a message entailing our action and send it to nearby players
                            Support.Networking.RPCs.RPCSay sleepMessage = new Support.Networking.RPCs.RPCSay();
                            sleepMessage.arguments.Add(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " lays down to sleep.");

                            // Get the nearby players to notify them our action
                            foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                            {
                                // If this player is in our chunk
                                if (playerEntry.Value.controlledGameObject.position == position)
                                    // Send an informational RPC to them letting them know
                                    attachedApplication.server.SendRPC(sleepMessage, playerEntry.Key);
                            }
                            // Go into the sleeping state
                            AIState = AIStates.SLEEPING;
                            // Set the sleeping timer
                            sleepingTimer = random.Next(60, 120);
                            // Set the adjective
                            identifier.descriptiveAdjectives.Add("sleeping");
                            break;
                        }
                        // Every so often graze on the grass
                        else if (random.Next(0, 60) == 0)
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
                        // Goat bleating
                        else if (random.Next(1, 60) == 1)
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
                            if (nearbyPlayers.Count > 0)
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
                            if (attachedApplication.server.world.GetChunk(position).temperature > 80)
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
                    }
                    // If not, go to a random nearby chunk to find food
                    else if (attachedApplication.server.world.players.Count > 0)
                    {
                        // Decide a direction to go
                        Direction desiredDirection = Direction.IntToDirection(random.Next(0, 7));
                        Position newPosition = new Position(position.x + desiredDirection.x, position.y + desiredDirection.y, position.z + desiredDirection.z);
                        // Choose a random direction until we get one where there is a valid chunk at our destination
                        while (attachedApplication.server.world.GetChunk(newPosition) == null)
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
                    }
                }
                else if (AIState == AIStates.SLEEPING)
                {
                    // Keep the sleeping timer going
                    sleepingTimer--;
                    // If awake
                    if (sleepingTimer <= 0)
                    {
                        // Stand back up
                        specialProperties["stance"] = StanceToString(Stances.STANDING);
                        // Get rid of the sleeping adjective
                        identifier.descriptiveAdjectives.Remove("sleeping");
                        // Go back to being hungry
                        AIState = AIStates.HUNGRY;
                        // Create a message entailing our action and send it to nearby players
                        Support.Networking.RPCs.RPCSay actionMessage = new Support.Networking.RPCs.RPCSay();
                        actionMessage.arguments.Add(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " wakes from it's slumber and stands up.");

                        // Get the nearby players to notify them our action
                        foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                        {
                            // If this player is in our chunk
                            if (playerEntry.Value.controlledGameObject.position == position)
                                // Send an informational RPC to them letting them know
                                attachedApplication.server.SendRPC(actionMessage, playerEntry.Key);
                        }
                        break;
                    }
                }
                else if (AIState == AIStates.ATTACKING)
                {
                    // TODO: Add a decision tree where the goat decides whether or not to attack based on it's own health, whether or not it has horns, and if it weighs enough to stand a chance to fight

                    // If we're not currently in the middle of another action, attack!
                    if(!currentlyInTheMiddleOfPerformingAction)
                    {
                        // Set the bool that we are currently in the middle of an action
                        currentlyInTheMiddleOfPerformingAction = true;

                        #region Notify everyone in the chunk
                        // Create a message entailing our action and send it to nearby players
                        Support.Networking.RPCs.RPCSay messageToEveryoneInChunk = new Support.Networking.RPCs.RPCSay();

                        // If we are fighting a player, send a message to the player being attacked
                        if (gameObjectKnownToBeHostile.specialProperties.ContainsKey("isPlayer"))
                        {
                            Support.Networking.RPCs.RPCSay messageForAttacking = new Support.Networking.RPCs.RPCSay();
                            messageForAttacking.arguments.Add("The " + identifier.fullName + " charges at you! It will hit you in " + speed.ToString() + " seconds!");
                            attachedApplication.server.SendRPC(messageForAttacking, gameObjectKnownToBeHostile.identifier.name);

                            // Build out the message to everyone else
                            messageToEveryoneInChunk.arguments.Add("A " + identifier.fullName + " charges at " + gameObjectKnownToBeHostile.identifier.name + "!");
                            // Get the other nearby players to notify them our action
                            foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                            {
                                // If this player is in our chunk
                                if (playerEntry.Value.controlledGameObject.position == position && playerEntry.Value.controlledGameObject.identifier.name != gameObjectKnownToBeHostile.identifier.name)
                                    // Send an informational RPC to them letting them know
                                    attachedApplication.server.SendRPC(messageToEveryoneInChunk, playerEntry.Key);
                            }
                        }
                        // Otherwise, we are not fighting another player, so we can format the message differently
                        else
                        {
                            messageToEveryoneInChunk.arguments.Add("A " + identifier.fullName + " charges at " + Processing.Describer.GetArticle(gameObjectKnownToBeHostile.identifier.fullName) + gameObjectKnownToBeHostile.identifier.fullName);

                            // Get the other nearby players to notify them our action
                            foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                            {
                                // If this player is in our chunk
                                if (playerEntry.Value.controlledGameObject.position == position)
                                    // Send an informational RPC to them letting them know
                                    attachedApplication.server.SendRPC(messageToEveryoneInChunk, playerEntry.Key);
                            }
                        }
                        #endregion

                        // Create a new thread where we wait for a bit, then impact the hostile object and do damage
                        new Thread(() =>
                        {
                            Thread.CurrentThread.IsBackground = true; 
                            // Wait for the set amount of time based on how fast the goat is
                            Thread.Sleep((int)(speed * 1000f));

                            // Check if the hostile object is still there, because they might have fled
                            // TODO: For some non peaceful animals, they should go into a pursuit state and chase down the hostile.
                            if(gameObjectKnownToBeHostile.position != position || gameObjectKnownToBeHostile.specialProperties["isDeceased"] == "TRUE")
                            {
                                AIState = AIStates.HUNGRY;
                                currentlyInTheMiddleOfPerformingAction = false;
                            }
                            else
                            {
                                // Calculate damage done to hostile object
                                float damageDealt = weight * speed * 0.10f;
                                // If we have horns, deal 2x more damage + bleeding effect, which will be calculated in the OnStrikeThisGameObjectWithGameObject function
                                if (FindChildrenWithName("horn").Count > 0)
                                {
                                    damageDealt *= 2;
                                    // Deal damage to the hostile object with each horn
                                    foreach (GameObject horn in FindChildrenWithName("horn"))
                                    {
                                        gameObjectKnownToBeHostile.OnStrikeThisGameObjectWithGameObject(this, horn, damageDealt);
                                    }
                                }
                                // Otherwise, ram into the hostile object and do blunt force damage with whole body
                                else
                                {
                                    gameObjectKnownToBeHostile.OnStrikeThisGameObjectWithGameObject(this, this, damageDealt);
                                }
                                

                                #region Notify everyone in the chunk
                                // Create a message entailing our action and send it to nearby players
                                Support.Networking.RPCs.RPCSay messageForAttacking = new Support.Networking.RPCs.RPCSay();
                                Support.Networking.RPCs.RPCSay messageToEveryoneElseInChunk = new Support.Networking.RPCs.RPCSay();

                                // If we are fighting a player, send a message to the player being attacked
                                if (gameObjectKnownToBeHostile.specialProperties.ContainsKey("isPlayer"))
                                {
                                    // Change the message based on if we have horns or not
                                    if (FindChildrenWithName("horn").Count > 0)
                                    {
                                        messageForAttacking.arguments.Add("The " + identifier.fullName + " rammed into you and gored you with it's horns!");
                                        // Build out the message to everyone else
                                        messageToEveryoneElseInChunk.arguments.Add("A " + identifier.fullName + " rammed into " + gameObjectKnownToBeHostile.identifier.name + " and gored them with it's horns!");
                                    }
                                    else
                                    {
                                        messageForAttacking.arguments.Add("The " + identifier.fullName + " rammed into you!");
                                        // Build out the message to everyone else
                                        messageToEveryoneElseInChunk.arguments.Add("A " + identifier.fullName + " rammed into " + gameObjectKnownToBeHostile.identifier.name + "!");
                                    }
                                    // Send the message to the player being attacked
                                    attachedApplication.server.SendRPC(messageForAttacking, gameObjectKnownToBeHostile.identifier.name);


                                    // Get the other nearby players to notify them our action
                                    foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                                    {
                                        // If this player is in our chunk
                                        if (playerEntry.Value.controlledGameObject.position == position && playerEntry.Value.controlledGameObject.identifier.name != gameObjectKnownToBeHostile.identifier.name)
                                            // Send an informational RPC to them letting them know
                                            attachedApplication.server.SendRPC(messageToEveryoneElseInChunk, playerEntry.Key);
                                    }
                                }
                                // Otherwise, we are not fighting another player, so we can format the message differently
                                else
                                {
                                    // Change the message based on if we have horns or not
                                    if (FindChildrenWithName("horn").Count > 0)
                                    {
                                        messageToEveryoneElseInChunk.arguments.Add("A " + identifier.fullName + " rammed into " + Processing.Describer.GetArticle(gameObjectKnownToBeHostile.identifier.fullName) + gameObjectKnownToBeHostile.identifier.fullName + " and gored it with it's horns!");
                                    }
                                    else
                                    {
                                        messageToEveryoneElseInChunk.arguments.Add("A " + identifier.fullName + " rammed into " + Processing.Describer.GetArticle(gameObjectKnownToBeHostile.identifier.fullName) + gameObjectKnownToBeHostile.identifier.fullName + "!");
                                    }

                                    // Get the other nearby players to notify them our action
                                    foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                                    {
                                        // If this player is in our chunk
                                        if (playerEntry.Value.controlledGameObject.position == position)
                                            // Send an informational RPC to them letting them know
                                            attachedApplication.server.SendRPC(messageToEveryoneElseInChunk, playerEntry.Key);
                                    }
                                }
                                #endregion

                                // Turn off the bool that says we are currently in the middle of an action
                                currentlyInTheMiddleOfPerformingAction = false;
                            }   
                        }).Start();
                    }
                }
                break;
            }
            #endregion
        }
        public override void OnStrikeThisGameObjectWithGameObject(GameObject whoIsStriking, GameObject whatIsBeingUsedToStrike, float howMuchDamage)
        {
            // Add the enemy to our percieved threats automatically if they do something aggressive
            gameObjectsPercievedAsThreat.Add(whoIsStriking);
            // TODO: Set this up so creatures can handle fighting multiple hostiles
            gameObjectKnownToBeHostile = whoIsStriking;
            // If the goat was sleeping, it's awake now!
            identifier.descriptiveAdjectives.Remove("sleeping");
            // Go into attacking state
            AIState = AIStates.ATTACKING;
            // Forward the damage to the body, since when someone hits the goat, we will assume they hit the body
            body.specialProperties["health"] = (float.Parse(body.specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) - howMuchDamage).ToString();
            // Create a message entailing our action and send it to nearby players
            Support.Networking.RPCs.RPCSay message = new Support.Networking.RPCs.RPCSay();
            // Add bleeding effect if object being used to strike is sharp
            if (whatIsBeingUsedToStrike.identifier.descriptiveAdjectives.Contains("sharp"))
            {
                body.specialProperties["isBleeding"] = "TRUE";
                message.arguments.Add("The " + identifier.fullName + " now has " + body.specialProperties["health"] + " body health! The flesh is pierced and bleeding!");
            }
            else
                message.arguments.Add("The " + identifier.fullName + " now has " + body.specialProperties["health"] + " body health!");
           
            // Get the nearby players to notify them our action
            foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
            {
                // If this player is in our chunk
                if (playerEntry.Value.controlledGameObject.position == position)
                    // Send an informational RPC to them letting them know
                    attachedApplication.server.SendRPC(message, playerEntry.Key);
            }
        }

        // TODO: Right now, when the head is struck nothing happens. This will need fixed and this function below called when it is struck
        public void OnStrikeHeadWithGameObject(GameObject whoIsStriking, GameObject whatIsBeingUsedToStrike, float howMuchDamage)
        {
            // Add the enemy to our percieved threats automatically if they do something aggressive
            gameObjectsPercievedAsThreat.Add(whoIsStriking);
            // TODO: Set this up so creatures can handle fighting multiple hostiles
            gameObjectKnownToBeHostile = whoIsStriking;
            // Go into attacking state
            AIState = AIStates.ATTACKING;
            // Forward the damage to the head
            head.specialProperties["health"] = (float.Parse(head.specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) - howMuchDamage).ToString();
            // Create a message entailing our action and send it to nearby players
            Support.Networking.RPCs.RPCSay message = new Support.Networking.RPCs.RPCSay();           
            // Add bleeding effect if object being used to strike is sharp
            if (whatIsBeingUsedToStrike.identifier.descriptiveAdjectives.Contains("sharp"))
            {
                head.specialProperties["isBleeding"] = "TRUE";
                message.arguments.Add("The " + identifier.fullName + " now has " + head.specialProperties["health"] + " head health! The flesh is pierced and bleeding!");
            }               
            else
                message.arguments.Add("The " + identifier.fullName + " now has " + head.specialProperties["health"] + " head health!");
            // Get the nearby players to notify them our action
            foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
            {
                // If this player is in our chunk
                if (playerEntry.Value.controlledGameObject.position == position)
                    // Send an informational RPC to them letting them know
                    attachedApplication.server.SendRPC(message, playerEntry.Key);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace CommandSurvivalAdventure.World.Creatures
{
    class CreatureBoar : Creature
    {
        // The AI state of the boar, from which the AI decides actions
        public AIStates AIState = AIStates.HUNGRY;
        // The sleeping timer
        public int sleepingTimer = 0;
        // Whether or not the boar is in the middle of an action
        public bool currentlyInTheMiddleOfPerformingAction = false;
        // The list of game objects percieved to be a threat
        public HashSet<GameObject> gameObjectsPercievedAsThreat = new HashSet<GameObject>();
        // The gameObject that is known to be hostile
        // TODO: Build this as a list soon so that creatures can handle fighting multiple enemies, and flee if too many enemies
        public GameObject gameObjectKnownToBeHostile;
        // The head of the boar
        public CreatureParts.CreaturePartBoar.CreaturePartBoarHead head;
        // The body of the boar
        public CreatureParts.CreaturePartBoar.CreaturePartBoarBody body;

        public CreatureBoar(Application newApplication)
        {
            attachedApplication = newApplication;
            // Set the type
            type = typeof(CreatureBoar);
            identifier.name = "boar";
            random = new Random();

            // Generate the stats            
            speed = 1f + (float)random.NextDouble() * 0.5f;
            specialProperties.Add("stance", "STANDING");
            specialProperties.Add("weight", weight.ToString());
            specialProperties.Add("isDeceased", "FALSE");
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
            // Generate the body parts of the boar
            // TODO: Go through and change all the health, isBleeding, etc attributes to special properties
            #region Front Right Leg
            CreatureParts.CreaturePartBoar.CreaturePartBoarLeg frontRightLeg = new CreatureParts.CreaturePartBoar.CreaturePartBoarLeg();
            frontRightLeg.identifier.name = "leg";
            frontRightLeg.identifier.classifierAdjectives.Add("front");
            frontRightLeg.identifier.classifierAdjectives.Add("right");
            frontRightLeg.identifier.classifierAdjectives.Add("boar");
            frontRightLeg.specialProperties["weight"] = (50 + random.Next(-5, 5)).ToString();
            frontRightLeg.specialProperties["health"] = frontRightLeg.specialProperties["weight"];
            frontRightLeg.specialProperties["isBleeding"] = "FALSE";
            frontRightLeg.specialProperties["isCooked"] = "FALSE";
            frontRightLeg.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Front Left Leg
            CreatureParts.CreaturePartBoar.CreaturePartBoarLeg frontLeftLeg = new CreatureParts.CreaturePartBoar.CreaturePartBoarLeg();
            frontLeftLeg.identifier.name = "leg";
            frontLeftLeg.identifier.classifierAdjectives.Add("front");
            frontLeftLeg.identifier.classifierAdjectives.Add("left");
            frontLeftLeg.identifier.classifierAdjectives.Add("boar");
            frontLeftLeg.specialProperties["weight"] = (50 + random.Next(-5, 5)).ToString();
            frontLeftLeg.specialProperties["health"] = frontLeftLeg.specialProperties["weight"];
            frontLeftLeg.specialProperties["isBleeding"] = "FALSE";
            frontLeftLeg.specialProperties["isCooked"] = "FALSE";
            frontLeftLeg.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Rear Left Leg
            CreatureParts.CreaturePartBoar.CreaturePartBoarLeg rearLeftLeg = new CreatureParts.CreaturePartBoar.CreaturePartBoarLeg();
            rearLeftLeg.identifier.name = "leg";
            rearLeftLeg.identifier.classifierAdjectives.Add("rear");
            rearLeftLeg.identifier.classifierAdjectives.Add("left");
            rearLeftLeg.identifier.classifierAdjectives.Add("boar");
            rearLeftLeg.specialProperties["weight"] = (50 + random.Next(-5, 5)).ToString();
            rearLeftLeg.specialProperties["health"] = rearLeftLeg.specialProperties["weight"];
            rearLeftLeg.specialProperties["isBleeding"] = "FALSE";
            rearLeftLeg.specialProperties["isCooked"] = "FALSE";
            rearLeftLeg.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Rear Right Leg
            CreatureParts.CreaturePartBoar.CreaturePartBoarLeg rearRightLeg = new CreatureParts.CreaturePartBoar.CreaturePartBoarLeg();
            rearRightLeg.identifier.name = "leg";
            rearRightLeg.identifier.classifierAdjectives.Add("rear");
            rearRightLeg.identifier.classifierAdjectives.Add("right");
            rearRightLeg.identifier.classifierAdjectives.Add("boar");
            rearRightLeg.specialProperties["weight"] = (50 + random.Next(-5, 5)).ToString();
            rearRightLeg.specialProperties["health"] = rearRightLeg.specialProperties["weight"];
            rearRightLeg.specialProperties["isBleeding"] = "FALSE";
            rearRightLeg.specialProperties["isCooked"] = "FALSE";
            rearRightLeg.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Head
            head = new CreatureParts.CreaturePartBoar.CreaturePartBoarHead();
            head.identifier.name = "head";
            head.identifier.classifierAdjectives.Add("boar");
            head.specialProperties["weight"] = (50 + random.Next(-2, 2)).ToString();
            head.specialProperties["health"] = head.specialProperties["weight"];
            head.specialProperties["isBleeding"] = "FALSE";
            head.specialProperties["isCooked"] = "FALSE";
            head.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Body
            body = new CreatureParts.CreaturePartBoar.CreaturePartBoarBody();
            body.identifier.name = "body";
            body.identifier.classifierAdjectives.Add("boar");
            body.specialProperties["weight"] = (300 + random.Next(-20, 20)).ToString();
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

            #region Tusk1
            CreatureParts.CreaturePartBoar.CreaturePartBoarTusk tusk1 = new CreatureParts.CreaturePartBoar.CreaturePartBoarTusk();
            tusk1.identifier.name = "tusk";
            tusk1.identifier.classifierAdjectives.Add("boar");
            tusk1.identifier.descriptiveAdjectives.Add("sharp");
            tusk1.specialProperties["weight"] = (4 + random.Next(-2, 2)).ToString();
            tusk1.specialProperties["health"] = tusk1.specialProperties["weight"];
            tusk1.specialProperties["isBleeding"] = "FALSE";
            tusk1.specialProperties["isCooked"] = "FALSE";
            tusk1.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Tusk2
            CreatureParts.CreaturePartBoar.CreaturePartBoarTusk tusk2 = new CreatureParts.CreaturePartBoar.CreaturePartBoarTusk();
            tusk2.identifier.name = "tusk";
            tusk2.identifier.classifierAdjectives.Add("boar");
            tusk2.identifier.descriptiveAdjectives.Add("sharp");
            tusk2.specialProperties["weight"] = (4 + random.Next(-2, 2)).ToString();
            tusk2.specialProperties["health"] = tusk2.specialProperties["weight"];
            tusk2.specialProperties["isBleeding"] = "FALSE";
            tusk2.specialProperties["isCooked"] = "FALSE";
            tusk2.specialProperties["isUnclean"] = "FALSE";
            #endregion

            head.AddChild(tusk1);
            head.AddChild(tusk2);
            
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
            if (specialProperties["isDeceased"] == "TRUE")
                return;
            // If out of health, die
            if (float.Parse(body.specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) <= 0f || float.Parse(head.specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) <= 0f)
            {
                specialProperties["isDeceased"] = "TRUE";
                identifier.descriptiveAdjectives.Add("dead");
                specialProperties["stance"] = StanceToString(Stances.LAYING);
                // Notify everyone
                attachedApplication.server.world.SendMessageToPosition("The " + identifier.fullName + " falls to the ground, dead!", position);
                return;
            }
            #endregion

            #region Update variables
            specialProperties["weight"] = weight.ToString();
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
                    // First, look for any nearby creatures, and attack them if they weigh less than us
                    List<GameObject> nearbyCreaturesThatWeighLess = new List<GameObject>();
                    // Get all the children of this chunk
                    List<GameObject> children = parent.GetAllChildren();
                    // Go through the children and find all the ones that weigh less than us
                    foreach (GameObject child in children)
                    {
                        // If a creature
                        if(typeof(Creature).IsAssignableFrom(child.type))
                        {
                            // And it weighs less than us
                            if(float.Parse(child.specialProperties["weight"], System.Globalization.CultureInfo.InvariantCulture) < float.Parse(specialProperties["weight"], System.Globalization.CultureInfo.InvariantCulture)
                                && child.specialProperties["isDeceased"] != "TRUE")
                            {
                                nearbyCreaturesThatWeighLess.Add(child);
                            }
                        }
                    }

                    // If there are nearby creatures that weigh less than us, attack them
                    if(nearbyCreaturesThatWeighLess.Count > 0)
                    {
                        AIState = AIStates.ATTACKING;
                        gameObjectKnownToBeHostile = nearbyCreaturesThatWeighLess.First();
                    }

                    // Look if there is any nearby plants to eat
                    List<GameObject> plantsToEat = new List<GameObject>();
                    string[] namesOfFood = { "grass", "root", "fern" };
                    // Go through the children and find all the ones with the name of the food in it's name
                    foreach (GameObject child in children)
                    {
                        foreach(string food in namesOfFood)
                        {
                            if (child.identifier.fullName.Contains(food))
                            {
                                plantsToEat.Add(child);
                                break;
                            }
                        }
                    }

                    // If so, eat them
                    if (plantsToEat.Count > 0)
                    {
                        // If content and has food, possibly sleep
                        if (random.Next(0, 120) == 0)
                        {
                            // Change the stance to laying down
                            specialProperties["stance"] = StanceToString(Stances.LAYING);
                            // Notify everyone
                            attachedApplication.server.world.SendMessageToPosition(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " lays down to sleep.", position);
                            // Go into the sleeping state
                            AIState = AIStates.SLEEPING;
                            // Set the sleeping timer
                            sleepingTimer = random.Next(60, 120);
                            // Set the adjective
                            identifier.descriptiveAdjectives.Add("sleeping");
                            break;
                        }
                        // Every so often eat the plants
                        else if (random.Next(0, 60) == 0)
                        {
                            // Notify everyone
                            attachedApplication.server.world.SendMessageToPosition(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " eats the " + plantsToEat.First().identifier.fullName + "...", position);
                        }
                        // Boar grunting
                        else if (random.Next(1, 60) == 1)
                        {
                            // Notify everyone
                            attachedApplication.server.world.SendMessageToPosition(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " lets out a deep, threatening grunt...", position);
                        }
                        // Boar looks at random nearby player
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
                                attachedApplication.server.world.SendMessageToPosition(
                                    Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " looks at you with sinister eyes...",
                                    playerToLookAt.name,
                                    Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " looks at " + playerToLookAt.name + " with sinister eyes...",
                                    position);
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
                        // Notify everyone in this chunk and destination chunk
                        attachedApplication.server.world.SendMessageToPosition(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " trots " + Direction.DirectionToString(desiredDirection) + " in search of food...", position);
                        attachedApplication.server.world.SendMessageToPosition(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " trots along from the " + Direction.DirectionToString(Direction.GetOpposite(desiredDirection)) + " in search of food...", newPosition);
                        // Move the object
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
                        // Notify everyone
                        attachedApplication.server.world.SendMessageToPosition(Processing.Describer.GetArticle(identifier.fullName).ToUpper() + " " + identifier.fullName + " wakes from it's slumber and stands up.", position);
                        break;
                    }
                }
                else if (AIState == AIStates.ATTACKING)
                {
                    // TODO: Add a decision tree where the boar decides whether or not to attack based on it's own health, whether or not it has horns, and if it weighs enough to stand a chance to fight

                    // If we're not currently in the middle of another action, attack!
                    if (!currentlyInTheMiddleOfPerformingAction)
                    {
                        // Set the bool that we are currently in the middle of an action
                        currentlyInTheMiddleOfPerformingAction = true;

                        // Notify everyone in the chunk

                        // If we are fighting a player, send a message to the player being attacked
                        if (gameObjectKnownToBeHostile.specialProperties.ContainsKey("isPlayer"))
                        {
                            attachedApplication.server.world.SendMessageToPosition("The " + identifier.fullName + " charges at you! It will hit you in " + (1 / speed * 20f).ToString() + " seconds!", gameObjectKnownToBeHostile.identifier.name, "A " + identifier.fullName + " charges at " + gameObjectKnownToBeHostile.identifier.name + "!", position);
                        }
                        // Otherwise, we are not fighting another player, so we can format the message differently
                        else
                        {
                            attachedApplication.server.world.SendMessageToPosition("A " + identifier.fullName + " charges at " + Processing.Describer.GetArticle(gameObjectKnownToBeHostile.identifier.fullName) + " " + gameObjectKnownToBeHostile.identifier.fullName, position);
                        }

                        // Create a new thread where we wait for a bit, then impact the hostile object and do damage
                        new Thread(() =>
                        {
                            Thread.CurrentThread.IsBackground = true;
                            // Wait for the set amount of time based on how fast the boar is
                            Thread.Sleep((int)(1 / speed * 20f * 1000f));

                            // Check if the hostile object is still there, because they might have fled
                            // TODO: For some non peaceful animals, they should go into a pursuit state and chase down the hostile.
                            if (gameObjectKnownToBeHostile.position != position || gameObjectKnownToBeHostile.specialProperties["isDeceased"] == "TRUE")
                            {
                                AIState = AIStates.HUNGRY;
                                currentlyInTheMiddleOfPerformingAction = false;
                            }
                            else
                            {
                                // We are now in the hostile object's proximity
                                gameObjectsInProximity.Add(gameObjectKnownToBeHostile);
                                gameObjectKnownToBeHostile.gameObjectsInProximity.Add(this);
                                gameObjectKnownToBeHostile.OnEnterProximity(this);
                                // Calculate damage done to hostile object
                                float damageDealt = weight * speed * 0.01f;
                                // If we have weapons, deal 2x more damage + bleeding effect, which will be calculated in the OnStrikeThisGameObjectWithGameObject function
                                if (FindChildrenWithName("tusk").Count > 0)
                                {
                                    damageDealt *= 2 * FindChildrenWithName("tusk").Count;
                                    gameObjectKnownToBeHostile.OnStrikeThisGameObjectWithGameObject(this, FindChildrenWithName("tusk").First(), damageDealt);
                                    // If we are fighting a player, send a message to the player being attacked
                                    if (gameObjectKnownToBeHostile.specialProperties.ContainsKey("isPlayer"))
                                    {
                                        attachedApplication.server.world.SendMessageToPosition(
                                        "The " + identifier.fullName + " rammed into you and gored you with it's tusks, dealing " + damageDealt.ToString() + " damage!",
                                        gameObjectKnownToBeHostile.identifier.name,
                                        "A " + identifier.fullName + " rammed into " + gameObjectKnownToBeHostile.identifier.name + " and gored them with it's tusks!",
                                        position);
                                    }
                                    else
                                    {
                                        attachedApplication.server.world.SendMessageToPosition(
                                        "A " + identifier.fullName + " rammed into " + Processing.Describer.GetArticle(gameObjectKnownToBeHostile.identifier.fullName) + " " + gameObjectKnownToBeHostile.identifier.fullName + " and gored it with it's tusks!",
                                        position);
                                    }
                                    
                                }
                                // Otherwise, ram into the hostile object and do blunt force damage with whole body
                                else
                                {
                                    gameObjectKnownToBeHostile.OnStrikeThisGameObjectWithGameObject(this, this, damageDealt);
                                    // If we are fighting a player, send a message to the player being attacked
                                    if (gameObjectKnownToBeHostile.specialProperties.ContainsKey("isPlayer"))
                                    {
                                        attachedApplication.server.world.SendMessageToPosition(
                                        "The " + identifier.fullName + " rammed into you, dealing " + damageDealt.ToString() + " damage!",
                                        gameObjectKnownToBeHostile.identifier.name,
                                        "A " + identifier.fullName + " rammed into " + gameObjectKnownToBeHostile.identifier.name + "!",
                                        position);
                                    }
                                    else
                                    {
                                        attachedApplication.server.world.SendMessageToPosition(
                                        "A " + identifier.fullName + " rammed into " + Processing.Describer.GetArticle(gameObjectKnownToBeHostile.identifier.fullName) + " " + gameObjectKnownToBeHostile.identifier.fullName + "!",
                                        position);
                                    }
                                }

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
            // If the boar was sleeping, it's awake now!
            identifier.descriptiveAdjectives.Remove("sleeping");
            // Go into attacking state
            AIState = AIStates.ATTACKING;
            // Forward the damage to the body, since when someone hits the boar, we will assume they hit the body
            body.specialProperties["health"] = (float.Parse(body.specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) - howMuchDamage).ToString();
            // Add bleeding effect if object being used to strike is sharp
            if (whatIsBeingUsedToStrike.identifier.descriptiveAdjectives.Contains("sharp"))
            {
                body.specialProperties["isBleeding"] = "TRUE";
                // Notify everyone
                attachedApplication.server.world.SendMessageToPosition(
                    "The " + identifier.fullName + " now has " + body.specialProperties["health"] + " body health! The flesh is pierced and bleeding!",
                    position);
            }
            else
                // Notify everyone
                attachedApplication.server.world.SendMessageToPosition("The " + identifier.fullName + " now has " + body.specialProperties["health"] + " body health!", position);
        }

        // TODO: Right now, when the head is struck nothing happens. This will need fixed and this function below called when it is struck
        public void OnStrikeHeadWithGameObject(GameObject whoIsStriking, GameObject whatIsBeingUsedToStrike, float howMuchDamage)
        {
            // Add the enemy to our percieved threats automatically if they do something aggressive
            gameObjectsPercievedAsThreat.Add(whoIsStriking);
            // TODO: Set this up so creatures can handle fighting multiple hostiles
            gameObjectKnownToBeHostile = whoIsStriking;
            // If the boar was sleeping, it's awake now!
            identifier.descriptiveAdjectives.Remove("sleeping");
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
                // Notify everyone
                attachedApplication.server.world.SendMessageToPosition(
                    "The " + identifier.fullName + " now has " + head.specialProperties["health"] + " head health! The flesh is pierced and bleeding!",
                    position);
            }
            else
                // Notify everyone
                attachedApplication.server.world.SendMessageToPosition(
                    "The " + identifier.fullName + " now has " + head.specialProperties["health"] + " head health!",
                    position);
        }
    }
}

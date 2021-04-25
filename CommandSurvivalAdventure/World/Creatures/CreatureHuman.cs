using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Creatures
{
    class CreatureHuman : Creature
    {
        // Quick references for body parts
        CreatureParts.CreaturePartHuman.CreaturePartHumanHead head;
        CreatureParts.CreaturePartHuman.CreaturePartHumanBody body;

        // Generate
        public CreatureHuman(Application newApplication)
        {
            attachedApplication = newApplication;
            // Set the type
            type = typeof(CreatureHuman);
            // Make a new seeded random instance for generating stats about the Minotaur
            Random random = new Random();
            // Set the properties
            identifier.name = "human";
            // Generate the stats for the minotaur
            specialProperties.Add("stance", "STANDING");
            specialProperties.Add("blocking", "FALSE");
            specialProperties.Add("isDeceased", "FALSE");
            specialProperties.Add("isStunned", "FALSE");
            specialProperties.Add("strength", random.Next(60, 80).ToString());
            specialProperties.Add("reactionTime", (random.NextDouble() * 2.0f + 3f).ToString());
            specialProperties.Add("speed", (random.NextDouble() * 2.0f).ToString());
            // Generate the body parts of the human

            #region Right Hand
            CreatureParts.CreaturePartHuman.CreaturePartHumanHand rightHand = new CreatureParts.CreaturePartHuman.CreaturePartHumanHand();
            rightHand.identifier.name = "hand";
            rightHand.identifier.classifierAdjectives.Add("right");
            rightHand.specialProperties["weight"] = (4 + random.Next(-1, 1)).ToString();
            rightHand.specialProperties["health"] = rightHand.specialProperties["weight"];
            rightHand.specialProperties["isBleeding"] = "FALSE";
            rightHand.specialProperties["isCooked"] = "FALSE";
            rightHand.specialProperties["isUnclean"] = "FALSE";
            rightHand.specialProperties.Add("isUtilizable", "");
            #endregion

            #region Left Hand
            CreatureParts.CreaturePartHuman.CreaturePartHumanHand leftHand = new CreatureParts.CreaturePartHuman.CreaturePartHumanHand();
            leftHand.identifier.name = "hand";
            leftHand.identifier.classifierAdjectives.Add("left");
            leftHand.specialProperties["weight"] = rightHand.specialProperties["weight"];
            leftHand.specialProperties["health"] = leftHand.specialProperties["weight"];
            leftHand.specialProperties["isBleeding"] = "FALSE";
            leftHand.specialProperties["isCooked"] = "FALSE";
            leftHand.specialProperties["isUnclean"] = "FALSE";
            leftHand.specialProperties.Add("isUtilizable", "");
            #endregion

            #region Right Arm
            CreatureParts.CreaturePartHuman.CreaturePartHumanArm rightArm = new CreatureParts.CreaturePartHuman.CreaturePartHumanArm();
            rightArm.identifier.name = "arm";
            rightArm.identifier.classifierAdjectives.Add("right");
            rightArm.specialProperties["weight"] = (12 + random.Next(-3, 3)).ToString();
            rightArm.specialProperties["health"] = rightArm.specialProperties["weight"];
            rightArm.specialProperties["isBleeding"] = "FALSE";
            rightArm.specialProperties["isCooked"] = "FALSE";
            rightArm.specialProperties["isUnclean"] = "FALSE";
            rightArm.AddChild(rightHand);
            #endregion

            #region Left Arm
            CreatureParts.CreaturePartHuman.CreaturePartHumanArm leftArm = new CreatureParts.CreaturePartHuman.CreaturePartHumanArm();
            leftArm.identifier.name = "arm";
            leftArm.identifier.classifierAdjectives.Add("left");
            leftArm.specialProperties["weight"] = rightArm.specialProperties["weight"];
            leftArm.specialProperties["health"] = leftArm.specialProperties["weight"];
            leftArm.specialProperties["isBleeding"] = "FALSE";
            leftArm.specialProperties["isCooked"] = "FALSE";
            leftArm.specialProperties["isUnclean"] = "FALSE";
            leftArm.AddChild(leftHand);
            #endregion

            #region Right Foot
            CreatureParts.CreaturePartHuman.CreaturePartHumanFoot rightFoot = new CreatureParts.CreaturePartHuman.CreaturePartHumanFoot();
            rightFoot.identifier.name = "foot";
            rightFoot.identifier.classifierAdjectives.Add("right");
            rightFoot.specialProperties["weight"] = (3 + random.Next(-1, 1)).ToString();
            rightFoot.specialProperties["health"] = rightFoot.specialProperties["weight"];
            rightFoot.specialProperties["isBleeding"] = "FALSE";
            rightFoot.specialProperties["isCooked"] = "FALSE";
            rightFoot.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Left Foot
            CreatureParts.CreaturePartHuman.CreaturePartHumanFoot leftFoot = new CreatureParts.CreaturePartHuman.CreaturePartHumanFoot();
            leftFoot.identifier.name = "foot";
            leftFoot.identifier.classifierAdjectives.Add("left");
            leftFoot.specialProperties["weight"] = rightFoot.specialProperties["weight"];
            leftFoot.specialProperties["health"] = leftFoot.specialProperties["weight"];
            leftFoot.specialProperties["isBleeding"] = "FALSE";
            leftFoot.specialProperties["isCooked"] = "FALSE";
            leftFoot.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Left Leg
            CreatureParts.CreaturePartHuman.CreaturePartHumanLeg leftLeg = new CreatureParts.CreaturePartHuman.CreaturePartHumanLeg();
            leftLeg.identifier.name = "leg";
            leftLeg.identifier.classifierAdjectives.Add("left");
            leftLeg.specialProperties["weight"] = (40 + random.Next(-5, 5)).ToString();
            leftLeg.specialProperties["health"] = leftLeg.specialProperties["weight"];
            leftLeg.specialProperties["isBleeding"] = "FALSE";
            leftLeg.specialProperties["isCooked"] = "FALSE";
            leftLeg.specialProperties["isUnclean"] = "FALSE";
            leftLeg.AddChild(leftFoot);
            #endregion

            #region Right Leg
            CreatureParts.CreaturePartHuman.CreaturePartHumanLeg rightLeg = new CreatureParts.CreaturePartHuman.CreaturePartHumanLeg();
            rightLeg.identifier.name = "leg";
            rightLeg.identifier.classifierAdjectives.Add("right");
            rightLeg.specialProperties["weight"] = leftLeg.specialProperties["weight"];
            rightLeg.specialProperties["health"] = rightLeg.specialProperties["weight"];
            rightLeg.specialProperties["isBleeding"] = "FALSE";
            rightLeg.specialProperties["isCooked"] = "FALSE";
            rightLeg.specialProperties["isUnclean"] = "FALSE";
            rightLeg.AddChild(rightFoot);
            #endregion

            #region Head
            head = new CreatureParts.CreaturePartHuman.CreaturePartHumanHead();
            head.identifier.name = "head";
            head.specialProperties["weight"] = (10 + random.Next(-2, 2)).ToString();
            head.specialProperties["health"] = head.specialProperties["weight"];
            head.specialProperties["isBleeding"] = "FALSE";
            head.specialProperties["isCooked"] = "FALSE";
            head.specialProperties["isUnclean"] = "FALSE";
            #endregion

            #region Torso
            body = new CreatureParts.CreaturePartHuman.CreaturePartHumanBody();
            body.identifier.name = "body";
            body.specialProperties["weight"] = (70 + random.Next(-10, 10)).ToString();
            body.specialProperties["health"] = body.specialProperties["weight"];
            body.specialProperties["isBleeding"] = "FALSE";
            body.specialProperties["isCooked"] = "FALSE";
            body.specialProperties["isUnclean"] = "FALSE";
            // Add all the creature parts
            body.AddChild(rightArm);
            body.AddChild(leftArm);
            body.AddChild(leftLeg);
            body.AddChild(rightLeg);
            body.AddChild(head);
            #endregion

            AddChild(body);
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

            // If dead, do not update
            if (specialProperties["isDeceased"] == "TRUE")
                return;
            // If out of health, do death function
            if (float.Parse(body.specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) <= 0 || float.Parse(head.specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                OnDeath();

        }
        
        public override void OnStrikeThisGameObjectWithGameObject(GameObject whoIsStriking, GameObject whatIsBeingUsedToStrike, float howMuchDamage)
        {
            // Forward the damage to the body, since when someone hits the human, we will assume they hit the body
            body.specialProperties["health"] = (float.Parse(body.specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) - howMuchDamage).ToString();
            if (float.Parse(body.specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) <= 0)
                body.specialProperties["health"] = 0f.ToString();
            // If we're out of health, we are struck down
            if (float.Parse(body.specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) <= 0 && specialProperties["isDeceased"] == "FALSE")
            {
                body.specialProperties["health"] = 0f.ToString();
                specialProperties["isDeceased"] = "TRUE";
                // Create a message entailing the damage done and send to player
                Support.Networking.RPCs.RPCSay deathMessage = new Support.Networking.RPCs.RPCSay();
                deathMessage.arguments.Add("You were struck down!");
                // Create a message entailing our action and send it to nearby players
                Support.Networking.RPCs.RPCSay deathMessageToEveryoneElse = new Support.Networking.RPCs.RPCSay();
                deathMessageToEveryoneElse.arguments.Add(identifier.fullName + " was struck down!");

                // Send message to person who died
                attachedApplication.server.SendRPC(deathMessage, identifier.name);

                // Get the nearby players to notify them our action
                foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
                {
                    // If this player is in our chunk
                    //if (playerEntry.Value.controlledGameObject.position == position && playerEntry.Key != identifier.name)
                    if (playerEntry.Key != identifier.name)
                        // Send an informational RPC to them letting them know
                        attachedApplication.server.SendRPC(deathMessageToEveryoneElse, playerEntry.Key);
                }
                OnDeath();
            }

            #region Notify everyone
            // Create a message entailing the damage done and send to player
            Support.Networking.RPCs.RPCSay messageToPlayer = new Support.Networking.RPCs.RPCSay();

            // Create a message entailing our action and send it to nearby players
            Support.Networking.RPCs.RPCSay messageToEveryoneElse = new Support.Networking.RPCs.RPCSay();
            // Add bleeding effect if object being used to strike is sharp
            if (whatIsBeingUsedToStrike.identifier.descriptiveAdjectives.Contains("sharp"))
            {
                body.specialProperties["isBleeding"] = "TRUE";
                messageToEveryoneElse.arguments.Add(identifier.fullName + " now has " + body.specialProperties["health"] + " body health! The flesh is pierced and bleeding!");
                messageToPlayer.arguments.Add("You now have " + body.specialProperties["health"] + " body health! The flesh is pierced and bleeding!");
            }
            else
            {
                messageToEveryoneElse.arguments.Add(identifier.fullName + " now has " + body.specialProperties["health"] + " body health!");
                messageToPlayer.arguments.Add("You now have " + body.specialProperties["health"] + " body health!");
            }
            attachedApplication.server.SendRPC(messageToPlayer, identifier.name);

            // Get the nearby players to notify them our action
            foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
            {
                // If this player is in our chunk
                if (playerEntry.Value.controlledGameObject.position == position && playerEntry.Key != identifier.name)
                    // Send an informational RPC to them letting them know
                    attachedApplication.server.SendRPC(messageToEveryoneElse, playerEntry.Key);
            }
            #endregion

        }

        public void OnDeath()
        {
            #region Notify everyone
            // Create a message entailing the damage done and send to player
            Support.Networking.RPCs.RPCSay messageToPlayer = new Support.Networking.RPCs.RPCSay();
            messageToPlayer.arguments.Add("You are deceased.");
            // Create a message entailing our action and send it to nearby players
            Support.Networking.RPCs.RPCSay messageToEveryoneElse = new Support.Networking.RPCs.RPCSay();
            messageToEveryoneElse.arguments.Add(identifier.fullName + " is deceased.");
            
            // Send message to person who died
            attachedApplication.server.SendRPC(messageToPlayer, identifier.name);

            // Get the nearby players to notify them our action
            foreach (KeyValuePair<string, Core.Player> playerEntry in attachedApplication.server.world.players)
            {
                // If this player is in our chunk
                //if (playerEntry.Value.controlledGameObject.position == position && playerEntry.Key != identifier.name)
                if (playerEntry.Key != identifier.name)
                    // Send an informational RPC to them letting them know
                    attachedApplication.server.SendRPC(messageToEveryoneElse, playerEntry.Key);
            }
            #endregion
        }
    }
}

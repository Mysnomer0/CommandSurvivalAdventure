using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Creatures
{
    class CreatureMinotaur : Creature
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
        }
        // Generate
        public CreatureMinotaur()
        {
            // Set the type
            type = typeof(CreatureMinotaur);
            // Make a new seeded random instance for generating stats about the Minotaur
            Random random = new Random();
            // Set the properties
            identifier.name = "minotaur";
            // Generate the stats for the minotaur
            specialProperties.Add("stance", "STANDING");
            specialProperties.Add("blocking", "NULL");
            specialProperties.Add("isDeceased", "NULL");
            specialProperties.Add("isStunned", "NULL");
            specialProperties.Add("strength", random.Next(60, 80).ToString());
            specialProperties.Add("weight", random.Next(250, 300).ToString());
            specialProperties.Add("reactionTime", (random.NextDouble() * 2.0f + 3f).ToString());
            specialProperties.Add("speed", (random.NextDouble() * 2.0f).ToString());
            // Generate the body parts of the Minotaur

            #region Right Hand
            CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurHand rightHand = new CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurHand();
            rightHand.identifier.name = "hand";
            rightHand.identifier.classifierAdjectives.Add("right");
            rightHand.weight = (2 * (random.Next(9, 11) / 10));
            rightHand.health = rightHand.weight;
            rightHand.muscleContent = rightHand.weight * (random.Next(8, 12) / 10);
            rightHand.fatContent = rightHand.weight * (random.Next(8, 12) / 10);
            rightHand.isBleeding = false;
            rightHand.isCooked = false;
            rightHand.isUnclean = false;
            rightHand.specialProperties.Add("isUtilizable", "");
            #endregion

            #region Left Hand
            CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurHand leftHand = new CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurHand();
            leftHand.identifier.name = "hand";
            leftHand.identifier.classifierAdjectives.Add("left");
            leftHand.weight = (2 * (random.Next(9, 11) / 10));
            leftHand.health = leftHand.weight;
            leftHand.muscleContent = leftHand.weight * (random.Next(8, 12) / 10);
            leftHand.fatContent = leftHand.weight * (random.Next(8, 12) / 10);
            leftHand.isBleeding = false;
            leftHand.isCooked = false;
            leftHand.isUnclean = false;
            leftHand.specialProperties.Add("isUtilizable", "");
            #endregion

            #region Right Arm
            CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurArm rightArm = new CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurArm();
            rightArm.identifier.name = "arm";
            rightArm.identifier.classifierAdjectives.Add("right");
            rightArm.weight = (20 * (random.Next(9, 11) / 10));
            rightArm.health = rightArm.weight;
            rightArm.muscleContent = rightArm.weight * (random.Next(8, 12) / 10);
            rightArm.fatContent = rightArm.weight * (random.Next(8, 12) / 10);
            rightArm.isBleeding = false;
            rightArm.isCooked = false;
            rightArm.isUnclean = false;
            rightArm.AddChild(rightHand);
            #endregion

            #region Left Arm
            CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurArm leftArm = new CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurArm();
            leftArm.identifier.name = "arm";
            leftArm.identifier.classifierAdjectives.Add("left");
            leftArm.weight = (20 * (random.Next(9, 11) / 10));
            leftArm.health = leftArm.weight;
            leftArm.muscleContent = leftArm.weight * (random.Next(8, 12) / 10);
            leftArm.fatContent = leftArm.weight * (random.Next(8, 12) / 10);
            leftArm.isBleeding = false;
            leftArm.isCooked = false;
            leftArm.isUnclean = false;
            leftArm.AddChild(leftHand);
            #endregion

            #region Right Foot
            CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurFoot rightFoot = new CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurFoot();
            rightFoot.identifier.name = "foot";
            rightFoot.identifier.classifierAdjectives.Add("right");
            rightFoot.weight = (3 * (random.Next(9, 11) / 10));
            rightFoot.health = rightFoot.weight;
            rightFoot.muscleContent = rightFoot.weight * (random.Next(8, 12) / 10);
            rightFoot.fatContent = rightFoot.weight * (random.Next(8, 12) / 10);
            rightFoot.isBleeding = false;
            rightFoot.isCooked = false;
            rightFoot.isUnclean = false;
            #endregion

            #region Left Foot
            CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurFoot leftFoot = new CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurFoot();
            leftFoot.identifier.name = "foot";
            leftFoot.identifier.classifierAdjectives.Add("left");
            leftFoot.weight = (3 * (random.Next(9, 11) / 10));
            leftFoot.health = leftFoot.weight;
            leftFoot.muscleContent = leftFoot.weight * (random.Next(8, 12) / 10);
            leftFoot.fatContent = leftFoot.weight * (random.Next(8, 12) / 10);
            leftFoot.isBleeding = false;
            leftFoot.isCooked = false;
            leftFoot.isUnclean = false;
            #endregion

            #region Left Leg
            CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurLeg leftLeg = new CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurLeg();
            leftLeg.identifier.name = "leg";
            leftLeg.identifier.classifierAdjectives.Add("left");
            leftLeg.weight = (25 * (random.Next(9, 11) / 10));
            leftLeg.health = leftLeg.weight;
            leftLeg.muscleContent = leftLeg.weight * (random.Next(8, 12) / 10);
            leftLeg.fatContent = leftLeg.weight * (random.Next(8, 12) / 10);
            leftLeg.isBleeding = false;
            leftLeg.isCooked = false;
            leftLeg.isUnclean = false;
            leftLeg.AddChild(leftFoot);
            #endregion

            #region Right Leg
            CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurLeg rightLeg = new CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurLeg();
            rightLeg.identifier.name = "leg";
            rightLeg.identifier.classifierAdjectives.Add("right");
            rightLeg.weight = (25 * (random.Next(9, 11) / 10));
            rightLeg.health = rightLeg.weight;
            rightLeg.muscleContent = rightLeg.weight * (random.Next(8, 12) / 10);
            rightLeg.fatContent = rightLeg.weight * (random.Next(8, 12) / 10);
            rightLeg.isBleeding = false;
            rightLeg.isCooked = false;
            rightLeg.isUnclean = false;
            rightLeg.AddChild(rightFoot);
            #endregion

            #region Head
            CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurHead head = new CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurHead();
            head.identifier.name = "head";
            head.weight = (10 * (random.Next(9, 11) / 10));
            head.health = head.weight;
            head.muscleContent = head.weight * (random.Next(8, 12) / 10);
            head.fatContent = head.weight * (random.Next(8, 12) / 10);
            head.isBleeding = false;
            head.isCooked = false;
            head.isUnclean = false;
            #endregion

            #region Torso
            CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurHead torso = new CreatureParts.CreaturePartMinotaur.CreaturePartMinotaurHead();
            torso.identifier.name = "torso";
            torso.weight = (100 * (random.Next(9, 11) / 10));
            torso.health = torso.weight;
            torso.muscleContent = torso.weight * (random.Next(8, 12) / 10);
            torso.fatContent = torso.weight * (random.Next(8, 12) / 10);
            torso.isBleeding = false;
            torso.isCooked = false;
            torso.isUnclean = false;
            // Add all the creature parts
            torso.AddChild(rightArm);
            torso.AddChild(leftArm);
            torso.AddChild(leftLeg);
            torso.AddChild(rightLeg);
            torso.AddChild(head);
            #endregion

            AddChild(torso);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Creatures
{
    class CreatureSheep : Creature
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
        public CreatureSheep()
        {
            // Set the type
            type = typeof(CreatureSheep);
            identifier.name = "sheep";

            // Make a new seeded random instance for generating stats about the Minotaur
            Random random = new Random();
            // Set the properties
            identifier.name = "minotaur";
            // Generate the stats for the minotaur
            specialProperties.Add("stance", "STANDING");
            specialProperties.Add("blocking", "NULL");
            specialProperties.Add("isDeceased", "NULL");
            specialProperties.Add("isStunned", "NULL");
            specialProperties.Add("strength", random.Next(10, 30).ToString());
            specialProperties.Add("weight", random.Next(99, 320).ToString());
            specialProperties.Add("reactionTime", (random.NextDouble() * 2.0f + 3f).ToString());
            specialProperties.Add("speed", (random.NextDouble() * 2.0f).ToString());
            // Generate the body parts of the sheep
            #region Front Right Leg
            CreatureParts.CreaturePartSheep.CreaturePartSheepLeg frontRightLeg = new CreatureParts.CreaturePartSheep.CreaturePartSheepLeg();
            frontRightLeg.identifier.name = "leg";
            frontRightLeg.identifier.classifierAdjectives.Add("front");
            frontRightLeg.identifier.classifierAdjectives.Add("right");
            frontRightLeg.weight = (10 * (random.Next(9, 11) / 10));
            frontRightLeg.health = frontRightLeg.weight;
            frontRightLeg.muscleContent = frontRightLeg.weight * (random.Next(8, 12) / 10);
            frontRightLeg.fatContent = frontRightLeg.weight * (random.Next(8, 12) / 10);
            frontRightLeg.isBleeding = false;
            frontRightLeg.isCooked = false;
            frontRightLeg.isUnclean = false;
            #endregion

            #region Front Left Leg
            CreatureParts.CreaturePartSheep.CreaturePartSheepLeg frontLeftLeg = new CreatureParts.CreaturePartSheep.CreaturePartSheepLeg();
            frontLeftLeg.identifier.name = "leg";
            frontLeftLeg.identifier.classifierAdjectives.Add("front");
            frontLeftLeg.identifier.classifierAdjectives.Add("left");
            frontLeftLeg.weight = (10 * (random.Next(9, 11) / 10));
            frontLeftLeg.health = frontLeftLeg.weight;
            frontLeftLeg.muscleContent = frontLeftLeg.weight * (random.Next(8, 12) / 10);
            frontLeftLeg.fatContent = frontLeftLeg.weight * (random.Next(8, 12) / 10);
            frontLeftLeg.isBleeding = false;
            frontLeftLeg.isCooked = false;
            frontLeftLeg.isUnclean = false;
            #endregion

            #region Rear Left Leg
            CreatureParts.CreaturePartSheep.CreaturePartSheepLeg rearLeftLeg = new CreatureParts.CreaturePartSheep.CreaturePartSheepLeg();
            rearLeftLeg.identifier.name = "leg";
            rearLeftLeg.identifier.classifierAdjectives.Add("rear");
            rearLeftLeg.identifier.classifierAdjectives.Add("left");
            rearLeftLeg.weight = (15 * (random.Next(9, 11) / 10));
            rearLeftLeg.health = rearLeftLeg.weight;
            rearLeftLeg.muscleContent = rearLeftLeg.weight * (random.Next(8, 12) / 10);
            rearLeftLeg.fatContent = rearLeftLeg.weight * (random.Next(8, 12) / 10);
            rearLeftLeg.isBleeding = false;
            rearLeftLeg.isCooked = false;
            rearLeftLeg.isUnclean = false;
            #endregion

            #region Rear Right Leg
            CreatureParts.CreaturePartSheep.CreaturePartSheepLeg rearRightLeg = new CreatureParts.CreaturePartSheep.CreaturePartSheepLeg();
            rearRightLeg.identifier.name = "leg";
            rearRightLeg.identifier.classifierAdjectives.Add("rear");
            rearRightLeg.identifier.classifierAdjectives.Add("right");
            rearRightLeg.weight = (15 * (random.Next(9, 11) / 10));
            rearRightLeg.health = rearRightLeg.weight;
            rearRightLeg.muscleContent = rearRightLeg.weight * (random.Next(8, 12) / 10);
            rearRightLeg.fatContent = rearRightLeg.weight * (random.Next(8, 12) / 10);
            rearRightLeg.isBleeding = false;
            rearRightLeg.isCooked = false;
            rearRightLeg.isUnclean = false;
            #endregion

            #region Head
            CreatureParts.CreaturePartSheep.CreaturePartSheepHead head = new CreatureParts.CreaturePartSheep.CreaturePartSheepHead();
            head.identifier.name = "head";
            head.weight = (8 * (random.Next(9, 11) / 10));
            head.health = head.weight;
            head.muscleContent = head.weight * (random.Next(8, 12) / 10);
            head.fatContent = head.weight * (random.Next(8, 12) / 10);
            head.isBleeding = false;
            head.isCooked = false;
            head.isUnclean = false;
            #endregion

            #region Torso
            CreatureParts.CreaturePartSheep.CreaturePartSheepTorso torso = new CreatureParts.CreaturePartSheep.CreaturePartSheepTorso();
            torso.identifier.name = "torso";
            torso.weight = (30 * (random.Next(9, 11) / 10));
            torso.health = torso.weight;
            torso.muscleContent = torso.weight * (random.Next(8, 12) / 10);
            torso.fatContent = torso.weight * (random.Next(8, 12) / 10);
            torso.isBleeding = false;
            torso.isCooked = false;
            torso.isUnclean = false;
            #endregion
            // Add all the creature parts
            AddChild(frontRightLeg);
            AddChild(frontLeftLeg);
            AddChild(rearLeftLeg);
            AddChild(rearRightLeg);
            AddChild(head);
            AddChild(torso);
        }
    }
}
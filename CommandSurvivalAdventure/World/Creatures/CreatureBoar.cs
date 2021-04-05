using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Creatures
{
    class CreatureBoar : Creature
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

        public CreatureBoar()
        {
            // Set the type
            type = typeof(CreatureBoar);

            // Name Creature
            identifier.name = "boar";
            // Make a new seeded random instance for generating stats about the Minotaur
            Random random = new Random();
            // Set the properties
            identifier.name = "minotaur";
            // Generate the stats for the minotaur
            specialProperties.Add("stance", "STANDING");
            specialProperties.Add("blocking", "NULL");
            specialProperties.Add("isDeceased", "NULL");
            specialProperties.Add("isStunned", "NULL");
            specialProperties.Add("strength", random.Next(30, 50).ToString());
            specialProperties.Add("weight", random.Next(130, 220).ToString());
            specialProperties.Add("reactionTime", (random.NextDouble() * 2.0f + 3f).ToString());
            specialProperties.Add("speed", (random.NextDouble() * 2.0f).ToString());
            // Generate the body parts of the Boar
            #region Front Right Leg
            CreatureParts.CreaturePartBoar.CreaturePartBoarLeg frontRightLeg = new CreatureParts.CreaturePartBoar.CreaturePartBoarLeg();
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
            CreatureParts.CreaturePartBoar.CreaturePartBoarLeg frontLeftLeg = new CreatureParts.CreaturePartBoar.CreaturePartBoarLeg();
            frontLeftLeg.identifier.name = "front left leg";
            frontRightLeg.identifier.classifierAdjectives.Add("front");
            frontRightLeg.identifier.classifierAdjectives.Add("left");
            frontLeftLeg.weight = (10 * (random.Next(9, 11) / 10));
            frontLeftLeg.health = frontLeftLeg.weight;
            frontLeftLeg.muscleContent = frontLeftLeg.weight * (random.Next(8, 12) / 10);
            frontLeftLeg.fatContent = frontLeftLeg.weight * (random.Next(8, 12) / 10);
            frontLeftLeg.isBleeding = false;
            frontLeftLeg.isCooked = false;
            frontLeftLeg.isUnclean = false;
            #endregion

            #region Rear Left Leg
            CreatureParts.CreaturePartBoar.CreaturePartBoarLeg rearLeftLeg = new CreatureParts.CreaturePartBoar.CreaturePartBoarLeg();
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
            CreatureParts.CreaturePartBoar.CreaturePartBoarLeg rearRightLeg = new CreatureParts.CreaturePartBoar.CreaturePartBoarLeg();
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
            CreatureParts.CreaturePartBoar.CreaturePartBoarHead head = new CreatureParts.CreaturePartBoar.CreaturePartBoarHead();
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
            CreatureParts.CreaturePartBoar.CreaturePartBoarTorso torso = new CreatureParts.CreaturePartBoar.CreaturePartBoarTorso();
            torso.identifier.name = "torso";
            torso.weight = (120 * (random.Next(9, 11) / 10));
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

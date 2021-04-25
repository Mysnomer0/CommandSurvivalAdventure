using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Creatures.CreatureParts.CreaturePartHuman
{
    class CreaturePartHumanArm : CreaturePart
    {
        public CreaturePartHumanArm()
        {
            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(40, 60).ToString());
        }
    }
}

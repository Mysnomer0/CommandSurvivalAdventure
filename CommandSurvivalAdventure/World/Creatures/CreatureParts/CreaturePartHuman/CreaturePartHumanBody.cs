using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Creatures.CreatureParts.CreaturePartHuman
{
    class CreaturePartHumanBody : CreaturePart
    {
        public CreaturePartHumanBody()
        {
            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(80, 100).ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantRope : Plant
    {
        public PlantRope()
        {
            identifier.name = "rope";
            specialProperties.Add("isFastenable", "");
            type = typeof(PlantRope);
            importanceLevel = 6;

            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1, 2).ToString());
        }
    }
}

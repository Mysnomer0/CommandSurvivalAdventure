using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants.PlantParts.PlantPartFern
{
    class PlantPartFernLeaf : PlantPart
    {
        public PlantPartFernLeaf()
        {
            type = typeof(PlantPartFernLeaf);
            importanceLevel = 2;

            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", "1");
        }
    }
}

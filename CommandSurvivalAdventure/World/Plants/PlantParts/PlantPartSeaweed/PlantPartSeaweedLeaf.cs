using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants.PlantParts.PlantPartSeaweed
{
    class PlantPartSeaweedLeaf : PlantPart
    {
        public PlantPartSeaweedLeaf()
        {
            type = typeof(PlantPartSeaweedLeaf);
            importanceLevel = 1;

            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1, 2).ToString());
        }
    }
}

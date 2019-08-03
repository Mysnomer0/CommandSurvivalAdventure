using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants.PlantParts.PlantPartPalmTree
{
    class PlantPartPalmTreeLeaf : PlantPart
    {
        public PlantPartPalmTreeLeaf()
        {
            type = typeof(PlantPartPalmTreeLeaf);
            importanceLevel = 2;

            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1, 2).ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants.PlantParts.PlantPartVine
{
    class PlantPartVineLeaf : PlantPart
    {
        public PlantPartVineLeaf()
        {
            type = typeof(PlantPartVineLeaf);
            importanceLevel = 1;

            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", "1");
        }
    }
}

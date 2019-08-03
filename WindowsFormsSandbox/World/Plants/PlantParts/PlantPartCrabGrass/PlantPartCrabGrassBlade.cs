using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants.PlantParts.PlantPartCrabGrass
{
    class PlantPartCrabGrassBlade : PlantPart
    {
        public PlantPartCrabGrassBlade()
        {
            type = typeof(PlantPartCrabGrassBlade);
            importanceLevel = 1;

            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", "1");
        }
    }
}

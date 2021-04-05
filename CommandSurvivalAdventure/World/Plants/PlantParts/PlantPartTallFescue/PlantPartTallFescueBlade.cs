using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants.PlantParts.PlantPartTallFescue
{
    class PlantPartTallFescueBlade : PlantPart
    {
        public PlantPartTallFescueBlade()
        {
            type = typeof(PlantPartTallFescueBlade);
            importanceLevel = 1;

            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", "1");
        }
    }
}

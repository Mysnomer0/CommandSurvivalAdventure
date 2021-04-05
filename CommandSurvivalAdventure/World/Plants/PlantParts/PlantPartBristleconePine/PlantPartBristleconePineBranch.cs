using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants.PlantParts.PlantPartBristleconePine
{
    class PlantPartBristleconePineBranch : PlantPart
    {
        public PlantPartBristleconePineBranch()
        {
            type = typeof(PlantPartBristleconePineBranch);
            importanceLevel = 3;

            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(2, 5).ToString());
        }
    }
}

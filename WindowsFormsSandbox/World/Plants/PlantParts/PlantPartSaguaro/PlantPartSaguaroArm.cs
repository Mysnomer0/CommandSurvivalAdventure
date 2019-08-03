using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants.PlantParts.PlantPartSaguaro
{
    class PlantPartSaguaroArm : PlantPart
    {
        public PlantPartSaguaroArm()
        {
            type = typeof(PlantPartSaguaroArm);
            importanceLevel = 3;

            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(2, 10).ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants.PlantParts.PlantPartEucalyptusTree
{
    class PlantPartEucalyptusTreeBranch : PlantPart
    {
        public PlantPartEucalyptusTreeBranch()
        {
            type = typeof(PlantPartEucalyptusTreeBranch);
            importanceLevel = 3;

            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(2, 5).ToString());
        }
    }
}
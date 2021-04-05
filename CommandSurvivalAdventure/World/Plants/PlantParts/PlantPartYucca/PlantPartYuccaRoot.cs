using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants.PlantParts.PlantPartYucca
{
    class PlantPartYuccaRoot : PlantPart
    {
        public PlantPartYuccaRoot()
        {
            type = typeof(PlantPartYuccaRoot);
            importanceLevel = 3;

            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", "20");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantReed : Plant
    {
        // Initialize
        public override void Start()
        {
            base.Start();
        }
        // Update
        public override void Update()
        {
            base.Update();
        }

        public PlantReed()
        {
            // Set the type
            type = typeof(PlantReed);
            importanceLevel = 5;
            // Make a new seeded random instance for generating stats about the reed
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", "2");

        }
    }
}

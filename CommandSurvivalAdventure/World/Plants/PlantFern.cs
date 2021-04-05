using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantFern : Plant
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

        public PlantFern()
        {
            // Set the type
            type = typeof(PlantFern);
            importanceLevel = 5;
            // Make a new seeded random instance for generating stats about the fern
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1, 3).ToString());

            // the amont of leaves the fern has
            int amontOfLeaves = random.Next(12, 24);

            for(int i = 0; i < amontOfLeaves; i++)
            {
                // Create a new leaf
                PlantParts.PlantPartFern.PlantPartFernLeaf newFernLeaf = new PlantParts.PlantPartFern.PlantPartFernLeaf();
                newFernLeaf.identifier.name = "leaf";
                newFernLeaf.identifier.classifierAdjectives.Add("fern");
                AddChild(newFernLeaf);
            }
        }
    }
}

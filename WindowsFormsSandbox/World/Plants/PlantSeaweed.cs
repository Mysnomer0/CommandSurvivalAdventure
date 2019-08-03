using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantSeaweed : Plant
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

        public PlantSeaweed()
        {
            // Set the type
            type = typeof(PlantSeaweed);
            importanceLevel = 4;
            // Make a new seeded random instance for generating stats about the seaweed
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(5, 20).ToString());

            // the amont of leaves the seaweed has
            int amontOfLeaves = random.Next(2, 4);

            for (int i = 0; i < amontOfLeaves; i++)
            {
                // Create a new leaf
                PlantParts.PlantPartSeaweed.PlantPartSeaweedLeaf newPlantPart = new PlantParts.PlantPartSeaweed.PlantPartSeaweedLeaf();
                newPlantPart.identifier.name = "leaf";
                newPlantPart.identifier.classifierAdjectives.Add("seaweed");
                AddChild(newPlantPart);
            }
        }
    }
}
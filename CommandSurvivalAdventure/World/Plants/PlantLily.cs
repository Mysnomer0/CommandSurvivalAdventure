using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantLily : Plant
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

        public PlantLily()
        {
            // Set the type
            type = typeof(PlantLily);
            importanceLevel = 4;
            // Make a new seeded random instance for generating stats about the lily
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1, 2).ToString());

            // the amont of leaves the lily has
            int amontOfLeaves = random.Next(1, 4);

            for (int i = 0; i < amontOfLeaves; i++)
            {
                // Create a new leaf
                PlantParts.PlantPartLily.PlantPartLilyLeaf newPlantPart = new PlantParts.PlantPartLily.PlantPartLilyLeaf();
                newPlantPart.identifier.name = "leaf";
                newPlantPart.identifier.classifierAdjectives.Add("lily");
                AddChild(newPlantPart);
            }
        }
    }
}
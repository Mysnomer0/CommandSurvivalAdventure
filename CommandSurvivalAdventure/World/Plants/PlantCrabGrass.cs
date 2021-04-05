using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantCrabGrass : Plant
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

        public PlantCrabGrass()
        {
            // Set the type
            type = typeof(PlantCrabGrass);
            importanceLevel = 3;
            // Make a new seeded random instance for generating stats about the CrabGrass
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1, 2).ToString());

            // the amont of leaves the Crab Grass has
            int amontOfLeaves = random.Next(5, 10);

            for (int i = 0; i < amontOfLeaves; i++)
            {
                // Create a new leaf
                PlantParts.PlantPartCrabGrass.PlantPartCrabGrassBlade newCrabGrassBlade = new PlantParts.PlantPartCrabGrass.PlantPartCrabGrassBlade();
                newCrabGrassBlade.identifier.name = "blade";
                newCrabGrassBlade.identifier.classifierAdjectives.Add("crab");
                newCrabGrassBlade.identifier.classifierAdjectives.Add("grass");
                AddChild(newCrabGrassBlade);
            }
        }
    }
}

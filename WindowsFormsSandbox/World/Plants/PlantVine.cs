using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantVine : Plant
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

        public PlantVine()
        {
            // Set the type
            type = typeof(PlantVine);
            importanceLevel = 4;
            // Make a new seeded random instance for generating stats about the vine
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(2, 5).ToString());

            // the amont of rhizomes the cattail has
            int amontOfLeafs = random.Next(0, 2);

            for (int i = 0; i < amontOfLeafs; i++)
            {
                // Create a new leafs
                PlantParts.PlantPartVine.PlantPartVineLeaf newVineLeaf = new PlantParts.PlantPartVine.PlantPartVineLeaf();
                newVineLeaf.identifier.name = "leaf";
                newVineLeaf.identifier.classifierAdjectives.Add("vine");
                AddChild(newVineLeaf);
            }
        }
    }
}

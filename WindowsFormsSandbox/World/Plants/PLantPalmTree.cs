using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantPalmTree : Plant
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

        public PlantPalmTree()
        {
            // Set the type
            type = typeof(PlantPalmTree);
            importanceLevel = 6;
            // Make a new seeded random instance for generating stats about the Palm Tree
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(2000, 5000).ToString());

            // the amont of leafs the Acacia Tree has
            int amontOfLeafs = random.Next(4, 6);

            for (int i = 0; i < amontOfLeafs; i++)
            {
                // Create a new Branch
                PlantParts.PlantPartPalmTree.PlantPartPalmTreeLeaf newPalmLeaf = new PlantParts.PlantPartPalmTree.PlantPartPalmTreeLeaf();
                newPalmLeaf.identifier.name = "leaf";
                newPalmLeaf.identifier.classifierAdjectives.Add("palm");
                AddChild(newPalmLeaf);
            }
        }
    }
}

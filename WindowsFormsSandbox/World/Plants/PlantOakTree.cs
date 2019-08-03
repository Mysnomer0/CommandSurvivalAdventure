using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantOakTree : Plant
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

        public PlantOakTree()
        {
            // Set the type
            type = typeof(PlantOakTree);
            importanceLevel = 6;
            // Make a new seeded random instance for generating stats about the oak Tree
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(2000, 5000).ToString());

            // the amont of Branches the oak Tree has
            int amontOfBranches = random.Next(20, 50);

            for (int i = 0; i < amontOfBranches; i++)
            {
                // Create a new Branch
                PlantParts.PlantPartOakTree.PlantPartOakTreeBranch newOakBranch = new PlantParts.PlantPartOakTree.PlantPartOakTreeBranch();
                newOakBranch.identifier.name = "branch";
                newOakBranch.identifier.classifierAdjectives.Add("oak");
                AddChild(newOakBranch);
            }

            // the amont of leafs the oak Tree has
            int amontOfLeafs = random.Next(20, 50);

            for (int i = 0; i < amontOfLeafs; i++)
            {
                // Create a new leaf
                PlantParts.PlantPartOakTree.PlantPartOakTreeLeaf newOakLeaf = new PlantParts.PlantPartOakTree.PlantPartOakTreeLeaf();
                newOakLeaf.identifier.name = "leaf";
                newOakLeaf.identifier.classifierAdjectives.Add("oak");
                AddChild(newOakLeaf);
            }
        }
    }
}

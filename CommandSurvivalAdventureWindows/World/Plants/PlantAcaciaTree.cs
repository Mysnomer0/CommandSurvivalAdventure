using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantAcaciaTree : Plant
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

        public PlantAcaciaTree()
        {
            // Set the type
            type = typeof(PlantAcaciaTree);
            importanceLevel = 6;
            // Make a new seeded random instance for generating stats about the Acacia Tree
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(2000, 5000).ToString());

            // the amont of Branches the Acacia Tree has
            int amontOfBranches = random.Next(20, 50);

            for (int i = 0; i < amontOfBranches; i++)
            {
                // Create a new Branch
                PlantParts.PlantPartAcaciaTree.PlantPartAcaciaTreeBranch newAcaciaTreeBranch = new PlantParts.PlantPartAcaciaTree.PlantPartAcaciaTreeBranch();
                newAcaciaTreeBranch.identifier.name = "branch";
                // Make it eather long or short
                if (random.Next(0, 1) == 0)
                {
                    newAcaciaTreeBranch.identifier.descriptiveAdjectives.Add("long");
                    newAcaciaTreeBranch.specialProperties["weight"] = random.Next(5, 10).ToString();
                }
                else
                {
                    newAcaciaTreeBranch.identifier.descriptiveAdjectives.Add("short");
                    newAcaciaTreeBranch.specialProperties["weight"] = random.Next(2, 5).ToString();
                }
                newAcaciaTreeBranch.identifier.classifierAdjectives.Add("acacia");
                AddChild(newAcaciaTreeBranch);
            }

            // the amont of leafs the Acacia Tree has
            int amontOfLeafs = random.Next(20, 50);

            for (int i = 0; i < amontOfLeafs; i++)
            {
                // Create a new leaf
                PlantParts.PlantPartAcaciaTree.PlantPartAcaciaTreeLeaf newAcaciaLeaf = new PlantParts.PlantPartAcaciaTree.PlantPartAcaciaTreeLeaf();
                newAcaciaLeaf.identifier.name = "leaf";
                newAcaciaLeaf.identifier.classifierAdjectives.Add("acacia");
                AddChild(newAcaciaLeaf);
            }
        }
    }
}

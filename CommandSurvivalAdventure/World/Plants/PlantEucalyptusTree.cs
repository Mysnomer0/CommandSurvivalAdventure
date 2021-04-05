using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantEucalyptusTree : Plant
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

        public PlantEucalyptusTree()
        {
            // Set the type
            type = typeof(PlantEucalyptusTree);
            importanceLevel = 6;
            // Make a new seeded random instance for generating stats about the Eucalyptus Tree
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(500, 1000).ToString());

            // the amont of Branches the eucalyptus Tree has
            int amontOfBranches = random.Next(3, 10);

            for (int i = 0; i < amontOfBranches; i++)
            {
                // Create a new Branch
                PlantParts.PlantPartEucalyptusTree.PlantPartEucalyptusTreeBranch newEucalyptusBranch = new PlantParts.PlantPartEucalyptusTree.PlantPartEucalyptusTreeBranch();
                newEucalyptusBranch.identifier.name = "branch";
                newEucalyptusBranch.identifier.classifierAdjectives.Add("eucalyptus");
                AddChild(newEucalyptusBranch);
            }

            // the amont of leafs the Eucalyptus Tree has
            int amontOfLeafs = random.Next(20, 50);

            for (int i = 0; i < amontOfLeafs; i++)
            {
                // Create a new leaf
                PlantParts.PlantPartEucalyptusTree.PlantPartEucalyptusTreeLeaf newEucalyptusLeaf = new PlantParts.PlantPartEucalyptusTree.PlantPartEucalyptusTreeLeaf();
                newEucalyptusLeaf.identifier.name = "leaf";
                newEucalyptusLeaf.identifier.classifierAdjectives.Add("eucalyptus");
                AddChild(newEucalyptusLeaf);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantRubberTree : Plant
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

        public PlantRubberTree()
        {
            // Set the type
            type = typeof(PlantRubberTree);
            importanceLevel = 6;
            // Make a new seeded random instance for generating stats about the oak Tree
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1000, 5000).ToString());

            // the amont of Branches the oak Tree has
            int amontOfBranches = random.Next(20, 50);

            for (int i = 0; i < amontOfBranches; i++)
            {
                // Create a new Branch
                PlantParts.PlantPartRubberTree.PlantPartRubberTreeBranch newRubberTreeBranch = new PlantParts.PlantPartRubberTree.PlantPartRubberTreeBranch();
                newRubberTreeBranch.identifier.name = "branch";
                newRubberTreeBranch.identifier.classifierAdjectives.Add("rubber tree");
                AddChild(newRubberTreeBranch);

                // the amont of leafs the oak branch has
                int amontOfLeafs = random.Next(5, 20);

                for (int j = 0; j < amontOfLeafs; j++)
                {
                    // Create a new leaf
                    PlantParts.PlantPartRubberTree.PlantPartRubberTreeLeaf newRubberTreeLeaf = new PlantParts.PlantPartRubberTree.PlantPartRubberTreeLeaf();
                    newRubberTreeLeaf.identifier.name = "leaf";
                    newRubberTreeLeaf.identifier.classifierAdjectives.Add("rubber tree");
                    newRubberTreeBranch.AddChild(newRubberTreeLeaf);
                }
            }

            int amontOfVines = random.Next(0, 3);

            for (int i = 0; i < amontOfVines; i++)
            {
                // Create a new Vine
                PlantVine newVine = new PlantVine();
                newVine.identifier.name = "vine";
                AddChild(newVine);
            }
        }
    }
}

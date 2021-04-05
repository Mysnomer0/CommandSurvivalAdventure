using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantBristleconePine : Plant
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

        public PlantBristleconePine()
        {
            // Set the type
            type = typeof(PlantBristleconePine);
            importanceLevel = 7;

            // Make a new seeded random instance for generating stats about the Bristlecone Pine
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1000, 3000).ToString());

            // the amont of Branches the Bristlecone Pine has
            int amontOfBranches = random.Next(5, 20);

            for (int i = 0; i < amontOfBranches; i++)
            {
                // Create a new Branch
                PlantParts.PlantPartBristleconePine.PlantPartBristleconePineBranch newBristleconePineBranch = new PlantParts.PlantPartBristleconePine.PlantPartBristleconePineBranch();
                newBristleconePineBranch.identifier.name = "branch";
                newBristleconePineBranch.identifier.classifierAdjectives.Add("bristlecone");
                newBristleconePineBranch.identifier.classifierAdjectives.Add("pine");
                AddChild(newBristleconePineBranch);
            }

            // the amont of leafs the Bristlecone Pine has
            int amontOfLeafs = random.Next(100, 200);

            for (int i = 0; i < amontOfLeafs; i++)
            {
                // Create a new leaf
                PlantParts.PlantPartBristleconePine.PlantPartBristleconePineLeaf newBristleconeLeaf = new PlantParts.PlantPartBristleconePine.PlantPartBristleconePineLeaf();
                newBristleconeLeaf.identifier.name = "leaf";
                newBristleconeLeaf.identifier.classifierAdjectives.Add("bristlecone");
                newBristleconeLeaf.identifier.classifierAdjectives.Add("pine");
                AddChild(newBristleconeLeaf);
            }
        }
    }
}

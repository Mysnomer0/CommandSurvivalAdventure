using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantYucca : Plant
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

        public PlantYucca()
        {
            // Set the type
            type = typeof(PlantYucca);
            importanceLevel = 6;
            // Make a new seeded random instance for generating stats about the Yucca
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(100, 300).ToString());

            // the amont of roots the Yucca plant has
            int amountOfRoots = random.Next(1, 5);

            for (int i = 0; i < amountOfRoots; i++)
            {
                // Create a new Branch
                PlantParts.PlantPartYucca.PlantPartYuccaRoot newAcaciaYuccaRoot = new PlantParts.PlantPartYucca.PlantPartYuccaRoot();
                newAcaciaYuccaRoot.identifier.name = "root";
                newAcaciaYuccaRoot.identifier.classifierAdjectives.Add("yucca");
                AddChild(newAcaciaYuccaRoot);
            }

            // the amont of leafs the Yucca has
            int amontOfLeafs = random.Next(20, 50);

            for (int i = 0; i < amontOfLeafs; i++)
            {
                // Create a new leaf
                PlantParts.PlantPartYucca.PlantPartYuccaLeaf newYuccaLeaf = new PlantParts.PlantPartYucca.PlantPartYuccaLeaf();
                newYuccaLeaf.identifier.name = "leaf";
                newYuccaLeaf.identifier.classifierAdjectives.Add("yucca");
                AddChild(newYuccaLeaf);
            }
        }
    }
}

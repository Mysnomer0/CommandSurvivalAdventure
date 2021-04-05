using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeJungle : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "jungle";
            normalTemperature = 90;
            normalWindSpeed = 5;
            associatedColor = "$ca";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add plants
            // the amont of rubber trees
            int amontOfRubberTrees = random.Next(5, 15);

            for (int i = 0; i < amontOfRubberTrees; i++)
            {
                // Create a new rubber tree
                Plants.PlantRubberTree newRubberTree = new Plants.PlantRubberTree();
                newRubberTree.identifier.name = "tree";

                // Make it eather short or tall
                if (random.Next(0, 1) == 0)
                    newRubberTree.identifier.descriptiveAdjectives.Add("short");
                else
                    newRubberTree.identifier.descriptiveAdjectives.Add("tall");

                newRubberTree.identifier.classifierAdjectives.Add("rubber");
                chunkToPopulate.AddChild(newRubberTree);
            }

            // the amont of CrabGrass
            int amontOfCrabGrass = random.Next(100, 200);

            for (int i = 0; i < amontOfCrabGrass; i++)
            {
                // Create a new CrabGrass
                Plants.PlantCrabGrass newCrabGrass = new Plants.PlantCrabGrass();
                newCrabGrass.identifier.name = "grass";
                newCrabGrass.identifier.classifierAdjectives.Add("crab");
                chunkToPopulate.AddChild(newCrabGrass);
            }

            // Mkae some vines
            int amountOfVines = random.Next(2, 5);

            for (int i = 0; i < amountOfVines; i++)
            {
                // Create a new thing of silt
                Plants.PlantVine newVine = new Plants.PlantVine();
                newVine.identifier.name = "vine";
                chunkToPopulate.AddChild(newVine);
            }
            #endregion

            #region Add minerals
            // Decide whether to generate Silt
            if (random.Next(1, 3) == 1)
            {
                // the amont of Silt
                int amontOfSilt = random.Next(20, 50);

                for (int i = 0; i < amontOfSilt; i++)
                {
                    // Create a new thing of silt
                    Minerals.MineralSilt newSilt = new Minerals.MineralSilt();
                    newSilt.identifier.name = "silt";
                    chunkToPopulate.AddChild(newSilt);
                }
            }

            // Decide whether to generate rocks
            if (random.Next(1, 5) == 1)
            {
                // the amont of rocks
                int amontOfRocks = random.Next(1, 3);

                for (int i = 0; i < amontOfRocks; i++)
                {
                    // Create a new piece of rock
                    Minerals.MineralBasaltRock newRock = new Minerals.MineralBasaltRock();
                    chunkToPopulate.AddChild(newRock);
                }
            }
            #endregion

            
        }
    }
}

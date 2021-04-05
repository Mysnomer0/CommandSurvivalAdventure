using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeBeach : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "beach";
            normalTemperature = 70;
            normalWindSpeed = 40;
            associatedColor = "$oa";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add minerals
            // the amont of sand
            int amontOfSand = random.Next(100, 200);

            for (int i = 0; i < amontOfSand; i++)
            {
                // Create a new piece of sand
                Minerals.MineralSand newIronOre = new Minerals.MineralSand();
                newIronOre.identifier.name = "sand";
                newIronOre.identifier.isPluralAgnostic = true;
                chunkToPopulate.AddChild(newIronOre);
            }

            // Decide whether to generate flint
            if (random.Next(1, 8) == 1)
            {
                // the amont of flint
                int amontOfFlint = random.Next(1, 3);

                for (int i = 0; i < amontOfFlint; i++)
                {
                    // Create a new piece of boulder
                    Minerals.MineralFlint newFlint = new Minerals.MineralFlint();
                    chunkToPopulate.AddChild(newFlint);
                }
            }

            // Decide whether to generate rocks
            if (random.Next(1, 5) == 1)
            {
                // the amont of rocks
                int amontOfRocks = random.Next(1, 5);

                for (int i = 0; i < amontOfRocks; i++)
                {
                    // Create a new piece of rock
                    Minerals.MineralBasaltRock newRock = new Minerals.MineralBasaltRock();
                    chunkToPopulate.AddChild(newRock);
                }
            }

            // Decide whether to generate a shell
            if (random.Next(1, 100) == 1)
            {
                // Create a new shell
                Minerals.MineralMoonShell newMoonShell = new Minerals.MineralMoonShell();
                chunkToPopulate.AddChild(newMoonShell);
            }

            // Decide whether to generate a shell
            if (random.Next(1, 10) == 1)
            {
                // Create a new shell
                Minerals.MineralSandDollar newDollar = new Minerals.MineralSandDollar();
                chunkToPopulate.AddChild(newDollar);
            }

            // Decide whether to generate a shell
            if (random.Next(1, 10) == 1)
            {
                // Create a new shell
                Minerals.MineralCalicoScallopShell newScallopShell = new Minerals.MineralCalicoScallopShell();
                chunkToPopulate.AddChild(newScallopShell);
            }
            #endregion

            #region Add plants
            // the amont of palm trees
            int amontOfPalmTrees = random.Next(0, 3);

            for (int i = 0; i < amontOfPalmTrees; i++)
            {
                // Create a new Palm Trees
                Plants.PlantPalmTree newPalmTree = new Plants.PlantPalmTree();
                newPalmTree.identifier.name = "tree";

                // Make it eather short or tall
                if (random.Next(0, 1) == 0)
                    newPalmTree.identifier.classifierAdjectives.Add("short");
                else
                    newPalmTree.identifier.classifierAdjectives.Add("tall");

                newPalmTree.identifier.classifierAdjectives.Add("palm");
                chunkToPopulate.AddChild(newPalmTree);
            }

            // Decide whether to generate Seaweed
            if (random.Next(1, 5) == 1)
            {
                // the amont of seaweed
                int amontOfSeaweed = random.Next(1, 3);

                for (int i = 0; i < amontOfSeaweed; i++)
                {
                    // Create a new seaweed
                    Plants.PlantSeaweed newSeaweed = new Plants.PlantSeaweed();
                    newSeaweed.identifier.name = "seaweed";
                    chunkToPopulate.AddChild(newSeaweed);
                }
            }
            #endregion
        }
    }
}

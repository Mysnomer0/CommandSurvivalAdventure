using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeMountains : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "mountain";
            normalTemperature = 25;
            normalWindSpeed = 30;
            associatedColor = "$pa";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add minerals
            // the amont of gravel
            int amontOfGravel = random.Next(100, 200);

            for (int i = 0; i < amontOfGravel; i++)
            {
                // Create a new piece of sand
                Minerals.MineralGravel newGravel = new Minerals.MineralGravel();
                chunkToPopulate.AddChild(newGravel);
            }

            // Decide whether to generate boulder
            if (random.Next(1, 2) == 1)
            {
                // the amont of boulder
                int amontOfBoulders = random.Next(1, 5);

                for (int i = 0; i < amontOfBoulders; i++)
                {
                    // Create a new boulder
                    Minerals.MineralBasaltBoulder newBoulder = new Minerals.MineralBasaltBoulder();
                    chunkToPopulate.AddChild(newBoulder);
                }
            }

            // Decide whether to generate flint
            if (random.Next(1, 2) == 1)
            {
                // the amont of flint
                int amontOfFlint = random.Next(1, 5);

                for (int i = 0; i < amontOfFlint; i++)
                {
                    // Create a new piece of boulder
                    Minerals.MineralFlint newFlint = new Minerals.MineralFlint();
                    chunkToPopulate.AddChild(newFlint);
                }
            }

            // the amont of rocks
            int amontOfRocks = random.Next(1, 5);

            for (int i = 0; i < amontOfRocks; i++)
            {
                // Create a new piece of boulder
                Minerals.MineralBasaltRock newRock = new Minerals.MineralBasaltRock();
                chunkToPopulate.AddChild(newRock);
            }

            // Decide whether to generate Lead
            if (random.Next(1, 3) == 1)
            {
                // the amont of Lead
                int amontOfLead = random.Next(1, 4);

                for (int i = 0; i < amontOfLead; i++)
                {
                    // Create a new piece of Lead
                    Minerals.MineralLead newLead = new Minerals.MineralLead();
                    chunkToPopulate.AddChild(newLead);
                }
            }
            // the amont of copper
            int amontOfCopper = random.Next(0, 5);

            for (int i = 0; i < amontOfCopper; i++)
            {
                // Create a new piece of copper
                Minerals.MineralCopper newCopper = new Minerals.MineralCopper();
                chunkToPopulate.AddChild(newCopper);
            }

            // Decide whether to generate a shell
            if (random.Next(1, 10) == 1)
            {
                // Create a new shell
                Minerals.MineralCalicoScallopShell newScallopShell = new Minerals.MineralCalicoScallopShell();
                chunkToPopulate.AddChild(newScallopShell);
            }
            #endregion
        }
    }
}

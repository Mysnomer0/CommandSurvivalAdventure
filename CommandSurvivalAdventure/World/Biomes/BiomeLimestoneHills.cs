using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeLimestoneHills : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "limestone hills";
            normalTemperature = 75;
            normalWindSpeed = 25;
            associatedColor = "$ga";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add minerals
            // Decide whether to generate boulder
            if (random.Next(1, 2) == 1)
            {
                // the amont of boulder
                int amontOfBoulders = random.Next(1, 5);

                for (int i = 0; i < amontOfBoulders; i++)
                {
                    // Create a new boulder
                    Minerals.MineralLimestoneBoulder newBoulder = new Minerals.MineralLimestoneBoulder();
                    chunkToPopulate.AddChild(newBoulder);
                }
            }

            // Decide whether to generate rocks
            if (random.Next(1, 2) == 1)
            {
                // the amont of rocks
                int amontOfRocks = random.Next(3, 8);

                for (int i = 0; i < amontOfRocks; i++)
                {
                    // Create a new piece of rock
                    Minerals.MineralLimestoneRock newRock = new Minerals.MineralLimestoneRock();
                    chunkToPopulate.AddChild(newRock);
                }
            }
            #endregion
        }
    }
}

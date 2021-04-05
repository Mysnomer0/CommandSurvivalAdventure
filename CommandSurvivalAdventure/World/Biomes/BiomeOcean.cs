using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeOcean : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "ocean";
            normalTemperature = 28;
            normalWindSpeed = 60;
            associatedColor = "$ba";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add plants
            // Decide whether to generate Seaweed
            if (random.Next(1, 3) == 1)
            {
                // the amont of seaweed
                int amontOfSeaweed = random.Next(1, 5);

                for (int i = 0; i < amontOfSeaweed; i++)
                {
                    // Create a new seaweed
                    Plants.PlantSeaweed newSeaweed = new Plants.PlantSeaweed();
                    newSeaweed.identifier.name = "seaweed";
                    chunkToPopulate.AddChild(newSeaweed);
                }
            }
            #endregion

            #region Add minerals
            // the amont of sand
            int amontOfSand = random.Next(100, 200);

            for (int i = 0; i < amontOfSand; i++)
            {
                // Create a new piece of sand
                Minerals.MineralSand newSand = new Minerals.MineralSand();
                newSand.identifier.name = "sand";
                newSand.identifier.isPluralAgnostic = true;
                chunkToPopulate.AddChild(newSand);
            }
            #endregion
        }
    }
}

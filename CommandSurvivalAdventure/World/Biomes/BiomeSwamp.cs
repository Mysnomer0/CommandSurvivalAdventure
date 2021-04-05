using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeSwamp : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "swamp";
            normalTemperature = 75;
            normalWindSpeed = 10;
            associatedColor = "$ga";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add plants

            // Decide whether to generate cattails
            if (random.Next(1, 3) == 1)
            {
                // the amont of cattails
                int amontOfCattails = random.Next(15, 30);

                for (int i = 0; i < amontOfCattails; i++)
                {
                    // Create a new Cattail
                    Plants.PlantCattail newCattail = new Plants.PlantCattail();
                    newCattail.identifier.name = "cattail";
                    chunkToPopulate.AddChild(newCattail);
                }
            }

            // Decide whether to generate reeds
            if (random.Next(1, 3) == 1)
            {
                // the amont of reeds
                int amontOfReeds = random.Next(15, 30);

                for (int i = 0; i < amontOfReeds; i++)
                {
                    // Create a new reed
                    Plants.PlantReed newReed = new Plants.PlantReed();
                    newReed.identifier.name = "reed";
                    chunkToPopulate.AddChild(newReed);
                }
            }
        
            // Decide whether to generate lilys
            if (random.Next(1, 3) == 1)
            {
                // the amont of lilys
                int amontOfLilys = random.Next(3, 8);

                for (int i = 0; i < amontOfLilys; i++)
                {
                    // Create a new Lilys
                    Plants.PlantLily newLily = new Plants.PlantLily();
                    newLily.identifier.name = "lily";
                    chunkToPopulate.AddChild(newLily);
                }
            }
            #endregion

            #region Add minerals
            // the amont of Silt
            int amontOfSilt = random.Next(20, 50);

            for (int i = 0; i < amontOfSilt; i++)
            {
                // Create a new thing of silt
                Minerals.MineralSilt newSilt = new Minerals.MineralSilt();
                newSilt.identifier.name = "silt";
                chunkToPopulate.AddChild(newSilt);
            }

            // Decide whether to generate boulder
            if (random.Next(1, 5) == 1)
            {
                // the amont of boulder
                int amontOfBoulders = random.Next(1, 2);

                for (int i = 0; i < amontOfBoulders; i++)
                {
                    // Create a new boulder
                    Minerals.MineralBasaltBoulder newBoulder = new Minerals.MineralBasaltBoulder();
                    chunkToPopulate.AddChild(newBoulder);
                }
            }
            #endregion
        }
    }
}

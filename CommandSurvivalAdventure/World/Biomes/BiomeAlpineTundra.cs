using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeAlpineTundra : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "alpine tundra";
            normalTemperature = 40;
            normalWindSpeed = 50;
            associatedColor = "$la";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add plants
            // the amont of Bristlecone Pines
            int amontOfBristleconePine = random.Next(0, 5);

            for (int i = 0; i < amontOfBristleconePine; i++)
            {
                // Create a new Bristlecone Pines
                Plants.PlantBristleconePine newBristleconePine = new Plants.PlantBristleconePine();
                newBristleconePine.identifier.name = "pine";

                // Make it eather short or tall
                if (random.Next(0, 1) == 0)
                    newBristleconePine.identifier.descriptiveAdjectives.Add("short");
                else
                    newBristleconePine.identifier.descriptiveAdjectives.Add("tall");

                newBristleconePine.identifier.classifierAdjectives.Add("bristlecone");
                chunkToPopulate.AddChild(newBristleconePine);
            }
            #endregion
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeGlacier : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "glacier";
            normalTemperature = 10;
            normalWindSpeed = 30;
            associatedColor = "$la";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add minerals
            // the amont of ice
            int amountOfIce = random.Next(100, 200);

            for (int i = 0; i < amountOfIce; i++)
            {
                // Create a new thing of Ice
                Minerals.MineralIce newIce = new Minerals.MineralIce();
                newIce.identifier.name = "ice";
                chunkToPopulate.AddChild(newIce);
            }

            // the amont of gravel
            int amontOfGravel = random.Next(0, 20);

            for (int i = 0; i < amontOfGravel; i++)
            {
                // Create a new piece of gravel
                Minerals.MineralGravel newGravel = new Minerals.MineralGravel();
                newGravel.identifier.name = "gravel";
                chunkToPopulate.AddChild(newGravel);
            }
            #endregion
        }
    }
}

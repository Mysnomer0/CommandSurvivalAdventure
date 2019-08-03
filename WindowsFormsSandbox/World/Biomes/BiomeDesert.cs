using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeDesert : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "desert";
            normalTemperature = 110;
            normalWindSpeed = 20;
            associatedColor = "$oa";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add plants
            // Decide whether to generate Saguaros
            if (random.Next(1, 3) == 1)
            {
                // the amont of Saguaros
                int amontOfSaguaro = random.Next(1, 4);

                for (int i = 0; i < amontOfSaguaro; i++)
                {
                    // Create a new Saguaro
                    Plants.PlantSaguaro newSaguaro = new Plants.PlantSaguaro();
                    newSaguaro.identifier.name = "saguaro";
                    chunkToPopulate.AddChild(newSaguaro);
                }
            }

            // Decide whether to generate yucca
            if (random.Next(1, 6) == 1)
            {
                // the amont of Saguaros
                int amontOfYucca = random.Next(1, 4);

                for (int i = 0; i < amontOfYucca; i++)
                {
                    // Create a new yucca
                    Plants.PlantYucca newYucca = new Plants.PlantYucca();
                    newYucca.identifier.name = "yucca";
                    chunkToPopulate.AddChild(newYucca);
                }
            }
            #endregion

            #region Add minerals
            // Decide whether to generate iron
            if (random.Next(1, 5) == 1)
            {
                // the amont of iron
                int amontOfIron = random.Next(1, 4);

                for (int i = 0; i < amontOfIron; i++)
                {
                    // Create a new piece of iron
                    Minerals.MineralIronOre newIronOre = new Minerals.MineralIronOre();
                    chunkToPopulate.AddChild(newIronOre);
                }
            }

            // Decide whether to generate boulder
            if (random.Next(1, 5) == 1)
            {
                // the amont of boulder
                int amontOfBoulders = random.Next(1, 2);

                for (int i = 0; i < amontOfBoulders; i++)
                {
                    // Create a new piece of boulder
                    Minerals.MineralBasaltBoulder newBoulder = new Minerals.MineralBasaltBoulder();
                    newBoulder.identifier.descriptiveAdjectives.Clear();
                    newBoulder.identifier.descriptiveAdjectives.Add("large");
                    chunkToPopulate.AddChild(newBoulder);
                }
            }

            // the amont of sand
            int amontOfSand = random.Next(100, 200);

            for (int i = 0; i < amontOfSand; i++)
            {
                // Create a new piece of sand
                Minerals.MineralSand newSand = new Minerals.MineralSand();
                chunkToPopulate.AddChild(newSand);
            }
            #endregion
        }
    }
}

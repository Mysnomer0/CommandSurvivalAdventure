using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeGrassland : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "grassland";
            normalTemperature = 60;
            normalWindSpeed = 30;
            associatedColor = "$ka";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add plants
            // the amont of Tall Fescues
            int amontOfTallFescues = random.Next(100, 200);

            for (int i = 0; i < amontOfTallFescues; i++)
            {
                // Create a new Tall Fescue
                Plants.PlantTallFescue newTallFescue = new Plants.PlantTallFescue();
                newTallFescue.identifier.name = "grass";
                newTallFescue.identifier.descriptiveAdjectives.Add("lush");
                newTallFescue.identifier.descriptiveAdjectives.Add("tall");
                newTallFescue.identifier.classifierAdjectives.Add("fescue");
                chunkToPopulate.AddChild(newTallFescue);
            }

            // the amont of CrabGrass
            int amontOfCrabGrass = random.Next(10, 50);

            for (int i = 0; i < amontOfCrabGrass; i++)
            {
                // Create a new CrabGrass
                Plants.PlantCrabGrass newCrabGrass = new Plants.PlantCrabGrass();
                newCrabGrass.identifier.name = "grass";
                newCrabGrass.identifier.classifierAdjectives.Add("crab");
                chunkToPopulate.AddChild(newCrabGrass);
            }
            #endregion
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeBog : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "bog";
            normalTemperature = 30;
            normalWindSpeed = 15;
            associatedColor = "$ga";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add plants
            // the amont of Tall Fescues
            int amontOfTallFescues = random.Next(50, 100);

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
            #endregion
        }
    }
}

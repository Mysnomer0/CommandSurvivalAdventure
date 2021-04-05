using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeChaparral : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "chaparral";
            normalTemperature = 70;
            normalWindSpeed = 25;
            associatedColor = "$ga";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add minerals
            // Decide whether to generate boulder
            if (random.Next(1, 3) == 1)
            {
                // the amont of boulder
                int amontOfBoulders = random.Next(1, 3);

                for (int i = 0; i < amontOfBoulders; i++)
                {
                    // Create a new piece of boulder
                    Minerals.MineralBasaltBoulder newBoulder = new Minerals.MineralBasaltBoulder();
                    chunkToPopulate.AddChild(newBoulder);
                }
            }
            #endregion

            #region Add plants
            // the amont of eucalyptus trees
            int amontOfEucalyptusTrees = random.Next(5, 20);

            for (int i = 0; i < amontOfEucalyptusTrees; i++)
            {
                // Create a new Eucalyptus Trees
                Plants.PlantEucalyptusTree newEucalyptusTree = new Plants.PlantEucalyptusTree();
                newEucalyptusTree.identifier.name = "tree";

                // Make it eather short or tall
                if (random.Next(0, 1) == 0)
                    newEucalyptusTree.identifier.descriptiveAdjectives.Add("short");
                else
                    newEucalyptusTree.identifier.descriptiveAdjectives.Add("tall");

                newEucalyptusTree.identifier.classifierAdjectives.Add("eucalyptus");
                chunkToPopulate.AddChild(newEucalyptusTree);
            }
            #endregion
        }
    }
}

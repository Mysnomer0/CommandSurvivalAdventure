using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeCanyon : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "canyon";
            normalTemperature = 85;
            normalWindSpeed = 20;
            associatedColor = "$ea";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            if (random.Next(0, 10) == 0)
            {
                Creatures.CreatureGoat newGoat = new Creatures.CreatureGoat(chunkToPopulate.attachedApplication);
                newGoat.identifier.descriptiveAdjectives.Add("wild");
                newGoat.identifier.classifierAdjectives.Add("bearded");
                chunkToPopulate.AddChild(newGoat);
            }

            #region Add minerals
            // Decide whether to generate boulder
            if (random.Next(1, 2) == 1)
            {
                // the amont of boulder
                int amontOfBoulders = random.Next(1, 5);

                for (int i = 0; i < amontOfBoulders; i++)
                {
                    // Create a new piece of boulder
                    Minerals.MineralBasaltBoulder newBoulder = new Minerals.MineralBasaltBoulder();
                    chunkToPopulate.AddChild(newBoulder);
                }
            }
            // Decide whether to generate copper
            if (random.Next(1, 3) == 1)
            {
                // the amont of copper
                int amontOfCopper = random.Next(3, 10);

                for (int i = 0; i < amontOfCopper; i++)
                {
                    // Create a new piece of copper
                    Minerals.MineralCopper newCopper = new Minerals.MineralCopper();
                    chunkToPopulate.AddChild(newCopper);
                }
            }
            // Decide whether to generate gold
            if (random.Next(1, 5) == 1)
            {
                // the amont of gold
                int amontOfGold = random.Next(2, 5);

                for (int i = 0; i < amontOfGold; i++)
                {
                    // Create a new piece of gold
                    Minerals.MineralGold newGold = new Minerals.MineralGold();
                    chunkToPopulate.AddChild(newGold);
                }
            }
            #endregion
        }
    }
}

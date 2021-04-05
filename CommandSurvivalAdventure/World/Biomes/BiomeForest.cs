using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeForest : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "forest";
            normalTemperature = 50;
            normalWindSpeed = 12;
            associatedColor = "$ca";
            // Create a new random generator
            Random random = new Random();
            // Generate the chunks properties
            chunkToPopulate.windSpeed = (normalWindSpeed * (random.Next(2, 8) / 5));
            chunkToPopulate.temperature = (normalTemperature * (random.Next(2, 4) / 3));

            #region Add minerals
            // The amount of earth
            int amontOfEarth = random.Next(100, 200);

            for (int i = 0; i < amontOfEarth; i++)
            {
                // Create a new piece of Earth
                Minerals.MineralEarth newEarth = new Minerals.MineralEarth();
                newEarth.identifier.name = "earth";
                chunkToPopulate.AddChild(newEarth);
            }

            // Decide whether to generate boulder
            if (random.Next(1, 3) == 1)
            {
                // the amont of boulder
                int amontOfBoulders = random.Next(1, 2);

                for (int i = 0; i < amontOfBoulders; i++)
                {
                    // Create a new piece of boulder
                    Minerals.MineralBasaltBoulder newBoulder = new Minerals.MineralBasaltBoulder();
                    chunkToPopulate.AddChild(newBoulder);
                }
            }
            #endregion

            #region Add plants to biome
            // the amont of oak trees
            int amontOfOakTrees = random.Next(5, 20);

            for (int i = 0; i < amontOfOakTrees; i++)
            {
                // Create a new Oak Trees
                Plants.PlantOakTree newOakTree = new Plants.PlantOakTree();
                newOakTree.identifier.name = "tree";

                // Make it eather short or tall
                if (random.Next(0, 1) == 0)
                    newOakTree.identifier.descriptiveAdjectives.Add("short");
                else
                    newOakTree.identifier.descriptiveAdjectives.Add("tall");

                newOakTree.identifier.classifierAdjectives.Add("oak");
                chunkToPopulate.AddChild(newOakTree);
            }

            // Decide whether to generate ferns
            if (random.Next(1, 3) == 1)
            {
                // the amont of ferns
                int amontOfFerns = random.Next(12, 24);

                for (int i = 0; i < amontOfFerns; i++)
                {
                    // Create a new fern
                    Plants.PlantFern newFern = new Plants.PlantFern();
                    newFern.identifier.name = "fern";
                    chunkToPopulate.AddChild(newFern);
                }
            }
            // the amont of Tall Fescues
            int amontOfTallFescues = random.Next(100, 200);

            for (int i = 0; i < amontOfTallFescues; i++)
            {
                // Create a new Tall Fescue
                Plants.PlantTallFescue newTallFescue = new Plants.PlantTallFescue();
                newTallFescue.identifier.name = "grass";
                newTallFescue.identifier.descriptiveAdjectives.Add("tall");
                newTallFescue.identifier.classifierAdjectives.Add("fescue");
                chunkToPopulate.AddChild(newTallFescue);
            }

            // the amont of oak Branches
            int amontOfOakBranches = random.Next(1, 3);

            for (int i = 0; i < amontOfOakBranches; i++)
            {
                // Create a new Oak Branches
                Plants.PlantParts.PlantPartOakTree.PlantPartOakTreeBranch newOakBranch = new Plants.PlantParts.PlantPartOakTree.PlantPartOakTreeBranch();
                newOakBranch.identifier.name = "branch";

                // Make it eather short or long
                if (random.Next(0, 1) == 0)
                    newOakBranch.identifier.descriptiveAdjectives.Add("short");
                else
                    newOakBranch.identifier.descriptiveAdjectives.Add("long");

                newOakBranch.identifier.classifierAdjectives.Add("oak");
                chunkToPopulate.AddChild(newOakBranch);
            }
            #endregion
        }
    }
}

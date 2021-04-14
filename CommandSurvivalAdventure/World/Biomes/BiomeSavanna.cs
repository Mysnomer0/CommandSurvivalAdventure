using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Biomes
{
    // This class holds all data for the forest biome
    class BiomeSavanna : Biome
    {
        // Generate and populate the forest
        public override void Generate(Chunk chunkToPopulate)
        {
            // Set the constants
            name = "savanna";
            normalTemperature = 85;
            normalWindSpeed = 10;
            associatedColor = "$ga";
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

            // the amont of acacia trees
            int amontOfAcaciaTrees = random.Next(1, 5);

            for (int i = 0; i < amontOfAcaciaTrees; i++)
            {
                // Create a new Acacia Trees
                Plants.PlantAcaciaTree newAcaciaTree = new Plants.PlantAcaciaTree();
                newAcaciaTree.identifier.name = "tree";

                // Make it eather short or tall
                if (random.Next(0, 1) == 0)
                    newAcaciaTree.identifier.descriptiveAdjectives.Add("short");
                else
                    newAcaciaTree.identifier.descriptiveAdjectives.Add("tall");

                newAcaciaTree.identifier.classifierAdjectives.Add("acacia");
                chunkToPopulate.AddChild(newAcaciaTree);
            }
            // the amont of acacia Branches
            int amontOfAcaciaBranches = random.Next(1, 3);

            for (int i = 0; i < amontOfAcaciaBranches; i++)
            {
                // Create a new acacia Branches
                Plants.PlantParts.PlantPartAcaciaTree.PlantPartAcaciaTreeBranch newAcaciaBranch = new Plants.PlantParts.PlantPartAcaciaTree.PlantPartAcaciaTreeBranch();
                newAcaciaBranch.identifier.name = "branch";

                // Make it eather long or short
                if (random.Next(0, 1) == 0)
                {
                    newAcaciaBranch.identifier.descriptiveAdjectives.Add("long");
                    newAcaciaBranch.specialProperties["weight"] = random.Next(5, 10).ToString();
                }
                else
                {
                    newAcaciaBranch.identifier.descriptiveAdjectives.Add("short");
                    newAcaciaBranch.specialProperties["weight"] = random.Next(2, 5).ToString();
                }
                    

                newAcaciaBranch.identifier.classifierAdjectives.Add("acacia");
                chunkToPopulate.AddChild(newAcaciaBranch);
            }
            #endregion

            #region Add minerals
            // Decide whether to generate copper
            if (random.Next(1, 5) == 1)
            {
                // the amont of copper
                int amontOfCopper = random.Next(1, 2);

                for (int i = 0; i < amontOfCopper; i++)
                {
                    // Create a new piece of copper
                    Minerals.MineralCopper newCopper = new Minerals.MineralCopper();
                    chunkToPopulate.AddChild(newCopper);
                }
            }

            // Decide whether to generate rocks
            if (random.Next(1, 5) == 1)
            {
                // the amont of rocks
                int amontOfRocks = random.Next(1, 3);

                for (int i = 0; i < amontOfRocks; i++)
                {
                    // Create a new piece of rock
                    Minerals.MineralBasaltRock newRock = new Minerals.MineralBasaltRock();
                    chunkToPopulate.AddChild(newRock);
                }
            }

            // Decide whether to generate salt
            if (random.Next(1, 5) == 1)
            {
                // the amont of salt
                int amontOfSalt = random.Next(1, 5);

                for (int i = 0; i < amontOfSalt; i++)
                {
                    // Create a new piece of salt
                    Minerals.MineralSalt newSalt = new Minerals.MineralSalt();
                    chunkToPopulate.AddChild(newSalt);
                }
            }
            #endregion
        }
    }
}


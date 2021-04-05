using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantTallFescue : Plant
    {
        // Initialize
        public override void Start()
        {
            base.Start();
        }
        // Update
        public override void Update()
        {
            base.Update();
        }

        public PlantTallFescue()
        {
            // Set the type
            type = typeof(PlantTallFescue);
            importanceLevel = 3;
            // Make a new seeded random instance for generating stats about the grass
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1, 2).ToString());

            // the amont of leaves the grass has
            int amontOfLeaves = random.Next(5, 12);

            for (int i = 0; i < amontOfLeaves; i++)
            {
                // Create a new leaf
                PlantParts.PlantPartTallFescue.PlantPartTallFescueBlade newTallFescueBlade = new PlantParts.PlantPartTallFescue.PlantPartTallFescueBlade();
                newTallFescueBlade.identifier.name = "blade";
                newTallFescueBlade.identifier.descriptiveAdjectives.Add("tall");
                newTallFescueBlade.identifier.classifierAdjectives.Add("fescue");
                AddChild(newTallFescueBlade);
            }
        }
    }
}
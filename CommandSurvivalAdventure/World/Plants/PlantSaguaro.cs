using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantSaguaro : Plant
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

        public PlantSaguaro()
        {
            // Set the type
            type = typeof(PlantSaguaro);
            importanceLevel = 6;
            // Make a new seeded random instance for generating stats about the Saguaro
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(20, 100).ToString());

            // the amont of leaves the Saguaro has
            int amontOfLeaves = random.Next(0, 6);

            for (int i = 0; i < amontOfLeaves; i++)
            {
                // Create a new arm
                PlantParts.PlantPartSaguaro.PlantPartSaguaroArm newSaguaroArm = new PlantParts.PlantPartSaguaro.PlantPartSaguaroArm();
                newSaguaroArm.identifier.name = "arm";
                newSaguaroArm.identifier.classifierAdjectives.Add("saguaro");
                AddChild(newSaguaroArm);
            }
        }
    }
}

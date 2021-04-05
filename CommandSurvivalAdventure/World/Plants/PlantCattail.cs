using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantCattail : Plant
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

        public PlantCattail()
        {
            // Set the type
            type = typeof(PlantCattail);
            importanceLevel = 5;
            // Make a new seeded random instance for generating stats about the cattail
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1, 3).ToString());

            // the amont of rhizomes the cattail has
            int amontOfRhizomes = random.Next(0, 2);

            for (int i = 0; i < amontOfRhizomes; i++)
            {
                // Create a new rhizome
                PlantParts.PlantPartCattail.PlantPartCattailRizome newPlantPart = new PlantParts.PlantPartCattail.PlantPartCattailRizome();
                newPlantPart.identifier.name = "rhizome";
                newPlantPart.identifier.classifierAdjectives.Add("cattail");
                AddChild(newPlantPart);
            }
        }
    }
}

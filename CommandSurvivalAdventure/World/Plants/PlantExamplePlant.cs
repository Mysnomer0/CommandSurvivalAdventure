using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    // An example plant to help people make their own new plants
    class PlantExamplePlant : Plant
    {
        // Update
        public override void Update()
        {
            base.Update();

            // Put your code here that will get run every time the world get's updated, which is every few seconds
        }
        // Initialize
        public PlantExamplePlant()
        {
            // Set the type
            type = typeof(PlantExamplePlant);
            // Set the plant's importance leve, that is, how important it is for the player to know about
            importanceLevel = 6;
            // Make a random instance for generating stats about the new plant
            Random random = new Random();

            // Add special properties such as weight
            specialProperties.Add("weight", random.Next(100, 200).ToString());

            // Put your code here that will run when the plant is first created, such as adjectives of the plant, branches connected to the plant, etc
            
        }
    }
}

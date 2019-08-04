using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Minerals
{
    class MineralBasaltBoulder : Mineral
    {
        public MineralBasaltBoulder()
        {
            // Make a new seeded random instance for generating stats about the boulder
            Random random = new Random();

            // Set the type
            type = typeof(MineralBasaltBoulder);
            importanceLevel = 5;

            // Adding special properties
            specialProperties.Add("canBeSharpenedWith", "");
            specialProperties.Add("canBeDulledWith", "");
            
            // Set the constants
            hardness = 65;
            density = hardness;
            transparency = 0;
            radioactiveLevel = 0;
            lightEmitingLevel = 0;
            meltingPoint = 2190;

            identifier.name = "boulder";

            // Make it eather large or small
            int chance = random.Next(0, 2);

            if (chance == 0)
            {
                identifier.descriptiveAdjectives.Add("large");
                specialProperties.Add("weight", random.Next(80, 110).ToString());
            }
            else if (chance == 1)
            {
                identifier.descriptiveAdjectives.Add("small");
                specialProperties.Add("weight", random.Next(20, 50).ToString());
            }
            else
                specialProperties.Add("weight", random.Next(50, 80).ToString());

            identifier.classifierAdjectives.Add("basalt");
        }
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
    }
}

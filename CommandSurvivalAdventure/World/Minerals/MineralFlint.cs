using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Minerals
{
    class MineralFlint : Mineral
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

        public MineralFlint()
        {
            // Set the type
            type = typeof(MineralFlint);
            importanceLevel = 3;
            // The flint can be used to sharpen with
            specialProperties.Add("canBeSharpenedWith", "");
            // Make a new seeded random instance for generating stats about the flint
            Random random = new Random();
            // Set the constants
            hardness = 60;
            density = hardness;
            transparency = 0;
            radioactiveLevel = 0;
            lightEmitingLevel = 0;
            meltingPoint = 3100;

            identifier.name = "flint";

            // Make it eather large or small
            int chance = random.Next(0, 2);

            if (chance == 0)
            {
                // large
                identifier.descriptiveAdjectives.Add("large");
                specialProperties.Add("weight", random.Next(5, 10).ToString());
            }
            else if (chance == 1)
            {
                // small
                identifier.descriptiveAdjectives.Add("small");
                specialProperties.Add("weight", random.Next(1, 3).ToString());
            }
            else
                specialProperties.Add("weight", random.Next(3, 5).ToString());

            //identifier.descriptiveAdjectives.Add("piece of");
            //identifier.classifierAdjectives.Add("piece");
            //identifier.classifierAdjectives.Add("of");
        }
    }
}
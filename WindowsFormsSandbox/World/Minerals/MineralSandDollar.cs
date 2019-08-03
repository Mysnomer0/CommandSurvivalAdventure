using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Minerals
{
    class MineralSandDollar : Mineral
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

        public MineralSandDollar()
        {
            // Set the type
            type = typeof(MineralSandDollar);
            importanceLevel = 6;
            
            // Make a new seeded random instance for generating stats about the shell
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1, 2).ToString());

            // Set the constants
            hardness = 10; // 1-100
            density = hardness;
            transparency = 0.5f; // 0-1
            radioactiveLevel = 0;
            lightEmitingLevel = 1; // 0-10
            meltingPoint = 200;

            identifier.name = "dollar";

            // Make it eather large or small
            int chance = random.Next(0, 2);

            if (chance == 0)
                identifier.descriptiveAdjectives.Add("large");
            else if (chance == 1)
                identifier.descriptiveAdjectives.Add("small");

            identifier.classifierAdjectives.Add("sand");
        }
    }
}
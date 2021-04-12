using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Minerals
{
    class MineralIce : Mineral
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

        public MineralIce()
        {

            // Set the type
            type = typeof(MineralIce);
            importanceLevel = 3;
            // Make a new seeded random instance for generating stats about the ice
            Random random = new Random();
            // Set the constants
            hardness = 20;
            density = hardness;
            transparency = 0.5f;
            radioactiveLevel = 0;
            lightEmitingLevel = 0;
            meltingPoint = 33;

            identifier.name = "slab";

            // Make it eather large or small
            int chance = random.Next(0, 2);

            if (chance == 0)
                identifier.descriptiveAdjectives.Add("large");
            else if (chance == 1)
                identifier.descriptiveAdjectives.Add("small");

            identifier.classifierAdjectives.Add("ice");
            //identifier.classifierAdjectives.Add("piece");
            //identifier.classifierAdjectives.Add("of");
        }
    }
}
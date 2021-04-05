using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Minerals
{
    class MineralSilt : Mineral
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

        public MineralSilt()
        {
            // Set the type
            type = typeof(MineralSilt);
            importanceLevel = 2;
            // Make a new seeded random instance for generating stats about the iron
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1, 2).ToString());

            // Set the constants
            hardness = 10;
            density = hardness;
            transparency = 0;
            radioactiveLevel = 0;
            lightEmitingLevel = 0;
            meltingPoint = 1760;

            identifier.name = "silt";

            // Make it eather large or small
            int chance = random.Next(0, 2);

            identifier.classifierAdjectives.Add("piece");
            identifier.classifierAdjectives.Add("of");
        }
    }
}
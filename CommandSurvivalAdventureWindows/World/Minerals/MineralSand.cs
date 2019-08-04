using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Minerals
{
    class MineralSand : Mineral
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

        public MineralSand()
        {
            // Set the type
            type = typeof(MineralSand);
            importanceLevel = 3;
            // Make a new seeded random instance for generating stats about the iron
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(1, 2).ToString());

            // Set the constants
            hardness = 15;
            density = hardness;
            transparency = 0.1f;
            radioactiveLevel = 0;
            lightEmitingLevel = 0;
            meltingPoint = 3000;

            identifier.name = "sand";

            identifier.isPluralAgnostic = true;
        }
    }
}
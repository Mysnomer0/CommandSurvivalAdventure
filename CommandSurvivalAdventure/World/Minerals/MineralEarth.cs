using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Minerals
{
    class MineralEarth : Mineral
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

        public MineralEarth()
        {
            // Set the type
            type = typeof(MineralEarth);
            importanceLevel = 1;
            // Make a new seeded random instance for generating stats about the earth
            Random random = new Random();
            // Add special properties
            specialProperties.Add("weight", random.Next(1, 5).ToString());

            // Set the constants
            hardness = 20;
            density = hardness;
            transparency = 0;
            radioactiveLevel = 0;
            lightEmitingLevel = 0;
            meltingPoint = 3000;

            identifier.name = "clump";

            // Make it eather large or small
            int chance = random.Next(0, 2);

            identifier.classifierAdjectives.Add("earth");
            //identifier.classifierAdjectives.Add("clump");
            //identifier.classifierAdjectives.Add("of");
        }
    }
}
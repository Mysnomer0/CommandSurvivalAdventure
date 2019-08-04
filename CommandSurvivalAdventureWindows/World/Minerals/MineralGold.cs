using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Minerals
{
    class MineralGold : Mineral
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

        public MineralGold()
        {
            // Set the type
            type = typeof(MineralGold);
            importanceLevel = 9;
            // Make a new seeded random instance for generating stats about the gold
            Random random = new Random();
            // Set the constants
            hardness = 25;
            density = hardness;
            transparency = 0;
            radioactiveLevel = 0;
            lightEmitingLevel = 0;
            meltingPoint = 1948;

            identifier.name = "gold";

            // Make it eather large or small
            int chance = random.Next(0, 2);

            if (chance == 0)
            {
                identifier.descriptiveAdjectives.Add("large");
                specialProperties.Add("weight", random.Next(15, 30).ToString());
            }
            else if (chance == 1)
            {
                identifier.descriptiveAdjectives.Add("small");
                specialProperties.Add("weight", random.Next(1, 5).ToString());
            }
            else
                specialProperties.Add("weight", random.Next(5, 15).ToString());

            identifier.classifierAdjectives.Add("piece");
            identifier.classifierAdjectives.Add("of");
        }
    }
}
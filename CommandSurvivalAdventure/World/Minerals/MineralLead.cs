using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Minerals
{
    class MineralLead : Mineral
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

        public MineralLead()
        {
            // Set the type
            type = typeof(MineralLead);
            importanceLevel = 5;
            // Make a new seeded random instance for generating stats about the lead
            Random random = new Random();
            // Set the constants
            hardness = 20;
            density = hardness;
            transparency = 0;
            radioactiveLevel = 0;
            lightEmitingLevel = 0;
            meltingPoint = 621;

            identifier.name = "chunk";

            // Make it eather large or small
            int chance = random.Next(0, 2);

            if (chance == 0)
            {
                identifier.descriptiveAdjectives.Add("large");
                specialProperties.Add("weight", random.Next(10, 20).ToString());
            }
            else if (chance == 1)
            {
                identifier.descriptiveAdjectives.Add("small");
                specialProperties.Add("weight", random.Next(1, 5).ToString());
            }
            else
                specialProperties.Add("weight", random.Next(5, 10).ToString());
            identifier.classifierAdjectives.Add("lead");
            //identifier.classifierAdjectives.Add("piece");
            //identifier.classifierAdjectives.Add("of");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Minerals
{
    class MineralSalt : Mineral
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

        public MineralSalt()
        {
            // Set the type
            type = typeof(MineralSalt);
            importanceLevel = 4;
            // Make a new seeded random instance for generating stats about the salt
            Random random = new Random();
            // Set the constants
            hardness = 15;
            density = hardness;
            transparency = 0.1f;
            radioactiveLevel = 0;
            lightEmitingLevel = 0;
            meltingPoint = 1474;

            identifier.name = "chunk";

            // Make it eather large or small
            int chance = random.Next(0, 2);

            if (chance == 0)
            {
                identifier.descriptiveAdjectives.Add("large");
                specialProperties.Add("weight", random.Next(3, 4).ToString());
            }
            else if (chance == 1)
            {
                identifier.descriptiveAdjectives.Add("small");
                specialProperties.Add("weight", random.Next(1, 2).ToString());
            }
            else
                specialProperties.Add("weight", random.Next(2, 3).ToString());
            identifier.classifierAdjectives.Add("salt");
            //identifier.classifierAdjectives.Add("piece");
            //identifier.classifierAdjectives.Add("of");
        }
    }
}
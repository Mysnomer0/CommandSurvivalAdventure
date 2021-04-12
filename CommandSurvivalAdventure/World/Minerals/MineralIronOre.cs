using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Minerals
{
    class MineralIronOre : Mineral
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

        public MineralIronOre()
        {

            // Set the type
            type = typeof(MineralIronOre);
            importanceLevel = 5;
            // Add special properties
            specialProperties.Add("canBeSharpenedWith", "");
            // Make a new seeded random instance for generating stats about the iron
            Random random = new Random();
            // Set the constants
            hardness = 70;
            density = hardness;
            transparency = 0;
            radioactiveLevel = 0;
            lightEmitingLevel = 0;
            meltingPoint = 2800;

            identifier.name = "ore";

            // Make it eather large or small
            int chance = random.Next(0, 3);

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
            identifier.classifierAdjectives.Add("iron");
            //identifier.classifierAdjectives.Add("piece");
            //identifier.classifierAdjectives.Add("of");
        }
    }
}

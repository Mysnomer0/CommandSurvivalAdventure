﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Creatures.CreatureParts.CreaturePartMinotaur
{
    class CreaturePartMinotaurLeg : CreaturePart
    {
        public CreaturePartMinotaurLeg()
        {
            // Make a new seeded random instance for generating stats about the part
            Random random = new Random();

            // Add special properties
            specialProperties.Add("weight", random.Next(60, 80).ToString());
        }
    }
}
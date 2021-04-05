using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Crafting.Combinations
{
    class CraftingCombinationSledgeHammer : CraftingCombination
    {
        public CraftingCombinationSledgeHammer()
        {
            newName = "sledgehammer";
            necessaryType.Add(typeof(Plants.PlantHandle));
            necessaryDescriptiveAdjectives.Add("long");
            chainOfNecessaryChildren.Add(new PossibleChild(new List<Type>() { typeof(Plants.PlantRope) }, new List<string>() { }));
            chainOfNecessaryChildren.Add(new PossibleChild(new List<Type>() { typeof(Minerals.MineralBasaltRock), typeof(Minerals.MineralBasaltBoulder) }, new List<string>() { "large", "blunt" }));
        }
    }
}


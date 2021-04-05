using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Crafting.Combinations
{
    class CraftingCombinationHatchet : CraftingCombination
    {
        public CraftingCombinationHatchet()
        {
            newName = "hatchet";
            necessaryType.Add(typeof(Plants.PlantHandle));
            necessaryDescriptiveAdjectives.Add("short");
            chainOfNecessaryChildren.Add(new PossibleChild(new List<Type>() { typeof(Plants.PlantRope) }, new List<string>() { }));
            chainOfNecessaryChildren.Add(new PossibleChild(new List<Type>() { typeof(Minerals.MineralBasaltRock), typeof(Minerals.MineralFlint) }, new List<string>() { "sharp" }));
        }
    }
}


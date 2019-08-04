using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Crafting.Combinations
{
    class CraftingCombinationAxe : CraftingCombination
    {
        public CraftingCombinationAxe()
        {
            newName = "axe";
            necessaryType.Add(typeof(Plants.PlantHandle));
            necessaryDescriptiveAdjectives.Add("long");
            chainOfNecessaryChildren.Add(new PossibleChild(new List<Type>() { typeof(Plants.PlantRope) }, new List<string>() { }));
            chainOfNecessaryChildren.Add(new PossibleChild(new List<Type>() { typeof(Minerals.MineralBasaltRock) }, new List<string>() { "large", "sharp" }));
        }
    }
}
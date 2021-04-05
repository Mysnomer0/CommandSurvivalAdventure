using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Crafting.Combinations
{
    class CraftingCombinationHammer : CraftingCombination
    {
        public CraftingCombinationHammer()
        {
            newName = "hammer";
            necessaryType.Add(typeof(Plants.PlantHandle));
            necessaryDescriptiveAdjectives.Add("short");
            chainOfNecessaryChildren.Add(new PossibleChild(new List<Type>() { typeof(Plants.PlantRope) }, new List<string>() { }));
            chainOfNecessaryChildren.Add(new PossibleChild(new List<Type>() { typeof(Minerals.MineralBasaltRock) }, new List<string>() { "small", "blunt" }));
        }
    }
}


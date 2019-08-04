using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Crafting.Recipies
{
    class CraftingRecipeRope : CraftingRecipe
    {
        public CraftingRecipeRope()
        {
            ingredients.Add(typeof(Plants.PlantVine), 2);
            ingredients.Add(typeof(Plants.PlantTallFescue), 5);
            output = typeof(Plants.PlantRope);
        }
    }
}

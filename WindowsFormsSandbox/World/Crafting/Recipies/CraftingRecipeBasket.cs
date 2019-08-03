using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Crafting.Recipies
{
    class CraftingRecipeBasket : CraftingRecipe
    {
        public CraftingRecipeBasket()
        {
            ingredients.Add(typeof(Plants.PlantYucca), 5);
            ingredients.Add(typeof(Plants.PlantReed), 5);
            ingredients.Add(typeof(Plants.PlantCattail), 5);
            output = typeof(Plants.PlantBasket);
        }
    }
}

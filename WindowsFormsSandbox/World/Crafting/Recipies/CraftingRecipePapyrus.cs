using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Crafting.Recipies
{
    class CraftingRecipePapyrus : CraftingRecipe
    {
        public CraftingRecipePapyrus()
        {
            ingredients.Add(typeof(Plants.PlantCattail), 5);
            output = typeof(Plants.PlantPapyrus);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Crafting.Recipies
{
    class CraftingRecipeHandle : CraftingRecipe
    {
        public CraftingRecipeHandle()
        {
            ingredients.Add(typeof(Plants.PlantParts.PlantPartAcaciaTree.PlantPartAcaciaTreeBranch), 1);
            ingredients.Add(typeof(Plants.PlantParts.PlantPartOakTree.PlantPartOakTreeBranch), 1);
            ingredients.Add(typeof(Plants.PlantParts.PlantPartEucalyptusTree.PlantPartEucalyptusTreeBranch), 1);
            ingredients.Add(typeof(Plants.PlantParts.PlantPartRubberTree.PlantPartRubberTreeBranch), 1);
            output = typeof(Plants.PlantHandle);
        }
    }
}

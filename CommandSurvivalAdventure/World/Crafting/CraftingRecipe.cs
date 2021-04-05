using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Crafting
{
    // The base class for all crafting recipies
    abstract class CraftingRecipe
    {
        // The list of object types and the amount of each the recipe calls for
        // Each new entry in the dictionary is an alternate ingredient that the recipe can use to create the object
        public Dictionary<Type, int> ingredients = new Dictionary<Type, int>();
        // The type of object that the recipe will output
        public Type output = null;
    }
}

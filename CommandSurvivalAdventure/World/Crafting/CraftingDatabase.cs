using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CommandSurvivalAdventure.World.Crafting
{
    // Contains all the crafting recipe names, objects required to craft them, and the corresponding object they produce
    class CraftingDatabase : CSABehaviour
    {
        // The dictionary of recipies
        private Dictionary<string, CraftingRecipe> recipies = new Dictionary<string, CraftingRecipe>();
        // The dictionary of object combinations that become a new object
        private HashSet<CraftingCombination> combinations = new HashSet<CraftingCombination>();

        // Returns the recipe given the name
        public CraftingRecipe GetRecipe(string nameOfRecipe)
        {
            return recipies[nameOfRecipe];
        }
        // Returns whether or not the given recipe exists
        public bool CheckRecipe(string nameOfRecipe)
        {
            return recipies.ContainsKey(nameOfRecipe);
        }
        // Checks the given object to see the object 
        public CraftingCombination CheckObjectForCombination(GameObject gameObject)
        {
            // Loop through the combinations
            foreach(CraftingCombination combination in combinations)
            {
                // Return true if this base object and it's children match the combination
                if (combination.necessaryType.Contains(gameObject.type) 
                    && gameObject.identifier.descriptiveAdjectives.All(combination.necessaryDescriptiveAdjectives.Contains) 
                    && CheckObjectForChildrenWithTypes(gameObject, combination.chainOfNecessaryChildren, 0))
                    return combination;
            }
            return null;
        }
        // Checks the given object to see if it has a child with one of the given types
        // A helper function for the above function
        private bool CheckObjectForChildrenWithTypes(GameObject gameObject, List<CraftingCombination.PossibleChild> possibleTypeForChild, int currentIndex)
        {
            // Loop through the children
            foreach(GameObject child in gameObject.children)
            {
                // If this child's type matches, and we're at the end of the crafting combination, return a success
                if (possibleTypeForChild[currentIndex].CheckGameObjectForMatch(child) && possibleTypeForChild[currentIndex] == possibleTypeForChild.Last())
                    return true;
                // Otherwise, if this child's type matches, continue the recursive function
                else if (possibleTypeForChild[currentIndex].CheckGameObjectForMatch(child))
                    return CheckObjectForChildrenWithTypes(child, possibleTypeForChild, currentIndex + 1);
            }
            // If the loop went clean through, that means it didn't find a child with the correct type
            return false;
        }
        // Initialize
        public CraftingDatabase(Application newApplication)
        {
            // Set the application
            attachedApplication = newApplication;

            // Initialize the dictionary of commands
            recipies.Add("rope", new Recipies.CraftingRecipeRope());
            recipies.Add("handle", new Recipies.CraftingRecipeHandle());
            recipies.Add("haft", new Recipies.CraftingRecipeHandle());
            recipies.Add("papyrus", new Recipies.CraftingRecipePapyrus());
            recipies.Add("basket", new Recipies.CraftingRecipeBasket());

            combinations.Add(new Combinations.CraftingCombinationHammer());
            combinations.Add(new Combinations.CraftingCombinationAxe());
            combinations.Add(new Combinations.CraftingCombinationSledgeHammer());
            combinations.Add(new Combinations.CraftingCombinationHatchet());
            combinations.Add(new Combinations.CraftingCombinationDagger());
            combinations.Add(new Combinations.CraftingCombinationSpear());
        }
    }
}

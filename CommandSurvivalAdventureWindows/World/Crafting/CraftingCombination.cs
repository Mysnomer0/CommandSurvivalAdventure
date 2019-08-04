using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CommandSurvivalAdventure.World.Crafting
{
    // The base class for all crafting recipies
    abstract class CraftingCombination
    {
        // The possible types that the object needs to be for the combination to work
        public List<Type> necessaryType = new List<Type>();
        // The descriptive adjectives required for the base object
        public List<string> necessaryDescriptiveAdjectives = new List<string>();
        // This data type stores possible children types with their corresponding descriptive adjectives
        public class PossibleChild
        {
            // The possible types for each of the children
            public List<Type> possibleTypesForChildren = new List<Type>();
            // The paralell list of necessary descriptive adjectives for the child
            public List<string> necessaryDescriptiveAdjectives = new List<string>();
            // Returns true if the given objct matches this possible child
            public bool CheckGameObjectForMatch(GameObject gameObject)
            {
                // Make sure the object is one of the correct types, and has the necessary descriptive adjectives
                return possibleTypesForChildren.Contains(gameObject.type) || !necessaryDescriptiveAdjectives.Except(gameObject.identifier.descriptiveAdjectives).Any();
            }
            // Initialize
            public PossibleChild(List<Type> possibleTypes, List<string> descriptiveAdjectives)
            {
                possibleTypesForChildren = possibleTypes;
                necessaryDescriptiveAdjectives = descriptiveAdjectives;
            }
        }
        // The list of possible children necessary for this crafting combination to transform the object into the new one
        // Each value contains a list of the optional types for each ingredient
        public List<PossibleChild> chainOfNecessaryChildren = new List<PossibleChild>();
        // The new name for the object that this combination creates
        public string newName = "";
    }
}
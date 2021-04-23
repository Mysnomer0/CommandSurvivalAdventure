using System;
using System.Collections.Generic;

namespace CommandSurvivalAdventure.World
{
    // This is the base class for any gameobject that exists in a world
    abstract class GameObject : CSABehaviour
    {
        // The name of the game object
        public Processing.Identifier identifier = new Processing.Identifier();
        // The importance level of the game object, which defaults to the highest
        public int importanceLevel = 10;
        // Any special boolean attributes of the gameObject, such as if this is utilizable or is a player, will exist here if true
        public Dictionary<string, string> specialProperties = new Dictionary<string, string>();
        // The queue of command that are running on this GameObject
        public Queue<Support.Networking.ServerCommand> commandQueue = new Queue<Support.Networking.ServerCommand>();
        // The type of the gameObject, which is necessary to store since sometimes we are generating instances without knowing types
        public Type type;
        // The ID given to this GameObject when put on a world;
        public int ID;
        // The position of the game object
        public Position position { get; private set; } = null;
        // The list of game objects immediately nearby this game object
        public HashSet<GameObject> gameObjectsInProximity = new HashSet<GameObject>();
        // The parent of this gameObject, if any
        public GameObject parent = null;
        // The list of sub parts, such as leg, head, leaf, trunk, etc
        public readonly HashSet<GameObject> children = new HashSet<GameObject>();
        // Starts the gameObject
        public virtual void Start() { }
        // Updates the gameObject
        public virtual void Update() { }
        // Generates the gameObject, usefull for the world generation scripts
        public virtual void Generate(int seed) { }
        // Adds a new child; Please always use this instead of children.Add()!
        public virtual void AddChild(GameObject newChild)
        {
            // Make sure we don't add ourself to ourself
            if (newChild == this)
                return;
            // Add the new child to our root GameObject, usually the world, if we are not the world
            if (!newChild.specialProperties.ContainsKey("isRegistered"))
            {
                // The root gameObject
                GameObject root;
                // Loop through the parents until we find the root
                for (root = this; root.parent != null; root = root.parent) ;
                // If we are not the root, don't add twice
                if (root != this)
                    // Add the child to the root
                    root.AddChild(newChild);
            }
            // If the new child had a previous parent, remove it from their children
            if(newChild.parent != null)
                newChild.parent.children.Remove(newChild);
            // Add the new child locally
            children.Add(newChild);
            newChild.parent = this;
            // Give the child our position
            newChild.ChangePosition(position);
        }
        // Removes a child; Use instead of children.Remove
        public virtual void RemoveChild(GameObject childToRemove)
        {
            children.Remove(childToRemove);
        }
        // Returns the first GameObject found if we have a child with the given string in it's name
        public virtual GameObject ContainsChild(string name)
        {
            // If this gameObject has children
            if (children != null)
            {
                // Loop through the children, checking whether or not they have the name in their name, or if they contain a child that does
                foreach (GameObject child in children)
                {
                    if (child.identifier.fullName.Contains(name))
                        return child;
                    else if (child.ContainsChild(name) != null)
                        return child.ContainsChild(name);
                }
            }
            return null;
        }
        // Returns all GameObjects found with the given string in it's name
        public virtual List<GameObject> GetAllChildren()
        {
            // Create a new hashset and populate and return it
            List<GameObject> listOfAllChildren = new List<GameObject>();
            AddAllChildrenToList(ref listOfAllChildren);
            return listOfAllChildren;
        }
        // A helper function for the GetAllChildren function
        private void AddAllChildrenToList(ref List<GameObject> listToAddTo)
        {
            // If this gameObject has children
            if (children != null)
            {
                // Loop through the children, adding them and their children to the list
                foreach (GameObject child in children)
                {
                    // Add the child and it's children to the list
                    listToAddTo.Add(child);
                    child.AddAllChildrenToList(ref listToAddTo);
                }
            }
        }
        // Returns all children found with the given string in it's name
        public virtual List<GameObject> FindChildrenWithName(string nameToCheckFor)
        {
            // If we were asked to find a specific child of an object, for example, "the tree's branch", parse the name of the parent, in our example, "tree", and pass that along to the recursive function

            // The possible name of the parent
            string possibleNameOfParent = "";
            // Make sure the string isn't null
            if (!String.IsNullOrWhiteSpace(nameToCheckFor))
            {
                // Get the index of the apostrophe which will indicate the 's somewhere
                int charLocation = nameToCheckFor.IndexOf("'", StringComparison.Ordinal);
                // If the char actually exists
                if (charLocation > 0)
                {
                    // Set the name of the parent equal to everything before the apostrophe
                    possibleNameOfParent = nameToCheckFor.Substring(0, charLocation);
                    // Remove the name of the parent from the name of the object we are looking for, so as not to confuse anybody
                    nameToCheckFor = nameToCheckFor.Length > charLocation + 2 ? nameToCheckFor.Substring(charLocation + 2) : nameToCheckFor;
                }
            }
            // Create a new hashset and populate and return it
            List <GameObject> listOfAllChildrenWithName = new List<GameObject>();
            AddAllChildrenWithNameToList(nameToCheckFor, possibleNameOfParent, ref listOfAllChildrenWithName);
            return listOfAllChildrenWithName;
        }
        // A helper function for the GetChildrenWithName function
        private void AddAllChildrenWithNameToList(string nameToCheckFor, string nameOfParentIfAny, ref List<GameObject> listToAddTo)
        {
            // If this gameObject has children
            if (children != null)
            {
                // Loop through the children, checking whether or not they have the name in their name, and if so, add them to the hashset
                foreach (GameObject child in children)
                {
                    // If there is no parent name to check for, just make sure the child's name matches
                    if(nameOfParentIfAny == "" && child.identifier.DoesStringPartiallyMatchFullName(nameToCheckFor)
                        // Or, if the name of the parent matches our own and the child's name matches as well
                        || identifier.DoesStringPartiallyMatchFullName(nameOfParentIfAny) && child.identifier.DoesStringPartiallyMatchFullName(nameToCheckFor))
                    {
                        // Add the child to the list of objects that match
                        listToAddTo.Add(child);
                    }
                    // Or if the name of the parent matches any of our parent's names
                    else if(nameOfParentIfAny != "" && FindParentWithName(nameOfParentIfAny).Count > 0 && child.identifier.DoesStringPartiallyMatchFullName(nameToCheckFor))
                    {
                        // Add the child to the list of objects that match
                        listToAddTo.Add(child);
                    }
                    // Do the same with all children
                    child.AddAllChildrenWithNameToList(nameToCheckFor, nameOfParentIfAny, ref listToAddTo);
                }
            }
        }
        // Returns all parents found with the given string in it's name
        public virtual List<GameObject> FindParentWithName(string nameToCheckFor)
        {
            // Create a new hashset and populate and return it
            List<GameObject> listOfAllChildrenWithName = new List<GameObject>();
            AddAllParentsWithNameToList(nameToCheckFor, ref listOfAllChildrenWithName);
            return listOfAllChildrenWithName;
        }
        // A helper function for the GetChildrenWithName function
        private void AddAllParentsWithNameToList(string nameToCheckFor, ref List<GameObject> listToAddTo)
        {
            // If this gameObject has a parent
            if (parent != null)
            {
                // If the parents name matches
                if (parent.identifier.DoesStringPartiallyMatchFullName(nameToCheckFor))
                    // Add it to the list
                    listToAddTo.Add(parent);
                // Do the same with all children
                parent.AddAllParentsWithNameToList(nameToCheckFor, ref listToAddTo);
            }
        }
        // Returns all GameObjects found with the given special property
        public virtual List<GameObject> FindChildrenWithSpecialProperty(string specialPropertyToCheckFor)
        {
            // Create a new hashset and populate and return it
            List<GameObject> listOfAllChildrenWithSpecialProperty = new List<GameObject>();
            AddAllChildrenWithSpecialPropertyToList(specialPropertyToCheckFor, ref listOfAllChildrenWithSpecialProperty);
            return listOfAllChildrenWithSpecialProperty;
        }
        // A helper function for the GetChildrenWithName function
        private void AddAllChildrenWithSpecialPropertyToList(string specialPropertyToCheckFor, ref List<GameObject> listToAddTo)
        {
            // If this gameObject has children
            if (children != null)
            {
                // Loop through the children, checking whether or not they have the name in their name, and if so, add them to the hashset
                foreach (GameObject child in children)
                {
                    if (child.specialProperties.ContainsKey(specialPropertyToCheckFor))
                        listToAddTo.Add(child);
                    // Do the same with all children
                    child.AddAllChildrenWithSpecialPropertyToList(specialPropertyToCheckFor, ref listToAddTo);
                }
            }
        }
        // Returns all GameObjects found with the given special property and matching value
        public virtual List<GameObject> FindChildrenWithSpecialPropertyAndValue(string specialPropertyToCheckFor, string value)
        {
            // Create a new hashset and populate and return it
            List<GameObject> listOfAllChildrenWithSpecialPropertyAndValue = new List<GameObject>();
            AddAllChildrenWithSpecialPropertyAndValueToList(specialPropertyToCheckFor, value, ref listOfAllChildrenWithSpecialPropertyAndValue);
            return listOfAllChildrenWithSpecialPropertyAndValue;
        }
        // A helper function for the GetChildrenWithName function
        private void AddAllChildrenWithSpecialPropertyAndValueToList(string specialPropertyToCheckFor, string value, ref List<GameObject> listToAddTo)
        {
            // If this gameObject has children
            if (children != null)
            {
                // Loop through the children, checking whether or not they have the name in their name, and if so, add them to the hashset
                foreach (GameObject child in children)
                {
                    if (child.specialProperties.ContainsKey(specialPropertyToCheckFor))
                    {
                        if(child.specialProperties[specialPropertyToCheckFor] == value)
                            listToAddTo.Add(child);
                    }
                    // Do the same with all children
                    child.AddAllChildrenWithSpecialPropertyAndValueToList(specialPropertyToCheckFor, value, ref listToAddTo);
                }
            }
        }
        // Returns all GameObjects found with the given special property and matching value
        public virtual List<GameObject> FindParentsWithSpecialProperty(string specialPropertyToCheckFor)
        {
            // Create a new hashset and populate and return it
            List<GameObject> listOfAllChildrenWithSpecialPropertyAndValue = new List<GameObject>();
            AddAllParentsWithSpecialPropertyToList(specialPropertyToCheckFor, ref listOfAllChildrenWithSpecialPropertyAndValue);
            return listOfAllChildrenWithSpecialPropertyAndValue;
        }
        // A helper function for the GetChildrenWithName function
        private void AddAllParentsWithSpecialPropertyToList(string specialPropertyToCheckFor, ref List<GameObject> listToAddTo)
        {
            // If this gameObject has a parent
            if (parent != null)
            {
                // If the parent has the property to check for, add it to the list
                if (parent.specialProperties.ContainsKey(specialPropertyToCheckFor))
                    listToAddTo.Add(parent);
                // Do the same with all the parents
                parent.AddAllParentsWithSpecialPropertyToList(specialPropertyToCheckFor, ref listToAddTo);
            }
        }
        // Changes the position of the GameObject and it's children
        public virtual void ChangePosition(Position newPosition)
        {
            // Reset our list of objects in proximity if we're moving positions
            if(position != null)
            {
                if(position != newPosition)
                {
                    gameObjectsInProximity.Clear();
                }
            }
                
            // Set our position and our children's position to the new one
            position = newPosition;
            if(children != null)
            {
                foreach(GameObject child in children)
                    child.ChangePosition(newPosition);
            }
        }
        // Call this function on a gameObject you want to strike. This function will handle damage specific for each type of game object. For example, a creature being struck would handle damage differently than a plant
        public virtual void StrikeThisGameObjectWithGameObject(GameObject whoIsStriking, GameObject whatIsBeingUsedToStrike)
        {

        }
    }
}


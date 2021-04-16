using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Humanizer;
using CommandSurvivalAdventure.World;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace CommandSurvivalAdventure.Processing
{
    // This class is responsible for taking an object with it's data and describing it to the player
    class Describer
    {
        // Object with everything that has been described
        public static Chunk logChunk = new Chunk();
        
        // Returns a description of the given GameObject
        public static string Describe(World.Chunk chunkToDescribe, World.GameObject objectRecievingDescription)
        {
            // The description to return
            string description = "\n\n";
            // The random generator
            Random random = new Random();

            #region Describe the biome
            // Describe the biome with a little variation
            if (random.Next(0, 1) == 0)
            {
                description += "You are surrounded by ";
            }
            else
            {
                description += "You are in ";
            }
            // If the biome is plural
            if (chunkToDescribe.biome.name[(chunkToDescribe.biome.name.Length == 0) ? 0 : chunkToDescribe.biome.name.Length - 1] == 's')
            {
                description += "some ";
            }
            // Update log of info stream
            //Logs.biomeName = GetArticle(chunkToDescribe.biome.name);
            description += GetArticle(chunkToDescribe.biome.name) + " ";
            description += ToColor(chunkToDescribe.biome.name, chunkToDescribe.biome.associatedColor);
            description += ". ";

            #endregion

            #region Describe the weather
            // Describe the wind speed
            if (chunkToDescribe.windSpeed < 5)
                description += "\nThe air is very still. ";
            else if (chunkToDescribe.windSpeed < 20)
                description += "\nThe wind is blowing gently. ";
            else if (chunkToDescribe.windSpeed < 40)
                description += "\nThe wind is $jablowing $jaharshly. ";
            else
                description += "\nThe wind is $jahowling $jauncontrollably. ";
            // Describe the temperature
            if (chunkToDescribe.temperature < 20)
                description += "\nThe temperature is $lavery $lacold. ";
            else if (chunkToDescribe.temperature < 40)
                description += "\nThe temperature is $lacold. ";
            else if (chunkToDescribe.temperature < 60)
                description += "\nThe temperature is mildly cold. ";
            else if (chunkToDescribe.temperature < 80)
                description += "\nThe temperature is $gawarm. ";
            else if (chunkToDescribe.temperature < 100)
                description += "\nThe temperature is $gahot. ";
            else 
                description += "\nThe temperature is $eavery $eahot. ";
            #endregion

            #region Describe the surrounding areas of land

            #endregion

            #region Describe the GameObjects
            // Tack on some new line characters
            description += "\n\n";

            #region Get all visible objects

            #endregion

            #region Sort by importance level
            // Get all the gameObjects on the chunk
            List<World.GameObject> allGameObjectsOnChunk = chunkToDescribe.GetAllChildren();
            // The list of all gameObjects on the chunk sorted by importance level
            Dictionary<int, List<World.GameObject>> allGameObjectsOnChunkSortedByImportanceLevel = new Dictionary<int, List<World.GameObject>>();
            // Loop through all the gameObjects on the chunk, sorting by importance level
            foreach (World.GameObject gameObject in allGameObjectsOnChunk)
            {
                // If the list doesn't have the key, allocate a new list in the slot in the dictionary
                if (!allGameObjectsOnChunkSortedByImportanceLevel.ContainsKey(gameObject.importanceLevel))
                    allGameObjectsOnChunkSortedByImportanceLevel.Add(gameObject.importanceLevel, new List<World.GameObject>());
                // Once we know the slot is allocated, add the game object to the slot
                allGameObjectsOnChunkSortedByImportanceLevel[gameObject.importanceLevel].Add(gameObject);
            }
            #endregion

            #region Sort by type and amount of each object
            // The list of the different types of objects, used for determining how to output them
            Dictionary<Type, int> amountOfEachObject = new Dictionary<Type, int>();
            // The dictionary of the first of each of the gameObjects
            List<World.GameObject> firstOfEachGameObject = new List<World.GameObject>();
            // Describe the players first
            foreach(World.GameObject gameObject in chunkToDescribe.children)
            {
                // If this object is a player, describe it 
                if (gameObject.specialProperties.ContainsKey("isPlayer"))
                {
                    if(gameObject.identifier.name != objectRecievingDescription.identifier.name)
                        description += gameObject.identifier.name + " is nearby.\n";
                }
                // If the dictionary does not contain the given type yet, add it
                else if (!amountOfEachObject.ContainsKey(gameObject.type))
                {
                    amountOfEachObject.Add(gameObject.type, 1);
                    firstOfEachGameObject.Add(gameObject);
                }
                else
                    amountOfEachObject[gameObject.type]++;
            }
            // Loop through again and add to the description according to how many there are
            foreach (World.GameObject gameObject in firstOfEachGameObject)
            {
                // The subject of the sentence we are creating
                string subject = "";
                // The verb phrase
                string verbPhrase = "";
                // The prepositional phrase tacked on at the end
                string prepositionalPhrase = "";
                // If there is only one of this type of object, print accordingly
                if(amountOfEachObject[gameObject.type] == 1)
                {
                    subject = "There";
                    verbPhrase = "is";
                    prepositionalPhrase = "a nearby " + ToColor(gameObject.identifier.fullName, gameObject.specialProperties.ContainsKey("colorAdd") ? gameObject.specialProperties["colorAdd"] : "");
                }
                // If there are relativly few, print accordingly
                else if(amountOfEachObject[gameObject.type] <= 10)
                {
                    subject = "There";
                    verbPhrase = "are";
                    prepositionalPhrase = "some nearby " + ToColor(gameObject.identifier.fullName.Pluralize(), gameObject.specialProperties.ContainsKey("colorAdd") ? gameObject.specialProperties["colorAdd"] : "");
                    /*
                    // If the word ends in "s", add an "es" to the end
                    if (gameObject.identifier.name[(gameObject.identifier.name.Length == 0) ? 0 : gameObject.identifier.name.Length - 1] == 's')
                        prepositionalPhrase = "some nearby " + ToColor(gameObject.identifier.fullName, gameObject.specialProperties.ContainsKey("colorAdd") ? gameObject.specialProperties["colorAdd"] : "") + "es";
                    else
                        prepositionalPhrase = "some nearby " + ToColor(gameObject.identifier.fullName, gameObject.specialProperties.ContainsKey("colorAdd") ? gameObject.specialProperties["colorAdd"] : "") + "s";
                    */
                }
                // If there are a lot, print accordingly
                else
                {
                    subject = "There";
                    verbPhrase = "are many nearby";
                    prepositionalPhrase = ToColor(gameObject.identifier.fullName.Pluralize(), gameObject.specialProperties.ContainsKey("colorAdd") ? gameObject.specialProperties["colorAdd"] : "");
                    /*
                    // If the word ends in "s", don't change it to plural
                    if (gameObject.identifier.name[(gameObject.identifier.name.Length == 0) ? 0 : gameObject.identifier.name.Length - 1] == 's')
                        prepositionalPhrase = ToColor(gameObject.identifier.fullName, gameObject.specialProperties.ContainsKey("colorAdd") ? gameObject.specialProperties["colorAdd"] : "");
                    else
                        prepositionalPhrase = ToColor(gameObject.identifier.fullName, gameObject.specialProperties.ContainsKey("colorAdd") ? gameObject.specialProperties["colorAdd"] : "") + "s";
                    */
                }
                // Build the description 
                description += subject + " " + verbPhrase + " " + prepositionalPhrase + ". \n";
            }
            #endregion

            #endregion

            // Update log chunk
            logChunk = chunkToDescribe;

            return description;
        }

        // Returns the article for the given word, a or an
        public static string GetArticle(string word)
        {
            // Determine the article
            if (word[0] == 'a'
                || word[0] == 'e'
                || word[0] == 'i'
                || word[0] == 'o'
                || word[0] == 'u')
            {
                return "an";
            }
            else
            {
                return "a";
            }
        }
        // Apply a color to each word in the string
        public static string ToColor(string stringToColor, string color)
        {
            // Split this string into words
            List<string> words = stringToColor.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            // The string tahat we are going to return
            string stringToReturn = "";
            foreach(string word in words)
            {
                if(word == words.Last())
                {
                    stringToReturn += color + word;
                }
                else
                {
                    stringToReturn += color + word + " ";
                }
                
            }

            return stringToReturn;
        }
    }
}

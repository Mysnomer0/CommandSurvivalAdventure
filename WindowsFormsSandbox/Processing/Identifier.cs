using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CommandSurvivalAdventure.Processing
{
    class Identifier 
    {
        // The list of adjectives of this identifier, which may include the type, such as acacia, oak, iron, or perhaps refer to size, like large, small, etc
        public List<string> descriptiveAdjectives = new List<string>();
        // The list of aliases that this identifier can also be known by
        public List<string> classifierAdjectives = new List<string>();
        // The literal string of this identifier
        public string name = "";
        // The full identifier with all the adjectives tacked on
        public string fullName
        {
            get
            {
                string stringToReturn = "";
                // Add on the descriptive adjectives
                if (descriptiveAdjectives.Count == 1)
                    stringToReturn += descriptiveAdjectives[0] + " ";
                else if(descriptiveAdjectives.Count > 1)
                {
                    foreach (string descriptiveAdjective in descriptiveAdjectives)
                        stringToReturn += descriptiveAdjective + ", ";
                }
                // Add on the classifier adjectives
                foreach(string classifierAdjective in classifierAdjectives)
                    stringToReturn += classifierAdjective + " ";
                // Of course, add on the name
                stringToReturn += name;

                return stringToReturn;
            }
        }
        // The identifier with just the classifier adjectives tacked on
        public string nameWithClassifiers
        {
            get
            {
                string stringToReturn = "";
                // Add on the classifier adjectives
                foreach (string classifierAdjective in classifierAdjectives)
                    stringToReturn += classifierAdjective + " ";
                // Of course, add on the name
                stringToReturn += name;

                return stringToReturn;
            }
        }
        // Whether of not this identifier is proper, such as if it is a proper name, place, player name, etc
        public bool isProper = false;
        // Whether or not this idenfier's form changes if it is plural.  Examples where this is true would be deer, sheep, moose, etc
        public bool isPluralAgnostic = false;
        // Whether or not the given string matches or partially matches the full name
        public bool DoesStringPartiallyMatchFullName(string possiblePartialMatch)
        {
            // Split the possible partial match up into words
            List<string> words = possiblePartialMatch.Split(new char[] { ' ', ','}, StringSplitOptions.RemoveEmptyEntries).ToList();
            // Loop through the words to make sure all but the last one matches a descriptive or classifier adjective
            foreach(string word in words)
            {
                // Trim the whitespace off of the word to make it easier to use
                word.Replace(" ", "");
                // If we're on the last word, and matches our name, then yes, we've found a match
                if (word == words.Last() && word == name)
                    return true;
                // Otherwise, we're probably still looping through the words, so make sure each word is one of the adjectives
                else if (!descriptiveAdjectives.Contains(word) && !classifierAdjectives.Contains(word))
                    return false;
            }
            return false;
        }
    }
}

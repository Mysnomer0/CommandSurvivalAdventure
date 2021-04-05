using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure
{
    namespace Processing
    {
        // This class handles command parsing and processing
        class Parser : CSABehaviour
        {
            // A hashset of words that can generally just be ignored in a string that is being parsed, such as articles
            public static HashSet<string> commonArticles = new HashSet<string>() { "a", "an", "the", "my" };
            // Initialize the parser
            public Parser(Application newApplication)
            {
                // Initialize the application
                attachedApplication = newApplication;
            }
            // Use this to give the parser raw string
            public void Parse(string stringToParse)
            {
                // If the string is empty, throw error now
                if (stringToParse.IsNullOrEmpty())
                    return;
                // First off, split the string into a list
                List<string> wordsInString = new List<string>(stringToParse.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                // The list of arguments
                List<string> arguments = new List<string>();
                // Get the list of arguments
                if(wordsInString.Count > 1)
                {
                    for (int i = 1; i < wordsInString.Count; i++)
                        arguments.Add(wordsInString[i]);
                }
                // Check if the first word, the command, is in the command database
                if (attachedApplication.commandDatabase.CheckCommand(wordsInString[0]))
                    // If so, run it with the arguments, if any
                    attachedApplication.commandDatabase.GetCommand(wordsInString[0]).Run(arguments);
                else
                    attachedApplication.output.PrintLine("$maUnrecognized $maverb " + wordsInString[0] + ". $ma:(");
            }

            #region Helper Parser Functions
            // Puts the index of the word we ended on plus 1 to skip the delimiter into the indexOfNextWord
            // Puts the substring without the delimiter word into the subStringOut
            // Useful for parsing a command that has it's arguments seperated by prepositions or certain words
            public static List<string> GetSubStringUpToWord(List<string> wordsToParse, int indexOfWordToStartOn, List<string> delimiterWords, ref int indexOfNextWord)
            {
                // The substring we will return
                List<string> subStringOut = new List<string>();
                // Starting at the index to start on, loop through the words and add them to the subStringOut until we hit the delimiter word
                for (int i = indexOfWordToStartOn; i < wordsToParse.Count; i++)
                {
                    // If we are on the last word, set the index of the next word
                    if(i == wordsToParse.Count - 1)
                        indexOfNextWord = i + 1;
                    // If we aren't on a delimiter word, keep adding the words to the sub string to output
                    if (!delimiterWords.Contains(wordsToParse[i]))
                        subStringOut.Add(wordsToParse[i]);
                    // If the word is one of the delimiter words, stop the loop
                    else
                    {
                        indexOfNextWord = i + 1;
                        break;
                    }
                }

                return subStringOut;
            }
            // Almost identical to the previous overload, just doesn't return the index of the word ended on
            public static List<string> GetSubStringUpToWord(List<string> wordsToParse, int indexOfWordToStartOn, List<string> delimiterWords)
            {
                // The substring we will return
                List<string> subStringOut = new List<string>();
                // Starting at the index to start on, loop through the words and add them to the subStringOut until we hit the delimiter word
                for (int i = indexOfWordToStartOn; i < wordsToParse.Count; i++)
                {
                    if (!delimiterWords.Contains(wordsToParse[i]))
                        subStringOut.Add(wordsToParse[i]);
                    // If the word is one of the delimiter words, stop the loop
                    else
                        break;
                }

                return subStringOut;
            }
            // Scrubs off any articles from the string
            public static string ScrubArticles(List<string> wordsToScrub)
            {
                // The scrubbed string
                string scrubbedString = "";
                // Loop through and scrub off any articles
                for(int i = 0; i < wordsToScrub.Count; i++)
                {
                    if (!commonArticles.Contains(wordsToScrub[i]) && i != wordsToScrub.Count - 1)
                        scrubbedString += wordsToScrub[i] + " ";
                    else if (!commonArticles.Contains(wordsToScrub[i]))
                        scrubbedString += wordsToScrub[i];
                }

                return scrubbedString;
            }
            #endregion
        }
    }
}

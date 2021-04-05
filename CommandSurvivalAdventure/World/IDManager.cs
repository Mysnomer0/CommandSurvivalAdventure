using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World
{
    // Manages generating unique IDs
    class IDManager
    {
        // The current highest ID; also, the amount of IDs that have been generated
        public int lastIDGenerated { get; private set; } = 0;

        // Generates a unique ID
        public int GenerateID()
        {
            int valueToReturn = lastIDGenerated;
            lastIDGenerated++;
            return valueToReturn;
        }
    }
}

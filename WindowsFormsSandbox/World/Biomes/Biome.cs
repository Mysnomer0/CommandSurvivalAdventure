using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World
{
    // This class encapuslates data pertaining to the different kinds of landscapes
    // There are also several generation functions to generate biome properties
    abstract class Biome
    {
        // The name of the biome
        public string name;
        // The average windspeed possible for this biome
        public float normalWindSpeed;
        // The normal temperature possible for this biome
        public float normalTemperature;
        // The color of the biome
        public string associatedColor;
        // Generates and populates the biome based on the seed
        public abstract void Generate(Chunk chunkToPopulate);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World
{
    // This class encapsulates all data and functionality pertaining to a mineral
    class Mineral : GameObject
    {
        // The hardness the mineral 0 = a water, 100 = diemond
        public float hardness;

        // The density of the mineral
        public float density;

        // transparency of the mineral 0 = air, 1 = stone
        public float transparency;

        // The poison level of the mineral
        public float radioactiveLevel;

        // lightEmiting Level of the mineral
        public float lightEmitingLevel;

        // The meltingPoint of the mineral
        public float meltingPoint;

    }
}

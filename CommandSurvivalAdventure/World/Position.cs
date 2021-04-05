using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World
{
    // Stores a basic 3d position
    class Position
    {
        public int x;
        public int y;
        public int z;
        public Position(int newX, int newY, int newZ)
        {
            x = newX;
            y = newY;
            z = newZ;
        }
        public static bool operator ==(Position position1, Position position2)
        {
            return (position1.x == position2.x && position1.y == position2.y && position1.z == position2.z);
        }
        public static bool operator !=(Position position1, Position position2)
        {
            return (position1.x != position2.x || position1.y != position2.y || position1.z != position2.z);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World
{
    // Stores a basic 3d position
    class Direction
    {
        // The direction vector values
        public int x;
        public int y;
        public int z;
        // Returns the opposite direction
        public static Direction GetOpposite(Direction direction)
        {
            // Create the new direction to return
            Direction directionToReturn = new Direction();
            // Based on the direction, return the corresponding string
            if (direction.x == 0 && direction.y == 0 && direction.z == 1)
            {
                directionToReturn.x = 0;
                directionToReturn.y = 0;
                directionToReturn.z = -1;
            }
            else if (direction.x == 1 && direction.y == 0 && direction.z == 1)
            {
                directionToReturn.x = -1;
                directionToReturn.y = 0;
                directionToReturn.z = -1;
            }
            else if (direction.x == 1 && direction.y == 0 && direction.z == 0)
            {
                directionToReturn.x = -1;
                directionToReturn.y = 0;
                directionToReturn.z = 0;
            }
            else if (direction.x == 1 && direction.y == 0 && direction.z == -1)
            {
                directionToReturn.x = -1;
                directionToReturn.y = 0;
                directionToReturn.z = 1;
            }
            else if (direction.x == 0 && direction.y == 0 && direction.z == -1)
            {
                directionToReturn.x = 0;
                directionToReturn.y = 0;
                directionToReturn.z = 1;
            }
            else if (direction.x == -1 && direction.y == 0 && direction.z == -1)
            {
                directionToReturn.x = 1;
                directionToReturn.y = 0;
                directionToReturn.z = 1;
            }
            else if (direction.x == -1 && direction.y == 0 && direction.z == 0)
            {
                directionToReturn.x = 1;
                directionToReturn.y = 0;
                directionToReturn.z = 0;
            }
            else if (direction.x == -1 && direction.y == 0 && direction.z == 1)
            {
                directionToReturn.x = 1;
                directionToReturn.y = 0;
                directionToReturn.z = -1;
            }
            return directionToReturn;
        }
        // Converts the string into the corresponding direction
        public static Direction StringToDirection(string direction)
        {
            // The new direction to return
            Direction directionToReturn = new Direction();
            // Based on the string, generate a direction
            if (direction == "north")
            {
                directionToReturn.x = 0;
                directionToReturn.y = 0;
                directionToReturn.z = 1;
            }
            else if (direction == "northeast")
            {
                directionToReturn.x = 1;
                directionToReturn.y = 0;
                directionToReturn.z = 1;
            }
            else if (direction == "east")
            {
                directionToReturn.x = 1;
                directionToReturn.y = 0;
                directionToReturn.z = 0;
            }
            else if (direction == "southeast")
            {
                directionToReturn.x = 1;
                directionToReturn.y = 0;
                directionToReturn.z = -1;
            }
            else if (direction == "south")
            {
                directionToReturn.x = 0;
                directionToReturn.y = 0;
                directionToReturn.z = -1;
            }
            else if (direction == "southwest")
            {
                directionToReturn.x = -1;
                directionToReturn.y = 0;
                directionToReturn.z = -1;
            }
            else if (direction == "west")
            {
                directionToReturn.x = -1;
                directionToReturn.y = 0;
                directionToReturn.z = 0;
            }
            else if (direction == "northwest")
            {
                directionToReturn.x = -1;
                directionToReturn.y = 0;
                directionToReturn.z = 1;
            }
            else
                return null;
            return directionToReturn;
        }
        // Converts the given direction to a string
        public static string DirectionToString(Direction direction)
        {
            // Based on the direction, return the corresponding string
            if (direction.x == 0 && direction.y == 0 && direction.z == 1)
            {
                return "north";
            }
            else if (direction.x == 1 && direction.y == 0 && direction.z == 1)
            {
                return "northeast";
            }
            else if (direction.x == 1 && direction.y == 0 && direction.z == 0)
            {
                return "east";
            }
            else if (direction.x == 1 && direction.y == 0 && direction.z == -1)
            {
                return "southeast";
            }
            else if (direction.x == 0 && direction.y == 0 && direction.z == -1)
            {
                return "south";
            }
            else if (direction.x == -1 && direction.y == 0 && direction.z == -1)
            {
                return "southwest";
            }
            else if (direction.x == -1 && direction.y == 0 && direction.z == 0)
            {
                return "west";
            }
            else if (direction.x == -1 && direction.y == 0 && direction.z == 1)
            {
                return "northwest";
            }
            return "";
        }
        // Converts the int into the corresponding direction
        public static Direction IntToDirection(int direction)
        {
            // The new direction to return
            Direction directionToReturn = new Direction();
            // Based on the string, generate a direction

            // North
            if (direction == 0)
            {
                directionToReturn.x = 0;
                directionToReturn.y = 0;
                directionToReturn.z = 1;
            }
            // Northeast
            else if (direction == 1)
            {
                directionToReturn.x = 1;
                directionToReturn.y = 0;
                directionToReturn.z = 1;
            }
            // East
            else if (direction == 2)
            {
                directionToReturn.x = 1;
                directionToReturn.y = 0;
                directionToReturn.z = 0;
            }
            // Southeast
            else if (direction == 3)
            {
                directionToReturn.x = 1;
                directionToReturn.y = 0;
                directionToReturn.z = -1;
            }
            // South
            else if (direction == 4)
            {
                directionToReturn.x = 0;
                directionToReturn.y = 0;
                directionToReturn.z = -1;
            }
            // Southwest
            else if (direction == 5)
            {
                directionToReturn.x = -1;
                directionToReturn.y = 0;
                directionToReturn.z = -1;
            }
            // West
            else if (direction == 6)
            {
                directionToReturn.x = -1;
                directionToReturn.y = 0;
                directionToReturn.z = 0;
            }
            // Northwest
            else if (direction == 7)
            {
                directionToReturn.x = -1;
                directionToReturn.y = 0;
                directionToReturn.z = 1;
            }
            return directionToReturn;
        }
        // Converts the given direction to an int
        public static int DirectionToInt(Direction direction)
        {
            // Based on the direction, return the corresponding string
            if (direction.x == 0 && direction.y == 0 && direction.z == 1)
            {
                return 0;
            }
            else if (direction.x == 1 && direction.y == 0 && direction.z == 1)
            {
                return 1;
            }
            else if (direction.x == 1 && direction.y == 0 && direction.z == 0)
            {
                return 2;
            }
            else if (direction.x == 1 && direction.y == 0 && direction.z == -1)
            {
                return 3;
            }
            else if (direction.x == 0 && direction.y == 0 && direction.z == -1)
            {
                return 4;
            }
            else if (direction.x == -1 && direction.y == 0 && direction.z == -1)
            {
                return 5;
            }
            else if (direction.x == -1 && direction.y == 0 && direction.z == 0)
            {
                return 6;
            }
            else if (direction.x == -1 && direction.y == 0 && direction.z == 1)
            {
                return 7;
            }
            return -1;
        }
    }
}

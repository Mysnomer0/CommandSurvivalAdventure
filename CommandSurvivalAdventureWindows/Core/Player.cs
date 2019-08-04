using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Core
{
    // This class encapsulates player account data
    class Player
    {
        // The name of the player
        public string name;
        // The password to authenticate the player
        public string password = "";
        // The reference to the GameObject that this player is controlling
        public World.GameObject controlledGameObject;
        // Initialize
        public Player(string newName)
        {
            name = newName;
        }
    }
}

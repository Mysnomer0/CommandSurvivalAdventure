using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Creatures
{
    // This class encapsulates all data and functionality pertaining to a creature body part, such as a head or leg, which will be appropriatly named
    class CreaturePart : GameObject
    {
        // TODO: All of these members are deprecated and moved to specialProperties so they can be accessed more globally
        // The weight of the creature, a way to determine the size as well
        public float weight;
        // The overall health of the creature, which is an averaged percentage of the health of all the creatures subparts
        public float health;
        // The muscle content of the creature, which is a sum of the muscle content of each subpart, in weight
        public float muscleContent;
        // The fat content of the creature, which is a sum of the fat content of each subpart, in weight
        public float fatContent;
        // Whether or not the creature is bleeding, anywhere
        public bool isBleeding = false;
        // Whether or not the creature is sick or unclean, on any part of the body
        public bool isUnclean = false;
        // Whether or not the part is cooked
        public bool isCooked = false;

        public override void Update()
        {
            base.Update();

            if (specialProperties["isBleeding"] == "TRUE" && float.Parse(specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) > 0)
                specialProperties["health"] = (float.Parse(specialProperties["health"], System.Globalization.CultureInfo.InvariantCulture) - 1).ToString();
        }
    }
}

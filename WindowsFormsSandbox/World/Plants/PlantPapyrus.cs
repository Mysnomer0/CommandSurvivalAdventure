using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World.Plants
{
    class PlantPapyrus : Plant
    {
        public PlantPapyrus()
        {
            identifier.name = "papyrus";
            specialProperties.Add("isFastenable", "");
            type = typeof(PlantPapyrus);
            importanceLevel = 5;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure
{
    // This is a simple class that any class in CSA that needs to run on an application inherits
    abstract class CSABehaviour
    {
        // The application that any CSABehaviour is running on
        public Application attachedApplication;
    }
}

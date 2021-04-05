using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CommandSurvivalAdventure.Processing
{
    // This is the base class for all commands
    class Command : CSABehaviour
    {
        // The command name
        public string name;
        // The code that runs the command
        public virtual void Run(List<string> arguments) { }
    }
}

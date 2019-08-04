using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Support.Networking.RPCs
{
    // Displays the message that was given
    class RPCSay : RPC
    {
        public override void Run(List<string> arguments, CSACore application)
        {
            // ARGS: <stringToOutput> <optionalSendersName>

            // Output the sender, if one was given
            if(arguments.Count == 2)
                // Output the string
                application.output.PrintLine(arguments[1] + "> " + arguments[0]);
            else
                // Output the string
                application.output.PrintLine(arguments[0]);
        }
        public RPCSay()
        {
            type = typeof(RPCSay);
        }
    }
}

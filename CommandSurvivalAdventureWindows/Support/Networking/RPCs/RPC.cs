using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Support.Networking
{
    // This class encapsulates the code to send to a client or server that you want the code to run on
    class RPC : CSABehaviour
    {
        // The type of RPC
        public Type type;
        // The arguments stored or given
        public List<string> arguments = new List<string>();
        // The player or server that sent this RPC
        public Core.Player sender;
        // The contained code in this RPC, run when the RPC is recieved
        public virtual void Run(List<string> givenArguments, CSACore application) { }
    }
}

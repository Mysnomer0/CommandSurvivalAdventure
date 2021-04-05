using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommandSurvivalAdventure.Support.Networking
{
    // This class is very similar to a normal input command, except that it is sent to the server to be run there
    class ServerCommand : CSABehaviour
    {
        // The name of the sender, which is stored along with the arguments
        public string nameOfSender;
        // The arguments stored or given
        public List<string> arguments = new List<string>();
        // The type of command
        public Type type;
        // The player that sent this command
        public World.GameObject sender;
        // The contained code in this RPC, run when the RPC is recieved
        public virtual void Run(List<string> givenArguments, Server server) { }
    }
}

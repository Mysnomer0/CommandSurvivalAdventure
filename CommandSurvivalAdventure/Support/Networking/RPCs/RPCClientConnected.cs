using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Support.Networking.RPCs
{
    // A RPC from the server to the client telling the client to connect
    class RPCClientConnect : RPC
    {
        public override void Run(List<string> arguments, Application application)
        {
            // Output the string
            application.output.PrintLine(Processing.Describer.ToColor("Authenticated!", "$ka"));
            // Set the authenticated bool
            application.client.isAuthenticated = true;
        }
        public RPCClientConnect()
        {
            type = typeof(RPCClientConnect);
        }
    }
}

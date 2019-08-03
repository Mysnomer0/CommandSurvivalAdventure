using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This is the client command that allows you to say things in game
    class CommandServer : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            System.Windows.Forms.Application.Run(new ServerWindow(attachedApplication));
            /*
            // Check the arguments
            if (arguments.Count > 0)
            {
                if (arguments[0] == "start" && arguments.Count >= 4)
                {
                    attachedApplication.output.PrintLine("Starting server...");
                    if(attachedApplication.server.Start(arguments[1], int.Parse(arguments[2]), arguments[3]))
                        attachedApplication.output.PrintLine("$kaServer $kaup $kaand $karunning!");
                }
                else if (arguments[0] == "start")
                    attachedApplication.output.PrintLine("Usage: server start <brokerAddress> <port> <serverName>");
                else if (arguments[0] == "stop")
                {
                    attachedApplication.server.Stop();
                    attachedApplication.output.PrintLine("Server stopped.");
                }
                else if(arguments[0] == "help")
                {
                    attachedApplication.output.PrintLine("\tServer");
                    attachedApplication.output.PrintLine("Description: Allows you to control your local CSA server with the commands below.\n\n");
                    attachedApplication.output.PrintLine("start <brokerAddress> <port> <serverName> : Starts the server by connecting it to the given host and naming it.");
                    attachedApplication.output.PrintLine("stop : Stops the server.");
                }
            }
            else
                attachedApplication.output.PrintLine("No arguments given. Try server help for list of commands.");
            */
        }
        // Initialize the command
        public CommandServer(CSACore application)
        {
            attachedApplication = application;
        }
    }
}

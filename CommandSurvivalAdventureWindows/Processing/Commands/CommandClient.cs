using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommandSurvivalAdventure.Processing.Commands
{
    // This is the client command that allows you to say things in game
    class CommandClient : Command
    {
        // Run the command
        public override void Run(List<string> arguments)
        {
            // Check the arguments
            if (arguments.Count > 0)
            {
                if (arguments[0] == "connect" && arguments.Count >= 5)
                {
                    attachedApplication.output.PrintLine("Connecting to " + arguments[4] + "...");
                    if(attachedApplication.client.Connect(arguments[1], int.Parse(arguments[2]), attachedApplication.client.clientID, arguments[4]))
                    {
                        // Send a connection request to the server
                        Support.Networking.ServerCommands.ServerCommandClientConnectRequest connectRequest = new Support.Networking.ServerCommands.ServerCommandClientConnectRequest(attachedApplication.client.clientID);
                        // Set the requested name
                        connectRequest.arguments.Add(arguments[3]);
                        // Send the request
                        attachedApplication.client.SendServerCommand(connectRequest);
                        // Reset the isAuthenticated since we're making a new connection
                        attachedApplication.client.isAuthenticated = false;
                        // Wait for a response, and time out if necessary
                        for(int i = 0; i < 20; i++)
                        {
                            attachedApplication.output.PrintLine("...");
                            // Wait
                            Thread.Sleep(500);
                            // If we're authenticated, finish the connection
                            if (attachedApplication.client.isAuthenticated)
                            {
                                // Reset the client and reconnect with the correct client ID
                                attachedApplication.client = new Support.Networking.Client(attachedApplication);
                                attachedApplication.client.Connect(arguments[1], int.Parse(arguments[2]), arguments[3], arguments[4]);
                                // Affirm to the user that we have connected
                                attachedApplication.output.PrintLine(Describer.ToColor("Connected to server!", "$ka"));

                                // Send a look command

                                // Create a new server command
                                Support.Networking.ServerCommands.ServerCommandLook serverCommand = new Support.Networking.ServerCommands.ServerCommandLook(arguments[3]);
                                // Send a look request to the server
                                attachedApplication.client.SendServerCommand(serverCommand);

                                break;
                            }
                            // Otherwise, if we've hit the end, time out
                            else if(i == 19)
                            {
                                attachedApplication.output.PrintLine(Describer.ToColor("Connection request timed out. :(", "$ma"));
                            }
                        }
                    }
                }
                else if (arguments[0] == "connect")
                    attachedApplication.output.PrintLine("Usage: client connect <brokerAddress> <port> <clientID> <serverName>");
                else if (arguments[0] == "disconnect")
                {
                    attachedApplication.client.Disconnect();
                    attachedApplication.output.PrintLine("Disconnected from server.");
                }
                else if(arguments[0] == "help")
                {
                    attachedApplication.output.PrintLine("\tClient");
                    attachedApplication.output.PrintLine("Description: Allows you to connect to a server.\n\n");
                    attachedApplication.output.PrintLine("connect <brokerAddress> <port> <clientID> <serverName> : Connects up to the server by connecting to the broker and subscribing to the server's topic.");
                    attachedApplication.output.PrintLine("disconnect : Disconnects from the server.");
                }
            }
            else
                attachedApplication.output.PrintLine("No arguments given. Try client help for list of commands.");
        }
        // Initialize the command
        public CommandClient(CSACore application)
        {
            attachedApplication = application;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.MQTT;
using Newtonsoft.Json;
using System.Threading;

namespace CommandSurvivalAdventure.Support.Networking
{
    // This class encapsulates all the data and functionality for a CSA server
    // This is the host that clients can send commands to and recieve world data from
    class Server : CSABehaviour
    {
        // The server ID, which is also the topic name that the server publishes to and all clients subscribe to
        public string sendCommandsTopic;
        // The topic on which the server recieves commands from clients
        public string recieveCommandsTopic;
        // Whether or not not the server is up or not
        public bool isRunning
        {
            get
            {
                return networkingManager.isConnected;
            }
        }
        // The networking manager this server is using
        private NetworkingManager networkingManager;
        // The world that is running on this server
        public World.World world;
        // Initialize 
        public Server(CSACore application)
        {
            attachedApplication = application;
        }
        // Starts the server; Make sure to give the server a unique ID
        public bool Start(string brokerAddress, int brokerPort, string serverID)
        {
            // Create a new networking manager
            networkingManager = new NetworkingManager(attachedApplication);
            // Connect to the broker
            bool connectedToBroker = networkingManager.Connect(brokerAddress, brokerPort, serverID, 10);
            // If the connection was successful
            if(connectedToBroker)
            {
                // Generate a new world
                attachedApplication.output.PrintLine("Generating new world...");
                world = new World.World(new Random().Next());
                // Start the new world in a thread
                //Thread worldThread = new Thread(world.Start);
                //worldThread.Start();
                // Setup the send and recieve topics
                recieveCommandsTopic = serverID + "/Recieve";
                networkingManager.Subscribe(recieveCommandsTopic);
                sendCommandsTopic = serverID + "/Send";
                // Add our OnRecieveCommand event to the networking manager so we can recieve messages
                networkingManager.MessageRecieved += OnRecieveCommand;
            }
            return connectedToBroker;
        }
        // Stops the server
        public void Stop()
        {
            networkingManager.Disconnect();
        }
        // Sends a RPC to all clients
        public void SendRPC(RPC RPCToSend)
        {
            // Publish this new message on the send topic
            networkingManager.Publish(sendCommandsTopic, JsonConvert.SerializeObject(RPCToSend));
        }
        // Sends a RPC to the client client with the given ID
        public void SendRPC(RPC RPCToSend, string clientID)
        {
            // Publish this new message on the send topic
            networkingManager.Publish(sendCommandsTopic + "/" + clientID, JsonConvert.SerializeObject(RPCToSend));
        }
        // Sends a RPC to everyone at a position except for the given clientID
        public void SendRPC(RPC RPCToSend, World.Position position, List<string> clientIDsToIgnore)
        {            
            // Find everyone in the same chunk and let them know
            foreach (KeyValuePair<string, Core.Player> playerEntry in world.players)
            {
                // If this player is at the same position but doesn't have the same name as the sender
                if (!clientIDsToIgnore.Contains(playerEntry.Key) && playerEntry.Value.controlledGameObject.position == position)
                    // Send an informational RPC to them letting them know
                    SendRPC(RPCToSend, playerEntry.Key);
            }
        }
        // Called whenever the server recieves a command
        private void OnRecieveCommand(object source, NetworkingManager.OnMessageRecievedEventArguments eventArguments)
        {
            // Run the new command in a new thread
            new Thread( delegate () { ThreadRecieveCommand(eventArguments); } ).Start();
        }
        // The thread that runs the command
        private void ThreadRecieveCommand(NetworkingManager.OnMessageRecievedEventArguments eventArguments)
        {
            #region Convert the JSON back to the server command
            // Convert the payload into an actual server command
            ServerCommand recievedServerCommand = JsonConvert.DeserializeObject<ServerCommand>(Encoding.Default.GetString(eventArguments.payload));
            // Convert the server command into the actual command that it is, rather than the base ServerCommand class, so we know which Run function to use
            dynamic convertedRecievedServerCommand = Activator.CreateInstance(recievedServerCommand.type);
            #endregion

            #region Set the variables on the new server command
            // Set the name of the sender
            convertedRecievedServerCommand.nameOfSender = recievedServerCommand.nameOfSender;
            // Set the type
            convertedRecievedServerCommand.type = recievedServerCommand.type;
            // Set the arguments
            convertedRecievedServerCommand.arguments = recievedServerCommand.arguments;
            // Set it's attached application to give it access to the program classes and databases
            convertedRecievedServerCommand.attachedApplication = attachedApplication;

            #endregion

            #region Run the server command
            // So, if the world has the player on it, this is a command being run by a player
            if (world.players.ContainsKey(recievedServerCommand.nameOfSender))
            {
                // Set the sender gameObject on the server command
                convertedRecievedServerCommand.sender = world.players[recievedServerCommand.nameOfSender].controlledGameObject;

                // Run the command once it is ready in the queue

                // Put this server command on the queue of the sender trying to run the command
                convertedRecievedServerCommand.sender.commandQueue.Enqueue(convertedRecievedServerCommand);
                // Go into an infinite loop, waiting for the command either to be removed from the queue, or the command to come to the top to be executed
                while (true)
                {
                    // If the command is the next to run in the queue, remove it from the queue and return true, letting the thread move on
                    if(convertedRecievedServerCommand.sender.commandQueue.Peek().type == convertedRecievedServerCommand.type)
                    {
                        if(convertedRecievedServerCommand.sender.commandQueue.Peek() == convertedRecievedServerCommand)
                        {
                            // Run the server command
                            convertedRecievedServerCommand.Run(convertedRecievedServerCommand.arguments, this);
                            // Take it off the queue of commands, since it just got run
                            convertedRecievedServerCommand.sender.commandQueue.Dequeue();
                            // Stop the loop
                            break;
                        }
                    }
                    // Otherwise, if the server command gets removed from the queue at any point, stop the loop
                    else if (!convertedRecievedServerCommand.sender.commandQueue.Contains(convertedRecievedServerCommand))
                        break;
                }
            }
            // Otherwise, this is probably someone pinging the server or just trying to connect
            else
            {
                // Run the server command
                convertedRecievedServerCommand.Run(convertedRecievedServerCommand.arguments, this);
            }
            #endregion
        }
    }
}

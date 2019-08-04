using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CommandSurvivalAdventure.Support.Networking
{
    // This class encapsulates the functionality for a client that can send, recieve, and execute commands from and to the server
    class Client : CSABehaviour
    {
        // The clients ID
        public string clientID = new Random().Next().ToString();
        // The topic name that the server subscribes to and all clients publish to
        public string sendCommandsTopic;
        // The topic on which the client recieves commands, usually <serverID>/Send
        public string recieveCommandsTopic;
        // The topic on which the client directly recieves exclusive commands
        public string directRecieveCommandsTopic;
        // Whether or not the client has been authenticated.  Do not change this variable during normal use, this is only for RPCs to set
        public bool isAuthenticated = false;
        // Whether or not not the client is connected to a server or not
        public bool isConnected
        {
            get
            {
                return networkingManager.isConnected;
            }
        }
        // The networking manager this client is using
        private NetworkingManager networkingManager;
        // Initialize
        public Client(CSACore application)
        {
            attachedApplication = application;
            networkingManager = new NetworkingManager(application);
        }
        // Connects this client up to a server; Make sure to give the client a unique ID
        public bool Connect(string brokerAddress, int brokerPort, string newClientID, string serverID)
        {
            // Create a new networking manager
            networkingManager = new NetworkingManager(attachedApplication);
            // Connect to the broker
            bool connectedToBroker = networkingManager.Connect(brokerAddress, brokerPort, newClientID, 10);
            // If the connection was successful
            if (connectedToBroker)
            {
                clientID = newClientID;
                // Setup the send and recieve topics
                sendCommandsTopic = serverID + "/Recieve";
                recieveCommandsTopic = serverID + "/Send";
                directRecieveCommandsTopic = recieveCommandsTopic + "/" + newClientID;
                networkingManager.Subscribe(recieveCommandsTopic);
                networkingManager.Subscribe(directRecieveCommandsTopic);
                // Add our OnRecieveCommand event to the networking manager so we can recieve messages
                networkingManager.MessageRecieved += OnRecieveRPC;
            }
            return connectedToBroker;
        }
        // Disconnects from server
        public void Disconnect()
        {
            networkingManager.Disconnect();
        }
        // Sends a command to all clients
        public void SendServerCommand(ServerCommand serverCommandToSend)
        {
            // Publish the server command as a JSON string on the send topic
            networkingManager.Publish(sendCommandsTopic, JsonConvert.SerializeObject(serverCommandToSend));
        }
        // Called whenever the client recieves a command
        private void OnRecieveRPC(object source, NetworkingManager.OnMessageRecievedEventArguments eventArguments)
        {
            // Convert the payload into an actual message
            RPC recievedRPC = JsonConvert.DeserializeObject<RPC>(Encoding.Default.GetString(eventArguments.payload));
            // Convert the RPC into the actual rpc that it is, rather than the base RPC class, so we know which Run function to use
            dynamic dynamicRPC = Activator.CreateInstance(recievedRPC.type);
            // Run the RPC
            dynamicRPC.Run(recievedRPC.arguments, attachedApplication);
        }
        // Changes the clientID
        public void ChangeClientID(string newClientID)
        {
            // Unsubscribe from the old direct recieve topic
            networkingManager.Unsubscribe(directRecieveCommandsTopic);
            // Set our new client ID
            clientID = newClientID;
            // Create the new direct recive topic
            directRecieveCommandsTopic = recieveCommandsTopic + "/" + newClientID;
            // Subscribe to it
            networkingManager.Subscribe(directRecieveCommandsTopic);
        }
    }
}

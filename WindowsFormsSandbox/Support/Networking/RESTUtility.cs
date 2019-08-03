using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using OpenNETCF.MQTT;
using System.Net.Http;

namespace CommandSurvivalAdventure.Support.Networking
{
    // This class manages all basic networking for the program, such as sending and recieving JSON strings
    class RESTUtility : CSABehaviour
    {
        // This class uses REST API with the HTTP protocol to send and recieve messages

        // The HTTP message handler class
        class MessageRecievedHandler : HttpClientHandler
        {
            // Handles the recieved messages by basically just forwarding it to the corresponding public event
                        
        }
        // The event arguments for recieving a message
        public class OnMessageRecievedEventArguments : EventArgs
        {
            // The path that the message was recieved on
            public string path;
            // The payload of the message recieved
            public byte[] payload;
            // Initialize
            public OnMessageRecievedEventArguments(string newPath, byte[] newPayload)
            {
                path = newPath;
                payload = newPayload;
            }
        }  
        // This is the event handler for recieving messages
        public delegate void OnMessageRecievedEventHandler(object source, OnMessageRecievedEventArguments eventArguments);
        // This event is called when a message is recieved
        public event OnMessageRecievedEventHandler MessageRecieved;
        // The HTTP client instance
        private HttpClient client;

        // Initialize
        public RESTUtility(CSACore newApplication)
        {
            // Initialize the application
            attachedApplication = newApplication;
        }
        // Begins listening for REST calls
        public void StartListening()
        {
            
        }
        // Makes a get request to the given address
        public string Get(string address)
        {
            return null;
        }
        // Posts the given data to the given address
        public void Post(string address, string payload)
        {

        }
    }
}

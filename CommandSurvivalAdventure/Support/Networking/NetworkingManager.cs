using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using OpenNETCF.MQTT;

namespace CommandSurvivalAdventure.Support.Networking
{
    // This class manages all basic networking for the program, such as sending and recieving JSON strings
    class NetworkingManager : CSABehaviour
    {
        // This class uses the Publish-Subscribe pattern to send and recieve messages
        // This class is intended to be a wrapper class for your desired MQTT API
        // This should hopefully make it easy to plug in and out an MQTT backend

        // The event arguments for recieving a message
        public class OnMessageRecievedEventArguments : EventArgs
        {
            // The topic of the message recieved
            public string topic;
            // The payload of the message recieved
            public byte[] payload;
            // Initialize
            public OnMessageRecievedEventArguments(string newTopic, byte[] newPayload)
            {
                topic = newTopic;
                payload = newPayload;
            }
        }  
        // This is the event handler for recieving messages
        public delegate void OnMessageRecievedEventHandler(object source, OnMessageRecievedEventArguments eventArguments);
        // This event is called when a message is recieved
        public event OnMessageRecievedEventHandler MessageRecieved;
        // Whether or not the manager is connected
        public bool isConnected
        {
            get
            {
                if (client == null)
                    return false;
                return client.IsConnected;
            }
        }
        // The MQTT client instance
        private MQTTClient client;
            
        // Initialize
        public NetworkingManager(Application newApplication)
        {
            // Initialize the application
            attachedApplication = newApplication;
        }
        // Use this to connect up to the given MQTT broker
        public bool Connect(string brokerAddress, int port, string clientID, int amountOfRetries)
        {
            //attachedApplication.output.PrintLine("Connecting to " + brokerAddress + ":" + port + "...");
            // Create a new MQTT client 
            client = new MQTTClient(brokerAddress, port);
            // Register to message received 
            client.MessageReceived += OnMessageRecievedLocal;
            // Connect up the client
            client.Connect(clientID);
            // Wait for the connection to complete
            while (!client.IsConnected)
            {
                Thread.Sleep(100);
                if(amountOfRetries <= 0)
                {
                    attachedApplication.output.PrintLine("$maFailed $mato $maconnect... $ma:(");
                    return false;
                }
                amountOfRetries--;
            }
            return true;
        }
        // This is a helper function that listens for when we recieve a message, then relays that to OnMessageRecieved event
        private void OnMessageRecievedLocal(string topic, QoS qos, byte[] payload)
        {
            //attachedApplication.output.PrintLine(topic + " : " + Encoding.Default.GetString(payload), IO.Output.OutputColor.YELLOW);
            OnMessageRecieved(topic, payload);
        }
        // This event is called when a message is recieved.  Feel free to add listeners
        protected virtual void OnMessageRecieved(string topic, byte[] payload)
        {
            if (MessageRecieved != null)
                MessageRecieved(this, new OnMessageRecievedEventArguments(topic, payload));
        }
        // Use this to disconnect
        public void Disconnect()
        {
            client.Disconnect();
        }
        // Use this to subscribe to a topic
        public void Subscribe(string topicToSubscribeTo)
        {
            // Add the subscription
            client.Subscriptions.Add(new Subscription(topicToSubscribeTo));
        }
        // Use this to unsubscribe to a topic
        public void Unsubscribe(string topicToUnsubscribeTo)
        {
            // Unsubscribe to the given topic
            client.Subscriptions.Remove(topicToUnsubscribeTo);
        }
        // Use this to publish a payload(message) to a topic
        public void Publish(string topicToPublishTo, string payload)
        {
            // Publish the payload to the topic
            client.Publish(topicToPublishTo, payload, QoS.FireAndForget, false);
        }
    }
    
}

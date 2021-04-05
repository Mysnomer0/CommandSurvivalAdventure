using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CommandSurvivalAdventure
{
    // This class operates at the lowest level in the program, handling class structure initialization
    class Application
    {
        // The various microservices of CSA, all as members to make easy access to each by all
        public Support.Networking.Server server;
        public Support.Networking.Client client;
        public IO.Input input;
        public IO.Output output;
        public Processing.Parser parser;
        public Processing.CommandDatabase commandDatabase;
        public World.Crafting.CraftingDatabase recipeDatabase;
        // Initializes the core
        public Application()
        {
            // Create the various supporting classes
            server = new Support.Networking.Server(this);
            client = new Support.Networking.Client(this);
            input = new IO.Input(this);
            output = new IO.Output(this);
            parser = new Processing.Parser(this);
            commandDatabase = new Processing.CommandDatabase(this);
            recipeDatabase = new World.Crafting.CraftingDatabase(this);

            // Print the welcome message
            output.PrintLine("$liWelcome $lito $liCommand $liSurvival $liAdventure!");
            output.PrintLine("");
            output.PrintLine("Use \"client connect\" to connect to a server to start playing.");
            output.PrintLine("Use \"server start\" to start your own local server.");
            output.PrintLine("Example:");
            output.PrintLine("");
            output.PrintLine("server start test.mosquitto.org 1883 TheServer");
            output.PrintLine("client connect test.mosquitto.org 1883 MyName TheServer");
            output.PrintLine("");
            //output.PrintLine("Type \"help\" for a list of commands.");
        }   
    }
}

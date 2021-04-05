using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.Processing
{
    // This class contains the database of commands
    class CommandDatabase : CSABehaviour
    {
        // The dictionary of commands
        private Dictionary<string, Command> commands;

        // Returns the command given the name
        public Command GetCommand(string nameOfCommand)
        {
            return commands[nameOfCommand];
        }
        // Returns whether or not the given command exists
        public bool CheckCommand(string nameOfCommand)
        {
            return commands.ContainsKey(nameOfCommand);
        }
        // Initialize
        public CommandDatabase(Application newApplication)
        {
            // Set the application
            attachedApplication = newApplication;

            // Initialize the dictionary of commands
            commands = new Dictionary<string, Command>();
            commands.Add("say", new Commands.CommandSay(attachedApplication));
            commands.Add("exit", new Commands.CommandExit(attachedApplication));
            commands.Add("server", new Commands.CommandServer(attachedApplication));
            commands.Add("client", new Commands.CommandClient(attachedApplication));
            commands.Add("look", new Commands.CommandLook(attachedApplication));
            commands.Add("go", new Commands.CommandGo(attachedApplication));
            commands.Add("walk", new Commands.CommandGo(attachedApplication));
            commands.Add("hug", new Commands.CommandHug(attachedApplication));
            commands.Add("pick", new Commands.CommandTake(attachedApplication));
            commands.Add("take", new Commands.CommandTake(attachedApplication));
            commands.Add("drop", new Commands.CommandDrop(attachedApplication));
            commands.Add("craft", new Commands.CommandCraft(attachedApplication));
            commands.Add("make", new Commands.CommandCraft(attachedApplication));
            commands.Add("fasten", new Commands.CommandAttach(attachedApplication));
            commands.Add("attach", new Commands.CommandAttach(attachedApplication));
            commands.Add("sharpen", new Commands.CommandSharpen(attachedApplication));
            commands.Add("whereami", new Commands.CommandWhereAmI(attachedApplication));
            commands.Add("inventory", new Commands.CommandInventory(attachedApplication));
            commands.Add("i", new Commands.CommandInventory(attachedApplication));
            commands.Add("stand", new Commands.CommandStand(attachedApplication));
            commands.Add("crouch", new Commands.CommandCrouch(attachedApplication));
            commands.Add("c", new Commands.CommandCrouch(attachedApplication));
            commands.Add("lie", new Commands.CommandLie(attachedApplication));
            commands.Add("lunge", new Commands.CommandLunge(attachedApplication));
            commands.Add("l", new Commands.CommandLunge(attachedApplication));
            commands.Add("dull", new Commands.CommandDull(attachedApplication));
            commands.Add("blunt", new Commands.CommandDull(attachedApplication));
            commands.Add("strike", new Commands.CommandStrike(attachedApplication));
            commands.Add("s", new Commands.CommandStrike(attachedApplication));
            commands.Add("hit", new Commands.CommandStrike(attachedApplication));
            commands.Add("block", new Commands.CommandBlock(attachedApplication));
            commands.Add("b", new Commands.CommandBlock(attachedApplication));
            commands.Add("put", new Commands.CommandPut(attachedApplication));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Linq;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandBlock : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <(l)eft|(r)ight|(h)ead>

            // If dead
            if (sender.specialProperties["isDeceased"] == "TRUE")
            {
                RPCs.RPCSay failure = new RPCs.RPCSay();
                failure.arguments.Add("You are deceased.");
                server.SendRPC(failure, nameOfSender);
                return;
            }

            // The object to block with
            World.GameObject objectToBlockWith = null;

            #region Validate the argument
            if (givenArguments[0] != "left"
                && givenArguments[0] != "right"
                && givenArguments[0] != "head"
                && givenArguments[0] != "l"
                && givenArguments[0] != "r"
                && givenArguments[0] != "h")
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("\"" + givenArguments[0] + "\" is not a valid direction to block. Use \"left\", \"right\", or \"head\".");
                server.SendRPC(error, nameOfSender);
                return;
            }
            // Translate the short hand of the direction to block if one was used
            if (givenArguments[0] == "l")
                givenArguments[0] = "left";
            else if (givenArguments[0] == "r")
                givenArguments[0] = "right";
            else if (givenArguments[0] == "h")
                givenArguments[0] = "head";
            #endregion

            #region Get the object to block with
            // Make sure there is an object to block with
            if (sender.FindChildrenWithSpecialProperty("isUtilizable").Count <= 0)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("You have nothing to block with.");
                server.SendRPC(error, nameOfSender);
                return;
            }
            // Loop through all the utilizable objects on the sender
            foreach(World.GameObject potentialObjectToUse in sender.FindChildrenWithSpecialProperty("isUtilizable"))
            {
                // If this potential object's thing being held matches, use it
                if (potentialObjectToUse.children.Count > 0)
                {
                    objectToBlockWith = potentialObjectToUse.children.First();
                    break;
                }
            }
            // If the loop didn't find anything that the sender is holding to block with, just use the first utilizable part
            if (objectToBlockWith == null)
                objectToBlockWith = sender.FindChildrenWithSpecialProperty("isUtilizable").First();
            #endregion

            #region Set the attribute on the object to block with, and set a timer to cancel it
            objectToBlockWith.specialProperties["blocking"] = givenArguments[0];
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Thread.Sleep(2000);
                objectToBlockWith.specialProperties["blocking"] = "NULL";

                RPCs.RPCSay rpcToSenderForReset = new RPCs.RPCSay();
                RPCs.RPCSay rpcToEveryoneElseForReset = new RPCs.RPCSay();

                rpcToSenderForReset.arguments.Add("You let down your guard.");
                rpcToEveryoneElseForReset.arguments.Add(nameOfSender + " let down their guard.");

                server.SendRPC(rpcToSenderForReset, nameOfSender);
                server.SendRPC(rpcToEveryoneElseForReset, sender.position, new List<string>() { nameOfSender });
            }).Start();
            #endregion

            #region Confirm the block
            RPCs.RPCSay rpcToSender = new RPCs.RPCSay();
            RPCs.RPCSay rpcToEveryoneElse = new RPCs.RPCSay();

            rpcToSender.arguments.Add("You blocked to your " + givenArguments[0] + " with your " + objectToBlockWith.identifier.fullName + "!");
            rpcToEveryoneElse.arguments.Add(nameOfSender + " blocked to the " + givenArguments[0] + " with " + Processing.Describer.GetArticle(objectToBlockWith.identifier.fullName) + " " + objectToBlockWith.identifier.fullName + "!");

            server.SendRPC(rpcToSender, nameOfSender);
            server.SendRPC(rpcToEveryoneElse, sender.position, new List<string>() { nameOfSender });
            #endregion
        }
        public ServerCommandBlock(string nameOfSender)
        {
            type = typeof(ServerCommandBlock);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandBlock()
        {
            type = typeof(ServerCommandBlock);
        }
    }
}

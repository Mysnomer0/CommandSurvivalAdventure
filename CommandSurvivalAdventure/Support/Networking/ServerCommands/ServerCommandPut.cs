using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CommandSurvivalAdventure.Support.Networking.ServerCommands
{
    // Tells the server to send an RPC to all people on the current chunk to display a message
    class ServerCommandPut : ServerCommand
    {
        public override void Run(List<string> givenArguments, Server server)
        {
            // ARGS: <nameOfObject> <nameOfContainer>

            // The object itself
            World.GameObject objectToPut = server.world.FindFirstGameObject(givenArguments[0], sender.position);
            // The container to put the object into
            World.GameObject containerToPutInto = server.world.FindFirstGameObject(givenArguments[1], sender.position);

            #region Make sure the objects exist
            // First off, make sure the crafting recipe exists
            if (containerToPutInto == null)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("The " + givenArguments[1] + " is not nearby.");
                server.SendRPC(error, nameOfSender);
                return;
            }
            else if(objectToPut == null)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("The " + givenArguments[0] + " is not nearby.");
                server.SendRPC(error, nameOfSender);
                return;
            }
            #endregion

            #region Make sure the container really is a container
            if(!containerToPutInto.specialProperties.ContainsKey("isContainer"))
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("The " + givenArguments[1] + " is not a container.");
                server.SendRPC(error, nameOfSender);
                return;
            }
            #endregion

            #region Make sure the object can really be put in the container
            if (objectToPut.parent.type == typeof(World.Plants.PlantRope))
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("The " + givenArguments[1] + " is attached to something.");
                server.SendRPC(error, nameOfSender);
                return;
            }
            else if (objectToPut.parent == containerToPutInto)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("The " + givenArguments[1] + " is already inside the " + containerToPutInto.identifier.fullName + ".");
                server.SendRPC(error, nameOfSender);
                return;
            }
            else if (objectToPut == containerToPutInto)
            {
                RPCs.RPCSay error = new RPCs.RPCSay();
                error.arguments.Add("You can't put the " + givenArguments[1] + " inside itself!.");
                server.SendRPC(error, nameOfSender);
                return;
            }
            #endregion

            #region Put the object inside the container
            // Add it as a child
            containerToPutInto.AddChild(objectToPut);
            #endregion

            #region Confirm that the object was put into the container
            // Confirm to the sender that they crafted the object
            RPCs.RPCSay confirmation = new RPCs.RPCSay();
            confirmation.arguments.Add("You put " + Processing.Describer.GetArticle(objectToPut.identifier.fullName) + " " + objectToPut.identifier.fullName + " into  " + Processing.Describer.GetArticle(containerToPutInto.identifier.fullName) + " " + containerToPutInto.identifier.fullName + ".");
            // Let everyone else in the chunk know that this player crafted something
            RPCs.RPCSay information = new RPCs.RPCSay();
            information.arguments.Add(nameOfSender + " put " + Processing.Describer.GetArticle(objectToPut.identifier.fullName) + " " + objectToPut.identifier.fullName + " into  " + Processing.Describer.GetArticle(containerToPutInto.identifier.fullName) + " " + containerToPutInto.identifier.fullName + ".");
            // Send the RPCs out
            server.SendRPC(confirmation, nameOfSender);
            server.SendRPC(information, sender.position, new List<string>() { nameOfSender });
            #endregion
        }
        public ServerCommandPut(string nameOfSender)
        {
            type = typeof(ServerCommandPut);
            this.nameOfSender = nameOfSender;
        }
        public ServerCommandPut()
        {
            type = typeof(ServerCommandPut);
        }
    }
}

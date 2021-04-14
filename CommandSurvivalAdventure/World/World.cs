using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading;
using System.Linq;

namespace CommandSurvivalAdventure.World
{
    // This class encapsulates all the data and data management functions for a game world
    class World : GameObject
    {
        // The seed of the world
        public int seed;
        // The water level of the world
        public float waterLevel;
        // The world width and length
        public int worldSize = 10;
        // The render distance
        public int renderDistance = 5;
        // The list of players on this world
        public Dictionary<string, Core.Player> players = new Dictionary<string, Core.Player>();
        // The 3D dictionary storing all the chunks on the world
        public Dictionary<int, Dictionary<int, Dictionary<int, Chunk>>> chunks;
        // The dictionary mapping all gameObject IDs to their corresponding gameObjects
        public Dictionary<int, GameObject> gameObjectDictionary = new Dictionary<int, GameObject>();
        // The ID manager for the world gameObjects
        public IDManager iDManager = new IDManager();
        // Initializes the world
        public World(Application newApplication, int newSeed)
        {
            attachedApplication = newApplication;
            // Add the properties to the world
            identifier.name = "world";
            seed = newSeed;
            waterLevel = new Random(newSeed).Next(60);
            // Generate the initial chunk
            Chunk chunk = new Chunk(attachedApplication);
            AddChild(chunk);
            chunk.Generate(0, 0, 0, seed, this);
            // Initialize the chunks
            chunks = new Dictionary<int, Dictionary<int, Dictionary<int, Chunk>>>();
            chunks.Add(0, new Dictionary<int, Dictionary<int, Chunk>>());
            chunks[0].Add(0, new Dictionary<int, Chunk>());
            chunks[0][0].Add(0, chunk);

            // Render around the new position
            // TODO: Render on the y axis as well, underground stuff
            for (int x = -worldSize / 2; x < worldSize / 2; x++)
            {
                for (int z = -worldSize / 2; z < worldSize / 2; z++)
                {
                    // If a chunk doesn't exist at the new position, generate one.  
                    GenerateChunkIfNecessary(new Position(x, 0, z));
                }
            }
        }

        #region Overriden GameObject API
        // Starts the world
        public override void Start()
        {
            // Start all chunks
            for (int x = -worldSize / 2; x < worldSize / 2; x++)
            {
                for (int z = -worldSize / 2; z < worldSize / 2; z++)
                {
                    chunks[x][0][z].Start();
                    //chunks[x][y][z].Start();
                    
                }
            }
            // Start the update cycle
            Update();
        }
        // Updates the world
        public override void Update()
        {
            // Update all chunks
            for (int x = -worldSize / 2; x < worldSize / 2; x++)
            {
                for (int z = -worldSize / 2; z < worldSize / 2; z++)
                {
                    //chunks[x][y][z].Update();
                    chunks[x][0][z].Update();                   
                }
            }
            // Wait a bit, then continue the update cycle
            Thread.Sleep(1000);
            Update();
        }
        // Adds a new gameObject to the world, gives it an ID, and adds it to the gameObject dictionary for easy reference
        public override void AddChild(GameObject newChild)
        {
            // If the new child hasn't been registered yet, register it
            if (!newChild.specialProperties.ContainsKey("isRegistered"))
            {
                children.Add(newChild);
                newChild.parent = this;
                // Give the child an ID
                newChild.ID = iDManager.GenerateID();
                // Add it to the dictionary for easy reference
                gameObjectDictionary.Add(newChild.ID, newChild);
                newChild.specialProperties.Add("isRegistered", "");
            }
        }
        // Destroys the gameObject by removing it from the children and from the gameObject dictionary as well
        public override void RemoveChild(GameObject childToRemove)
        {
            // Remove the child from both our hashset and the dictionary
            children.Remove(childToRemove);
            gameObjectDictionary.Remove(childToRemove.ID);
        }
        #endregion

        #region Chunk API
        // Generates a new chunk at the specified position if it doesn't already exist
        public void GenerateChunkIfNecessary(Position position)
        {
            // If a chunk doesn't exist at the new position, generate one.  This is NOT the cleanest way to do it, but I just needed a quick work around
            if (!chunks.ContainsKey(position.x))
            {
                chunks.Add(position.x, new Dictionary<int, Dictionary<int, Chunk>>());
                chunks[position.x].Add(position.y, new Dictionary<int, Chunk>());
                // Create a new chunk and add it as a child
                Chunk chunk = new Chunk(attachedApplication);
                AddChild(chunk);
                chunk.Generate(position.x, position.y, position.z, seed, this);
                chunks[position.x][position.y].Add(position.z, chunk);
            }
            else if (!chunks[position.x].ContainsKey(position.y))
            {
                chunks[position.x].Add(position.y, new Dictionary<int, Chunk>());
                // Create a new chunk and add it as a child
                Chunk chunk = new Chunk(attachedApplication);
                AddChild(chunk);
                chunk.Generate(position.x, position.y, position.z, seed, this);
                chunks[position.x][position.y].Add(position.z, chunk);
            }
            else if (!chunks[position.x][position.y].ContainsKey(position.z))
            {
                // Create a new chunk and add it as a child
                Chunk chunk = new Chunk(attachedApplication);
                AddChild(chunk);
                chunk.Generate(position.x, position.y, position.z, seed, this);
                chunks[position.x][position.y].Add(position.z, chunk);
            }
        }
        // Gets the chunk at the given position, and if it doesn't exist, it generates one
        public Chunk GetChunkOrGenerate(Position position)
        {
            // Generate the chunk if necessary
            GenerateChunkIfNecessary(position);
            return chunks[position.x][position.y][position.z];
        }
        // Gets the chunk at the given position, and if it doesn't exist, it returns null
        public Chunk GetChunk(Position position)
        {
            if (chunks.ContainsKey(position.x))
            {
                if (chunks[position.x].ContainsKey(position.y))
                {
                    if (chunks[position.x][position.y].ContainsKey(position.z))
                    {
                        return chunks[position.x][position.y][position.z];
                    }
                }
            }
            return null;
        }
        // Adds the given object to the chunk at the given position
        public void AddToChunk(GameObject gameObject, Position position)
        {
            GetChunkOrGenerate(position).AddChild(gameObject);
        }
        // Removes the given object from the chunk at the given position
        public void RemoveFromChunk(int IDOfGameObject, Position position)
        {
            GetChunk(position).RemoveChild(GetGameObject(IDOfGameObject));
        }
        #endregion

        #region Object Manipulation API
        // Adds a new player to the world
        public void AddPlayer(string nameOfPlayer, GameObject newPlayer, Position newPosition)
        {
            // Change their name
            newPlayer.identifier.name = nameOfPlayer;
            // Set the position
            newPlayer.ChangePosition(newPosition);
            // Add the player to the world
            AddChild(newPlayer);
            // Create a new player instance for the new player
            Core.Player player = new Core.Player(nameOfPlayer);
            player.controlledGameObject = newPlayer;
            // Add them to the list of players
            players.Add(nameOfPlayer, player);
            // Add them to the corresponding chunk
            GetChunkOrGenerate(newPosition).AddChild(newPlayer);
        }
        // Moves the given player to the new position
        public void MovePlayer(string nameOfPlayer, Position newPosition)
        {
            // Move the object
            MoveObjectAndRenderAroundNewPosition(players[nameOfPlayer].controlledGameObject.ID, newPosition);
        }
        // Moves the given object to the new position
        public void MoveObjectAndRenderAroundNewPosition(int IDOfObject, Position newPosition)
        {
            // Render around the new position
            // TODO: Render on the y axis as well, underground stuff
            for(int x = newPosition.x - renderDistance; x < newPosition.x + renderDistance; x++)
            {
                for (int z = newPosition.z - renderDistance; z < newPosition.z + renderDistance; z++)
                {
                    // If a chunk doesn't exist at the new position, generate one.  
                    GenerateChunkIfNecessary(new Position(x, 0 , z));
                }
            }
            // Add the gameObject to the new chunk
            AddToChunk(GetGameObject(IDOfObject), newPosition);
            // Remove it from the old one
            RemoveFromChunk(IDOfObject, GetGameObject(IDOfObject).position);
            // Set it's position to the new position
            GetGameObject(IDOfObject).ChangePosition(newPosition);
        }
        public void MoveObject(int IDOfObject, Position newPosition)
        {
            // If a chunk exists at the new position
            if(GetChunk(newPosition) != null)
            {
                // Add the gameObject to the new chunk
                AddToChunk(GetGameObject(IDOfObject), newPosition);
                // Remove it from the old one
                RemoveFromChunk(IDOfObject, GetGameObject(IDOfObject).position);
                // Set it's position to the new position
                GetGameObject(IDOfObject).ChangePosition(newPosition);
            }
        }           
        // Gets the object with the specified ID and specified position
        public GameObject GetGameObject(int ID)
        {
            if(gameObjectDictionary.ContainsKey(ID))
                return gameObjectDictionary[ID];
            Console.WriteLine("Failed to find gameObject with ID " + ID.ToString());
            return null;
        }
        // Finds and returns a list of gameObjects that have the name specified
        public List<GameObject> FindGameObjects(string name, Position position)
        {
            return GetChunkOrGenerate(position).FindChildrenWithName(name);
        }
        // Finds and returns the first gameObject found with the name specified
        public GameObject FindFirstGameObject(string name, Position position)
        {
            // If the list of objects is not empty, return the first one
            if (FindGameObjects(name, position).Count != 0)
                return FindGameObjects(name, position).First();
            return null;
        }
        #endregion
    }
}
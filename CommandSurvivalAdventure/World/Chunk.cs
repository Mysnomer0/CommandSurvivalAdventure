using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSurvivalAdventure.World
{
    // This class is the building block of the world, the world is essentially a 3D grid of these
    class Chunk : GameObject
    {
        // The biome of this chunk
        public Biome biome;
        // The wind speed
        public float windSpeed;
        // The current temperature on the chunk
        public float temperature;
        // The seed of the chunk
        public int seed;
        
        // Always use this constructor if this chunk is to be part of the world
        public Chunk(Application newApplication)
        {
            attachedApplication = newApplication;
        }

        public Chunk()
        {

        }

        // Starts the chunk
        public override void Start()
        {
            // Start all the game objects on the chunk
            foreach (GameObject gameObject in children)
                gameObject.Start();
        }
        // Updates the chunk
        public override void Update()
        {
            // Update all the game objects on the chunk

            // Get the children as a copied array because children may leave the chunk and thus modify the children HashSet, preventing us from doing a normal foreach
            GameObject[] arrayOfChildren = new GameObject[children.Count];
            children.CopyTo(arrayOfChildren);
            for(int i = 0; i < children.Count; i++)
            {
                arrayOfChildren[i].Update();
            }
        }
        // Generates the chunk
        public void Generate(Position newPosition, int newSeed, World world)
        {
            base.Generate(seed);
            identifier.name = "Chunk " + newPosition.x.ToString() + " " + newPosition.y.ToString() + " " + newPosition.z.ToString();
            ChangePosition(newPosition);
            seed = newSeed;
            // Generate the perlin noise
            Support.Perlin perlin = new Support.Perlin();
            // Based on the seed figure out the height of the chunk
            double noiseValue = perlin.GetValue(newPosition.x, 0, newPosition.z, 4, 15, 0.005f, seed) * 150.0f;
            // Ocean
            if (noiseValue < world.waterLevel)
                biome = new Biomes.BiomeOcean();
            // Beach
            else if (noiseValue < world.waterLevel + 5)
                biome = new Biomes.BiomeBeach();
            // Grassland
            else if (noiseValue < world.waterLevel + 20)
                biome = new Biomes.BiomeGrassland();
            // Savanna
            else if (noiseValue < world.waterLevel + 30)
                biome = new Biomes.BiomeSavanna();
            // BiomeChaparral
            else if (noiseValue < world.waterLevel + 35)
                biome = new Biomes.BiomeChaparral();
            // Desert
            else if (noiseValue < world.waterLevel + 40)
                biome = new Biomes.BiomeDesert();
            // Canyons
            else if (noiseValue < world.waterLevel + 45)
                biome = new Biomes.BiomeCanyon();
            // LimestoneHills
            else if (noiseValue < world.waterLevel + 50)
                biome = new Biomes.BiomeLimestoneHills();
            // Jungle
            else if (noiseValue < world.waterLevel + 60)
                biome = new Biomes.BiomeJungle();
            // Swamp
            else if (noiseValue < world.waterLevel + 65)
                biome = new Biomes.BiomeSwamp();
            // Bog
            else if (noiseValue < world.waterLevel + 70)
                biome = new Biomes.BiomeBog();
            // Forest
            else if (noiseValue < world.waterLevel + 80)
                biome = new Biomes.BiomeForest();
            // Mountains
            else if (noiseValue < world.waterLevel + 85)
                biome = new Biomes.BiomeMountains();
            // Alpine tundra
            else if (noiseValue < world.waterLevel + 90)
                biome = new Biomes.BiomeAlpineTundra();
            // Glacier
            else
                biome = new Biomes.BiomeGlacier();
            //biome = new Biomes.BiomeCanyon();
            // Generate the new biome
            biome.Generate(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;



class Program
{
    static void Main(string[] args)
    {
        CommandSurvivalAdventure.Application application = new CommandSurvivalAdventure.Application();
        /*
        CommandSurvivalAdventure.Support.Perlin perlin = new CommandSurvivalAdventure.Support.Perlin();
        Random rand = new Random();
        int seed = rand.Next();
        int waterLevel = rand.Next(20);
        Console.SetWindowSize(100, 30);
        for (int y = 0; y < 50; y++)
        {
            for (int x = 0; x < 100; x++)
            {
                // Based on the seed figure out the height of the chunk
                double noiseValue = perlin.GetValue(x, 0, y, 4, 4, 0.005f, seed) * 130;
                // Ocean
                if (noiseValue < waterLevel)
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.DARK_BLUE);
                // Beach
                else if (noiseValue < waterLevel + 5)
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.YELLOW);
                // Grassland
                else if (noiseValue < waterLevel + 20)
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.GREEN);
                // Savanna
                else if (noiseValue < waterLevel + 30)
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.DARK_YELLOW);
                // Desert
                else if (noiseValue < waterLevel + 40)
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.YELLOW);
                // Canyons
                else if (noiseValue < waterLevel + 45)
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.DARK_RED);
                // Jungle
                else if (noiseValue < waterLevel + 50)
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.DARK_GREEN);
                // Swamp
                else if (noiseValue < waterLevel + 55)
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.DARK_YELLOW);
                // Bog
                else if (noiseValue < waterLevel + 60)
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.DARK_MAGENTA);
                // Forest
                else if (noiseValue < waterLevel + 70)
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.DARK_GREEN);
                // Mountains
                else if (noiseValue < waterLevel + 80)
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.WHITE);
                // Alpine tundra
                else if (noiseValue < waterLevel + 90)
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.DARK_CYAN);
                // Glacier
                else
                    CommandSurvivalAdventure.IO.Output.PrintPixel(x, y, CommandSurvivalAdventure.IO.Output.OutputColor.CYAN);
            }
        }
        */
    }
}


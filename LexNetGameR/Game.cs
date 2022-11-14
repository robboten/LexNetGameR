using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace LexNetGameR
{
    internal class Game
    {
        public void Run()
        {
            Random random = new();
            //put this into Gameworld and make a list of levels
            Map map = new("Första", 10, 10);
            //(int X, int Y) GetRandomLocation() => (rnd.Next(), rnd.Next());
            Vector2 pos = new(random.Next(0,10), random.Next(0, 10));
            Console.WriteLine($"{pos.X},{pos.Y}");

            for (int y=0; y < map.Height;y++)
            {
                for (int x=0; x < map.Width;x++)
                {
                    Console.Write(map.cells[y, x].GetCell());
                }
                Console.WriteLine();
            }

            var heroCell = map.GetCell(0, 0);
            while (true)
            {
                Console.WriteLine(ConsoleInput.GetKey());
            }
        }
    }
}

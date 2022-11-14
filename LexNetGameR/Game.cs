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
            Vector2Int size=new(10,10);
            Vector2Int HeroStartCell=new(0,0);
            
            //put this into Gameworld and make a list of levels
            Map map = new("Första", size);
            var heroCell = map.GetCell(HeroStartCell);
            Entity hero=new(HeroStartCell,"H ","Hero", ConsoleColor.Red);
            
            //(int X, int Y) GetRandomLocation() => (rnd.Next(), rnd.Next());
            //Vector2 pos = new(random.Next(0,10), random.Next(0, 10));
            //Console.WriteLine($"{pos.X},{pos.Y}");
            map.cells[HeroStartCell.X,HeroStartCell.Y].SetCellSymbol(hero);

            for (int y=0; y < map.Size.Y;y++)
            {
                for (int x=0; x < map.Size.X;x++)
                {
                    var mapCell=map.cells[y, x];
                    //Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(mapCell.GetCellSymbol() );
                }
                Console.WriteLine();
            }

            
            while (true)
            {
                Console.WriteLine(ConsoleInput.GetKey());
            }
        }
    }
}

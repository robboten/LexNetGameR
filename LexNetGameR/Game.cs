using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace LexNetGameR
{
    internal class Game
    {

    string WallsString =
    "╔══════════════════╗" +
    "║                  ║" +
    "║   ╔═╗   ╔═════╗  ║" +
    "║   ╚═╝   ╚═════╝  ║" +
    "║                  ║" +
    "║                  ║" +
    "║                  ║" +
    "║                  ║" +
    "║                  ║" +
    "║                  ║" +
    "╚══════════════════╝";
        public int Score;
        //put this into Gameworld and make a list of levels

        Vector2Int MapSize;
        Vector2Int HeroStartCell;
        readonly string[] Ghosts;
        readonly string[] Coins;

        //Map map;
        readonly EntityManager em;
  
        public Game()
        {
            Score = 0;
            //map = new("Första", MapSize);
            MapSize = new(20, 10);
            HeroStartCell = new(0, 0);
            em = new();
            Ghosts = new string[] {"g1","g2","g3","g4","g5"};
            Coins = new string[] { "c1", "c2", "c3", "c4", "c5" };

            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void Run()
        {
            //create entities
            em.CreateEntity("Hero",HeroStartCell,'H',ConsoleColor.Blue,true);

            foreach (string ghost in Ghosts)
                em.CreateEntity(ghost, 'G');

            foreach (string coins in Coins)
                em.CreateEntity(coins,'O',false,true);

            while (true)//(!(Console.KeyAvailable))
            {
                DrawMap(em);
                foreach (var entityInList in em.GetEntityList())
                {
                    Vector2Int Acceleration;
                    var currentEnt = em.GetEntity(entityInList.Value.Name);

                    if (entityInList.Value.IsPlayer)
                        Acceleration = Controller.GetInput();
                    else if (entityInList.Value.IsStatic)
                        Acceleration=Vector2Int.Zero;
                    else
                        Acceleration = Controller.AIInput();

                    currentEnt.SetPos(Acceleration);
                }
                
            }
        }

        //I don't like sending the em in here but not sure how to solve it atm
        //break this up.. DrawMap does the game things now. Not how I want it
        public void DrawMap(EntityManager em)
        {
            Console.Clear();
            char BoardAt(int x, int y) => WallsString[y * 6 + x];

            for (int y = 0; y < MapSize.Y; y++)
            {
                for (int x = 0; x < MapSize.X; x++)
                {
                    //var mapCell = cells[x, y];
                    var mapPos = new Vector2Int(x, y);

                    var entitiesList = em.GetEntityList();

                    var entitiesAtPos = entitiesList.Where(xy => xy.Value.Position == mapPos);

                    var EntityAtPos = entitiesAtPos.FirstOrDefault().Value; //should entity be exposed like this?

                    var playerEntitiy = entitiesList.Where(e => e.Value.IsPlayer == true).FirstOrDefault().Value;

                    var NrEntities = entitiesList.Where(xy => xy.Value.Position == mapPos).Count();

                    char symbol;

                    


                    if (EntityAtPos != null)
                    {
                        //collision occured with player
                        if (NrEntities > 1 && playerEntitiy.Position == mapPos)
                        {
                            symbol = 'X';
                            Console.ForegroundColor = ConsoleColor.Red;
                            var collisionEntity = entitiesAtPos.Where(e => e.Value.IsPlayer == false).FirstOrDefault().Value;
                            em.RemoveEntity(collisionEntity);
                            Score ++;
                        }
                        else
                        {
                            symbol = EntityAtPos.Symbol;
                            Console.ForegroundColor = EntityAtPos.Color;
                        }

                    }
                    else
                    {

                        //bool IsWall(int x, int y) => BoardAt(x, y) is not ' ';
                        //symbol = BoardAt(x,y);//mapCell.GetCellSymbol();
                        symbol = '.';
                        Console.ForegroundColor = ConsoleColor.White;//mapCell.Color;
                    }
                    Console.Write(symbol.ToString() + ' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Score: {Score}");
        }
    }

}

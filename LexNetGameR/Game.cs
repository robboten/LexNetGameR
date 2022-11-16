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
        char[,] maze =
        {
            { '╔','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','╗'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ','╔','═','═','═','╗',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ','║',' ',' ',' ','║',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ','║',' ',' ',' ','║',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ','╚','═','═','═','╝',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '╚','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','╝'}

        };
    string MazeString =
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



        //Break out some of this into Gameworld and make a list of levels?

        public int Score;

        Vector2Int MapSize;
        Vector2Int HeroStartCell;
        readonly string[] Ghosts;
        readonly string[] Coins;

        readonly EntityManager em;
  
        public Game()
        {
            Score = 0;
            MapSize = new(20, 10); //read from map instead
            HeroStartCell = new(0, 0);
            em = new();
            Ghosts = new string[] {"g1","g2","g3","g4","g5"};
            Coins = new string[] { "c1", "c2", "c3", "c4", "c5" };

            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;

            //create entities
            em.CreateEntity("Hero", 'H', HeroStartCell, ConsoleColor.Blue, true);

            foreach (string ghost in Ghosts)
                em.CreateEntity(ghost, '†');

            foreach (string coins in Coins)
                em.CreateEntity(coins, '$', ConsoleColor.Yellow, false, true);
        }
        public void Run()
        {

            //RenderEntity(em.GetEntity("Hero"));
            //RenderEntity(em.GetEntity("g1"));
            DrawMap();
            while (true)//(!(Console.KeyAvailable))
            {
                //Console.Clear();
                
                Move();
                
            }
        }

        
        private void Move()
        {
            Vector2Int Acceleration;

            foreach (var entityInList in em.GetEntityList())
            {
                var currentEnt = entityInList.Value;

                if (currentEnt.IsPlayer)
                    Acceleration = Controller.GetInput();
                else if (currentEnt.IsStatic)
                    Acceleration = Vector2Int.Zero;
                else
                    Acceleration = Controller.AIInput();


                    RenderEntityTrace(currentEnt);
                    currentEnt.SetPos(Acceleration); // --- fix this ! shouldn't move under some conditions ...
                    RenderEntity(currentEnt);

                    
            }
        }

        //draw map as static first
        //in move draw each char relative

        //break this up.. DrawMap does the game things now. Not how I want it
        //use relative position instead
        public void DrawMap()
        {
            char symbol;
            Vector2Int Size = MapSize; //clean
            Size = new Vector2Int(maze.GetLength(0), maze.GetLength(1));

            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                        symbol = maze[y,x];//mapCell.GetCellSymbol();
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.Write(symbol.ToString() );
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Score: {Score} "); //put into own method
        }

        bool IsWall(int x, int y) => maze[y, x] is not ' ';
        public bool CanMove(Vector2Int pos)
        {
            if (!IsWall(pos.X, pos.Y))  
                return true;
            return false;
            //bool 
        }

        public void CheckPos(Vector2Int mapPos)
        {
            var entitiesList = em.GetEntityList();

            var entitiesAtPos = entitiesList.Where(xy => xy.Value.Position == mapPos);

            var EntityAtPos = entitiesAtPos.FirstOrDefault().Value; //should entity be exposed like this?

            var playerEntitiy = entitiesList.Where(e => e.Value.IsPlayer == true).FirstOrDefault().Value;

            var NrEntities = entitiesList.Where(xy => xy.Value.Position == mapPos).Count();
            //collision occured with player
            //if (NrEntities > 1 && playerEntitiy.Position == mapPos)
            //{
            //    symbol = 'X';
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    var collisionEntity = entitiesAtPos.Where(e => e.Value.IsPlayer == false).FirstOrDefault().Value;
            //    em.RemoveEntity(collisionEntity);
            //    Score++;
            //}
            //else
            //{
            //}
        }

        //put into entities class?
        public void RenderEntity(Entity entity)
        {
            char symbol=entity.Symbol;

            Vector2Int entPos=entity.GetPos();

            if (CanMove(entPos))
            {
                Console.ForegroundColor = entity.Color;
                Console.SetCursorPosition(entPos.X,entPos.Y);
                Console.Write(symbol.ToString());
                //}
            }
        }
        public void RenderEntityTrace(Entity entity)
        {
            char symbol = entity.Symbol;

            Vector2Int entPos = entity.GetPos();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(entPos.X, entPos.Y);
            Console.Write(maze[entPos.Y, entPos.X].ToString());
        }
    }
}

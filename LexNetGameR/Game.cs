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
            { '╔','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','╗'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ','╔','═','═','═','╗',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ','╚','═','═','═','╝',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','╔','═','═','═','╗',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','╚','═','═','═','╝',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ','╔','═','═','═','╗',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ','╚','═','═','═','╝',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '╚','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','╝'}

        };

        //Break out some of this into Gameworld and make a list of levels?

        public int Score;

        Vector2Int MapSize;
        Vector2Int HeroStartCell;
        readonly string[] Ghosts;
        readonly string[] Coins;

        readonly EntityManager em;

        //
        public Game()
        {
            Score = 0;

            HeroStartCell = new(1, 1);
            em = new();

            MapSize = new Vector2Int(maze.GetLength(1), maze.GetLength(0));

            Ghosts = new string[] {"g1","g2","g3","g4","g5"};
            Coins = new string[] { "c1", "c2", "c3", "c4", "c5" };

            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;

            //create entities
            em.CreateEntity("Hero", 'H', HeroStartCell, ConsoleColor.Blue,true);

            foreach (string ghost in Ghosts)
            {
                em.CreateEntity(ghost, '†', RanPos(), ConsoleColor.DarkGray,false,false);
            }
                
            foreach (string coins in Coins)
            {                
                em.CreateEntity(coins, '$', RanPos(), ConsoleColor.Yellow, false, true);
            }
                
        }

        public void ShowPoints()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, MapSize.Y+2);
            Console.WriteLine("                     ");
            Console.SetCursorPosition(0, MapSize.Y + 2);
            Console.WriteLine(($"Score: {Score} "));
        }

        public Vector2Int RanPos()
        {
            Vector2Int randPos;
            while (true) //ugly check.. work on this
            {
                randPos = Vector2Int.GetRandom(MapSize.X, MapSize.Y); //make random method - check for walls...
                if (CanMove(randPos))
                    return randPos;
            }
        }
        public void Run()
        {

            DrawMap();
            ShowPoints();
            Init();
            while (true)//(!(Console.KeyAvailable))
            {
                //Console.Clear();
                Move();
            }
        }
        private void Init()
        {

            foreach (var entityInList in em.GetEntityList())
            {
                var currentEnt = entityInList.Value;
                RenderEntity(currentEnt, Vector2Int.Zero);
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

                RenderEntity(currentEnt, Acceleration);
                CheckPos(currentEnt.Position); //where to call this?
            }
        }

        public void DrawMap()
        {
            char symbol;
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < MapSize.Y; y++)
            {
                for (int x = 0; x < MapSize.X; x++)
                {
                        symbol = maze[y,x];//mapCell.GetCellSymbol();
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.Write(symbol.ToString() );
                }
                Console.WriteLine();
            }
        }

        bool IsWall(int x, int y) => maze[y, x] is not ' ';

        public bool CanMove(Vector2Int pos)
        {
            if (pos.X>0 && pos.X<maze.GetLength(1) && pos.Y > 0 && pos.Y < maze.GetLength(0))
                if (!IsWall(pos.X, pos.Y))
                    return true;
            return false;
        }

        public void CheckPos(Vector2Int pos) //change to entity as argument?
        {
            var entitiesList = em.GetEntityList();

            //get all entities at position
            var entitiesAtPos = entitiesList.Where(xy => xy.Value.Position == pos); 
            //get count of entities at this pos
            var NrEntities = entitiesAtPos.Count();

            //check if there are more than one entity at pos
            if (NrEntities > 1)
            {
                //is any of them player?
                //get player
                var playerEntitiy = entitiesAtPos.Where(e => e.Value.IsPlayer == true).FirstOrDefault().Value;
                
                //collision occured with player
                if (playerEntitiy != null)
                {
                    var collisionEntities = entitiesAtPos.Where(e => e.Value.IsPlayer == false);
                    foreach(var entity in collisionEntities)
                    {
                        OutputSymbol(ConsoleColor.DarkRed, 'X', playerEntitiy.Position);
                        //remove the entity from the ent manager list
                        em.RemoveEntity(entity.Value);
                        Score++;
                        ShowPoints();
                    }
                }
                //get other entity
            }
        }

        //put into entities class?
        public void RenderEntity(Entity entity, Vector2Int acceleration)
        {
            
            char symbol = entity.Symbol;

            Vector2Int entPos=entity.GetPos();
            Vector2Int newPos=entPos += acceleration;

            if (CanMove(newPos))
            {
                RenderEntityTrace(entity); //remove char from old pos
                entity.SetPos(newPos); //set new pos

                OutputSymbol(entity.Color, entity.Symbol,entity.Position);
            }
        }

        private static void OutputSymbol(ConsoleColor color, char symbol, Vector2Int pos)
        {
            //output the symbol at the new pos
            Console.ForegroundColor = color;
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(symbol.ToString());
        }

        public void RenderEntityTrace(Entity entity)
        {
            Vector2Int entPos = entity.GetPos();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(entPos.X, entPos.Y);
            Console.Write(maze[entPos.Y, entPos.X].ToString());

        }
    }
}

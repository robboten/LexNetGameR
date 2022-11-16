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
        readonly char[,] maze =
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
        
        //drop mines from inv?

        //isolate entities more into em?

        public int Score;
        Vector2Int MapSize;

        readonly EntityManager em;

        //Break out some and make a list of levels?
        //what to move into init instead?
        public Game()
        {
            em = new();

            Score = 0;

            MapSize = new Vector2Int(maze.GetLength(1), maze.GetLength(0)); //needed?

            //make entities subclasses for oop or keep more data oriented design ?
            Vector2Int HeroStartCell = new(1, 1);

            string[] Ghosts = new [] { "g1","g2","g3","g4","g5" };
            string[] Coins = new [] { "c1", "c2", "c3", "c4", "c5" };

            //create entities
            em.CreateEntity("Hero", 'H', HeroStartCell, UI.Blue,true);

            foreach (string ghost in Ghosts)
            {
                em.CreateEntity(ghost, '†', RanPosWithCheck(), UI.DarkGray,false,false);
            }
                
            foreach (string coins in Coins)
            {                
                em.CreateEntity(coins, '$', RanPosWithCheck(), UI.Yellow, false, true);
            }     
        }

        public void Run()
        {
            Init();
            while (true)//(!(Console.KeyAvailable))
            {
                Move();
            }
        }

        private void Init()
        {
            UI.InitUI();
            DrawMap();
            foreach (var entityInList in em.GetEntityList())
            {
                var currentEnt = entityInList.Value;
                RenderEntity(currentEnt, Vector2Int.Zero);
            }
            ShowPoints();
        }

        public void ShowPoints()
        {
            Vector2Int position = new (0, MapSize.Y + 2);
            string str = $"Score: {Score} ";
            UI.OutputSymbol(UI.White, str, position); 
        }

        public Vector2Int RanPosWithCheck()
        {
            Vector2Int randPos;
            while (true) //ugly check.. work on this
            {
                randPos = Vector2Int.GetRandom(MapSize.X, MapSize.Y); //make random method - check for walls...
                if (CanMove(randPos))
                    return randPos;
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
                CheckPos(currentEnt.Position); //where to call this? use entity instead of pos?
            }
        }

        public void DrawMap()
        {
            char symbol;
            //Console.SetCursorPosition(0, 0);

            for (int y = 0; y < MapSize.Y; y++)
            {
                for (int x = 0; x < MapSize.X; x++)
                {
                    symbol = maze[y,x];
                    UI.OutputSymbol(UI.White, symbol.ToString(), new Vector2Int(x,y));
                }
                UI.NewLine(); //how to get rid of console here?
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
                //get player
                var playerEntitiy = entitiesAtPos.FirstOrDefault(e => e.Value.IsPlayer == true).Value;

                //is any of them player?
                if (playerEntitiy != null)
                {
                    //check what entity is involved in collision
                    var collisionEntities = entitiesAtPos.Where(e => e.Value.IsPlayer == false);
                    foreach(var entity in collisionEntities)
                    {
                        //make an X to mark action
                        UI.OutputSymbol(UI.DarkRed, "X", playerEntitiy.Position);
                        //remove the entity from the ent manager list
                        em.RemoveEntity(entity.Value);
                        Score++; //diversify score?
                        ShowPoints();
                    }
                }
                //get other entity collisions for more complexity
            }
        }

        //put into entities class?
        public void RenderEntity(Entity entity, Vector2Int acceleration)
        {
            Vector2Int entPos=entity.GetPos();
            Vector2Int newPos=entPos += acceleration;

            if (CanMove(newPos))
            {
                RenderEntityTrace(entity); //remove char from old pos instead of redrawing everything.. good or not?
                entity.SetPos(newPos); //set new pos

                UI.OutputSymbol(entity.Color, entity.Symbol.ToString(),entity.Position);
            }
        }

        //for now just redraw map at old position
        public void RenderEntityTrace(Entity entity)
        {
            Vector2Int entPos = entity.GetPos();
            UI.OutputSymbol(UI.White, maze[entPos.Y, entPos.X].ToString(), entPos);
        }

        //move to ui

    }
}

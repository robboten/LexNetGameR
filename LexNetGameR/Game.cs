using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LexNetGameR.Entities;

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
        //add timer

        int Score;
        Vector2Int MapSize;

        //em keep track of all entities
        readonly EntityManager em;

        //Break out some and make a list of levels?
        //what to move into init instead?
        public Game()
        {
            Score = 0;

            em = new();

            MapSize = new Vector2Int(maze.GetLength(1), maze.GetLength(0)); //needed?
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
            MakeEntities();
            UI.InitUI();
            DrawMap();

            //render all entities to show them before move
            em.GetEntityList().ForEach(c => c.RenderEntity()); 

            ShowPoints();
        }

        private void MakeEntities()
        {
            //create entities
            em.CreateEntity('H', new Vector2Int(1, 1), UI.Blue, true);

            //make dict or smt with values for each level
            int GhostsNr = 5;
            int CoinsNr = 7;

            //var res= Enumerable.Range(0, GhostsNr).Select(i=> em.CreateEnemy2(RandomPosWithCheck()).ToList()); //eeh doesn't work

            for (int i = 0; i < GhostsNr; i++)
            {
                em.CreateEnemy(RandomPosWithCheck());
            }

            for (int i = 0; i < CoinsNr; i++)
            {
                em.CreateStatic(RandomPosWithCheck());
            }
        }

        void MakeEntGen()
        {

        }

        public void ShowPoints()
        {
            Vector2Int position = new (0, MapSize.Y + 2);
            string str = $"Score: {Score} ";
            UI.OutputSymbol(UI.White, str, position); 
        }

        public Vector2Int RandomPosWithCheck()
        {
            Vector2Int randPos;
            while (true) //ugly check.. work on this
            {
                randPos = Vector2Int.GetRandom(MapSize.X, MapSize.Y);
                if (CanMove(randPos))
                    return randPos;
            }
        }

        private void Move()
        {
            //remake this with linq/delegate?

            //var player = em.GetEntityList().FirstOrDefault(e => e.IsPlayer == true);
            //have to check if not null... so is it really better?
            //player.Acceleration = Controller.GetInput();

            foreach (var entity in em.GetEntityList())
            {
                if (entity.IsPlayer)
                    entity.Acceleration = Controller.GetInput();
                else if (!entity.IsStatic) //for all other that is not statics
                    entity.Acceleration = Controller.AIInput();

                //easy way to skip this for all that is not changed?
                if (CanMove(entity.Position, entity.Acceleration))
                {
                    entity.RenderEntityTrace(maze[entity.Position.Y, entity.Position.X]);
                    entity.RenderEntity();
                }
                    
                CheckPos(entity.Position); //where to call this? use entity instead of pos?
            }
        }

        public void DrawMap()
        {
            for (int y = 0; y < MapSize.Y; y++)
            {
                for (int x = 0; x < MapSize.X; x++)
                {
                    UI.OutputSymbol(UI.White, maze[y, x].ToString(), new Vector2Int(x,y));
                }
                UI.NewLine();
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
        //how to get rid of this one?
        public bool CanMove(Vector2Int pos, Vector2Int acc)
        {
            pos += acc;
            if (pos.X > 0 && pos.X < maze.GetLength(1) && pos.Y > 0 && pos.Y < maze.GetLength(0))
                if (!IsWall(pos.X, pos.Y))
                    return true;
            return false;
        }

        public void CheckPos(Vector2Int pos) //change to entity as argument?
        {
            var entitiesList = em.GetEntityList();

            //get all entities at position that is active
            var entitiesAtPos = entitiesList.Where(e => e.Position == pos && e.IsActive).ToList();
            //get count of entities at this pos
            var NrEntities = entitiesAtPos.Count;

            //check if there are more than one entity at pos
            if (NrEntities > 1)
            {
                //get player
                var playerEntitiy = entitiesAtPos.FirstOrDefault(e => e.IsPlayer == true);

                //is any of them player?
                if (playerEntitiy != null)
                {
                    //check what entity is involved in collision
                    var collisionEntities = entitiesAtPos.Where(e => e.IsPlayer == false);

                    foreach (var entity in collisionEntities)
                    {
                        //---------- does not work correctly atm (since Lists instead of Dict)
                        //because of deferred or smt else?

                        //make an X to mark action
                        UI.OutputSymbol(UI.DarkRed, "X", playerEntitiy.Position);
                        //remove the entity from the ent manager list
                        em.RemoveEntity(entity);
                        if (entity is Enemy)
                        {
                            var temp = (Enemy)entity;
                            Score += temp.Points;
                        } else if(entity is Static)
                        {
                            var temp = (Static)entity;
                            Score += temp.Points;
                        }

                        ShowPoints();
                    }
                }
                //get other entity collisions for more complexity

                //see if em is empty except player for win
            }
        }
    }
}

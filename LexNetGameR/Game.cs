using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LexNetGameR.Entities;

//ideas - todo
//drop mines from inv?
//add timer

namespace LexNetGameR
{
    internal class Game : IO
    {
        int Score;
        readonly Map map;
        readonly ConsoleUI UI;

        //em keeps track of all entities
        readonly EntityManager em;


        public Game()
        {
            map = new();
            Score = 0;
            em = new();
            UI = new ConsoleUI();
            
        }

        /// <summary>
        /// main game loop
        /// </summary>
        public void Run()
        {
            Init();
            while (true)//(!(Console.KeyAvailable))
            {
                UpdateEntities();
            }
        }

        /// <summary>
        /// Initialize the game
        /// </summary>
        private void Init()
        {
            List<Entity>? entityDataList = ReadConfig();
            if(entityDataList != null)
            {
                UI.InitUI();
                map.DrawMap();
                em.EntitiesInit(entityDataList);

                //randomize positions
                em.GetEntityList().ToList().ForEach(c => c.Position= map.RandomPosWithCheck());
                //render all entities to show them before move
                em.GetEntityList().ToList().ForEach(c => c.RenderEntity());

                //UI.ShowPoints(map.GetSizeV2(), Score);
            }
            else throw new InvalidOperationException("No config loaded, can't play..");
        }

        //want to move this from here, but not sure with all the things it uses...

        

        //alt. make a loop to get all acceleration and put everything else in entity class
        //or divide into get input - check movement - move
        private void UpdateEntities()
        {
            ///<summary>
            /// update player acceleration and move
            ///</summary>
            var player = em.GetEntityList().FirstOrDefault(e => e.IsPlayer == true);
            if (player == null)
            {
                throw new InvalidOperationException("No player entities loaded, can't play..");
            } else
            {
                Vector2Int acc = Controller.GetInput();
                player.Acceleration = acc;
                CheckMove(player);
            }

            ///<summary>
            /// update npcs acceleration and move
            ///</summary>
            var npcs = em.GetEntityList().Where(e => e.IsPlayer == false && e.IsStatic == false);

            npcs.ToList().ForEach(
                i => {
                    i.Acceleration = Controller.AIInput();
                    CheckMove(i);
                });
        }

        /// <summary>
        /// Check if move is possible in the accelerated direction, if so move and render
        /// </summary>
        /// <param name="entity"></param>
        private void CheckMove(Entity entity)
        {
            if (map.CanMove(entity.Position, entity.Acceleration))
            {
                //check here for collision instead?

                entity.RenderEntityTrace(map.GetChar(entity.Position.X, entity.Position.Y));
                entity.RenderEntity();

                CheckCollision(entity.Position); //where to call this? use entity instead of pos?
            }
        }


        public void CheckCollision(Vector2Int pos) //change to entity as argument?
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
                        //make an X to mark action
                        ConsoleUI.OutputSymbol("DarkRed", "X", playerEntitiy.Position);

                        //remove the entity from the ent manager list
                        em.RemoveEntity(entity);

                        //different scores for different entities
                        //if (entity is Enemy tempE)
                        //{
                        //    Score += tempE.Points;
                        //} else if (entity is Static tempS)
                        //{
                        //    Score += tempS.Points;
                        //}
                        Score += entity.Points;
                        UI.ShowPoints(map.GetSizeV2(), Score);
                    }
                }
                //get other entity collisions for more complexity

                //see if em is empty except player for win
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LexNetGameR.Controller;
using LexNetGameR.Entities;

//ideas - todo
//drop mines from inv?
//add timer


//get freespace list and em list - 
//replace all fsl pos that has a em.pos hit

//get list with voids, make a new copy, add the list of the enities pos, output the whole thing

namespace LexNetGameR
{
    internal class Game : IO
    {
        readonly IUI UI;

        //em keeps track of all entities
        readonly EntityManager em= new();
        readonly PlayerController Controller = new();
        readonly AIController AIController = new();
        readonly Map map = new();

        bool IsGameRunning;
        int Score = 0;

        public Game(IUI ui)
        {
            UI = ui;
        }

        /// <summary>
        /// main game loop
        /// </summary>
        public void Run()
        {
            Init();
            IsGameRunning = true;

            while (IsGameRunning)//(!(Console.KeyAvailable))
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
                map.DrawMap(UI);
                
                em.EntitiesInit(entityDataList);
                
                //randomize positions
                em.GetEntityList().ToList().ForEach(c => c.Position= map.RandomPosWithCheck());
                //render all entities to show them before move
                em.GetEntityList().ToList().ForEach(c => c.TransformPosition());

                map.RenderAll(UI, em.GetEntityList(), map.GetMapPosList());

                UI.ShowPoints(map.GetSizeV2(), Score);
            }
            else throw new InvalidOperationException("No config loaded, can't play..");
        }
        
        //alt. make a loop to get all acceleration and put everything else in entity class
        //or divide into get input - check movement - move
        private void UpdateEntities()
        {
            ///<summary>
            /// update player acceleration and move
            ///</summary>
            
            var player = em.GetEntityList().First(e => e.IsPlayer == true);
            if (player == null)
            {
                throw new InvalidOperationException("No player entities loaded, can't play..");
            } 
            else
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
                    i.Acceleration = AIController.GetInput();
                    CheckMove(i);
                });

            CheckGameOver();
        }

        private void CheckGameOver()
        {
            var entitiesLeft = em.GetEntityList().Where(e => e.IsPlayer == false && e.IsActive).Count();
            if (entitiesLeft <= 0)
            {
                //-------change this to a better end screen--------
                Console.WriteLine("You Won!");
                Console.ReadKey();
                IsGameRunning=false;
            }
        }

        /// <summary>
        /// Check if move is possible in the accelerated direction, if so move and render
        /// </summary>
        /// <param name="entity"></param>
        private void CheckMove(Entity entity)
        {
            Debug.WriteLine(entity.Symbol);
            if (map.CanMove(entity.Position, entity.Acceleration))
            {
                entity.TransformPosition();
                CheckCollision(entity.Position);
                map.RenderAll(UI, em.GetEntityList(), map.GetMapPosList());
            }
        }

        /// <summary>
        /// check collision and update points
        /// </summary>
        /// <param name="pos"></param>
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

                    //make an X to mark action
                    //playerEntitiy.Color = "DarkRed";
                    //UI.OutputSymbol("DarkRed", playerEntitiy.Symbol.ToString(), playerEntitiy.Position);

                    foreach (var entity in collisionEntities)
                    {
                        //remove the entity from the ent manager list
                        em.RemoveEntity(entity);

                        //update score
                        Score += entity.Points;
                        UI.ShowPoints(map.GetSizeV2(), Score);
                    }
                }
                //get other entity collisions for more complexity
            }
        }
    }
}

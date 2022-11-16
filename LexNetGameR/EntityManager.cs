using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using LexNetGameR.Entities;

namespace LexNetGameR
{
    internal class EntityManager
    {
        readonly List<Entity> EntitiesList;

        public EntityManager(){
            EntitiesList = new List<Entity>();
        }
        public void AddEntity(Entity entity){
            EntitiesList.Add(entity);
        }
        public void RemoveEntity(Entity entity){
            EntitiesList.Remove(entity);
        }

        public void CreateEntity(char symbol, Vector2Int position, ConsoleColor color, bool isPlayer = false, bool isStatic = false)
        {
            Entity entity = new(symbol )
            {
                Color = color,
                IsPlayer = isPlayer,
                IsStatic = isStatic,
                Position=position,
            };
            AddEntity(entity);
        }
        public void CreateEntity(char symbol, Vector2Int position, ConsoleColor color, bool isPlayer = false)
        {
            Entity entity = new(symbol)
            {
                Position=position,
                Color = color,
                IsPlayer = isPlayer
            };
            AddEntity(entity);
        }

        public void CreateStatic(Vector2Int position)
        {
            Static entity = new(position)
            {
                Position = position,
            };
            AddEntity(entity);
        }
        public void CreateEnemy(Vector2Int position)
        {
            Enemy entity = new(position)
            {
                Position = position,
            };
            AddEntity(entity);
        }

        //use or not?
        //public Entity GetEntity(string name)
        //{
        //    return EntitiesDict[name];
        //}

        public List<Entity> GetEntityList()
        {
            return EntitiesList.ToList(); //how to safeguard multiple changes at once?
        }
    }
}

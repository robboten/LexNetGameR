using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace LexNetGameR
{
    internal class EntityManager
    {
        Dictionary<string, Entity> EntitiesDict;
        public EntityManager(){
            EntitiesDict =new Dictionary<string, Entity>();
        }
        public void AddEntity(Entity entity){
            EntitiesDict.Add(entity.Name,entity);
        }
        public void RemoveEntity(Entity entity){
            EntitiesDict.Remove(entity.Name);
        }
        public void CreateEntity(string name, char symbol, bool isPlayer = false, bool isStatic=false)
        {
            Entity entity = new(name, symbol)
            {
                IsPlayer = isPlayer,
                IsStatic = isStatic
            };
            AddEntity(entity);
        }
        public void CreateEntity(string name, char symbol, ConsoleColor color, bool isPlayer = false, bool isStatic = false)
        {
            Entity entity = new(name, symbol )
            {
                Color = color,
                IsPlayer = isPlayer,
                IsStatic = isStatic
            };
            AddEntity(entity);
        }
        public void CreateEntity(string name, char symbol, Vector2Int position, ConsoleColor color, bool isPlayer = false)
        {
            Entity entity = new(name, symbol)
            {
                Position=position,
                Color = color,
                IsPlayer = isPlayer
            };
            AddEntity(entity);
        }
        public Entity GetEntity(string name)
        {
            return EntitiesDict[name];
        }

        public Dictionary<string, Entity> GetEntityList()
        {
            return EntitiesDict;
        }

    }
}

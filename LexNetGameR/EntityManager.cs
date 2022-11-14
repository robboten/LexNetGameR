using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace LexNetGameR
{
    internal class EntityManager
    {
        List<Entity> Entities;
        public EntityManager(){
            Entities=new();
        }
        public void AddEntity(Entity entity){
            Entities.Add(entity);
        }
        public void RemoveEntity(Entity entity){
            Entities.Remove(entity);
        }
        public void CreateEntity(Vector2Int position, string symbol, string name){
            Entity entity=new(position, symbol, name);
            AddEntity(entity);
        }
    }
}

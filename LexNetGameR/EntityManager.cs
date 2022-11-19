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
            entity.IsActive = false;
        }

        //can I have only one but still make different ones?
        //public void CreateEntity(char symbol, Vector2Int position, ConsoleColor color, bool isPlayer = false,bool isStatic=false,int Points=0)
        //{
        //    Entity entity = new(symbol)
        //    {
        //        Position=position,
        //        Color = color,
        //        IsPlayer = isPlayer,
        //        IsStatic = isStatic,
        //        Points = Points
        //    };
        //    AddEntity(entity);
        //}

        public void CreateEntity(string name, char symbol, Vector2Int position, ConsoleColor color, bool isPlayer = false, bool isStatic = false, int Points = 0)
        {
            Entity entity = new(name, symbol)
            {
                Position = position,
                CColor = color,
                IsPlayer = isPlayer,
                IsStatic = isStatic,
                Points = Points
            };
            AddEntity(entity);
        }

        //public void CreateStatic(Vector2Int position)
        //{
        //    Static entity = new(position)
        //    {
        //        Position = position,
        //    };
        //    AddEntity(entity);
        //}
        //public void CreateEnemy(Vector2Int position)
        //{
        //    Enemy entity = new(position)
        //    {
        //        Position = position,
        //    };
        //    AddEntity(entity);
        //}

        //public List<Entity> CreateEnemy2(Vector2Int position)
        //{
        //    Enemy entity = new(position)
        //    {
        //        Position = position,
        //    };
        //    AddEntity(entity);
        //    return EntitiesList;
        //}


        public List<Entity> GetEntityList()//IEnumerable<Entity> GetEntityList()
        {
            //foreach (var item in EntitiesList)
            //{
            //    yield return item;
            //}
            return EntitiesList.ToList(); //how to safeguard multiple changes at once?
        }
    }
}

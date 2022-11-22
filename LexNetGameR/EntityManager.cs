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
    public class EntityManager
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

        public List<Entity> GetEntityList()//IEnumerable<Entity> GetEntityList()
        {
            //foreach (var item in EntitiesList)
            //{
            //    yield return item;
            //}
            return EntitiesList.ToList(); //how to safeguard multiple changes at once?
        }

        /// <summary>
        /// set up all the entities for the level
        /// </summary>
        public void EntitiesInit(List<Entity> entityDataList)
        {
            //List<Entity>? entityDataList = ReadConfig();
            for (int i = 0; i < entityDataList.Count; i++)
            {
                Entity e = entityDataList[i];
                AddEntity(e);
                if (e.Position.Equals(0))
                {
                    //how to randomize from here?
                    e.Position = new Vector2Int(2, 2);//RandomPosWithCheck();
                }
                
            }
        }
    }
}

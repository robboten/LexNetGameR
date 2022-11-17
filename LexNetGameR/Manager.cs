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
    internal class Manager<T>
    {
        readonly List<T> ItemList;

        public Manager(){
            ItemList = new List<T>();
        }
        public void AddItem(T item){
            ItemList.Add(item);
        }
        public void RemoveItem(T item){
            ItemList.Remove(item);
        }

        public void CreateItem(Vector2Int position)
        {
            Entity entity = new(position)
            {
                Position = position,
            };
            //AddEntity(entity); ????
        }

        public List<T> GetEntityList()
        {
            return ItemList.ToList(); //how to safeguard multiple changes at once?
        }
    }
}

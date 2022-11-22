using LexNetGameR.Entities;

namespace LexNetGameR
{
    public interface IEntityManager
    {
        //how to make use of IEntity instead?
        void AddEntity(Entity entity);
        void EntitiesInit(List<Entity> entityDataList);
        List<Entity> GetEntityList();
        void RemoveEntity(Entity entity);
    }
}
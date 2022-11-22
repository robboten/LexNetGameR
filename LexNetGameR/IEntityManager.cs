using LexNetGameR.Entities;

namespace LexNetGameR
{
    public interface IEntityManager
    {
        void AddEntity(Entity entity);
        void EntitiesInit(List<Entity> entityDataList);
        List<Entity> GetEntityList();
        void RemoveEntity(Entity entity);
    }
}
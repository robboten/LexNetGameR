namespace LexNetGameR.Entities
{
    public interface IEntity
    {
        Vector2Int Acceleration { get; set; }
        string Color { get; set; }
        bool IsActive { get; set; }
        bool IsPlayer { get; set; }
        bool IsStatic { get; set; }
        string Name { get; set; }
        int Points { get; set; }
        Vector2Int Position { get; set; }
        char Symbol { get; set; }

        void TransformPosition();
    }
}
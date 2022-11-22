using LexNetGameR.Entities;

namespace LexNetGameR
{
    public interface IMap
    {
        bool CanMove(Vector2Int pos, Vector2Int acc);
        void DrawMap(IUI UI);
        char GetMapChar(int x, int y);
        List<Vector2Int> GetMapPosList();
        Tuple<int, int> GetSizeInt();
        Vector2Int GetSizeV2();
        List<Vector2Int> MakeValidPosList();
        Vector2Int RandomPosWithCheck();
        void RenderAll(IUI UI, List<Entity> el, List<Vector2Int> maplist);
    }
}
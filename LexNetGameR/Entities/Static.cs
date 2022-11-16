using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR.Entities
{
    internal class Static : Entity
    {
        public Static(Vector2Int pos) : base('$', pos)
        {
            Position = pos;
            IsActive = true;
            Color = UI.Yellow;
            IsStatic = true;
            Acceleration=Vector2Int.Zero;
        }
    }
}

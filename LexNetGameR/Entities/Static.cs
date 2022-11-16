using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR.Entities
{
    internal class Static : Entity
    {
        public int Points { get; }
        public Static(Vector2Int pos) : base('$', pos)
        {
            Color = UI.Yellow;
            IsStatic = true;
            Acceleration=Vector2Int.Zero;
            Points = 5;

        }
    }
}

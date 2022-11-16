using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR.Entities
{
    internal class Enemy : Entity
    {
        public Enemy(Vector2Int pos) : base('†', pos)
        {
            Position = pos;
            IsActive = true;
            Color = UI.DarkGray;
            IsStatic = false;
        }
    }
}

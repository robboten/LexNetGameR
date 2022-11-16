using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR.Entities
{
    internal class Enemy : Entity
    {
        public int Points { get;}
        public Enemy(Vector2Int pos) : base('†', pos)
        {
            Points = 10;
            Color = UI.DarkGray;
            IsStatic = false;
        }
    }
}

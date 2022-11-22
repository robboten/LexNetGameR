using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR.Controller
{
    internal class AIController : IController
    {
        public Vector2Int GetInput()
        {
            Random random = new();
            Vector2Int acc = new(random.Next(-1, 2), random.Next(-1, 2)); //exclusive max
            return acc;
        }
    }
}

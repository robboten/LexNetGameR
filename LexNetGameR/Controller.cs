using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR
{
    public static class Controller
    {
        public static Vector2Int GetInput()
        {
            return ConsoleInput.GetCommand();
        }
        public static Vector2Int AIInput()
        {
            Random random = new();
            Vector2Int acc = new(random.Next(-1,2), random.Next(-1, 2)); //exclusive max
            return acc;
        }
    }
}

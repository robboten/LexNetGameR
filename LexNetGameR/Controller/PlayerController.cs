using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR.Controller
{
    public class PlayerController : IController
    {
        public Vector2Int GetInput()
        {
            return ConsoleInput.GetCommand();
        }
    }
}

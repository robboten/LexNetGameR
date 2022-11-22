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
            return GetCommand();
        }

        //mixes in console here which is not good
        private static ConsoleKey GetKey() => Console.ReadKey(intercept: true).Key;

        public static Vector2Int GetCommand()
        {
            var key = GetKey();
            return key switch
            {
                ConsoleKey.LeftArrow => Vector2Int.Left,
                ConsoleKey.RightArrow => Vector2Int.Right,
                ConsoleKey.UpArrow => Vector2Int.Up,
                ConsoleKey.DownArrow => Vector2Int.Down,
                _ => Vector2Int.Zero,
            };
        }
    }


}

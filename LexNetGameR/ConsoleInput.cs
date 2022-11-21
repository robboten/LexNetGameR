using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR
{
    public class ConsoleInput
    {
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

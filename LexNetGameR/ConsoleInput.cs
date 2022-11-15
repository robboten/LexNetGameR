using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR
{
    internal static class ConsoleInput
    {
        internal static ConsoleKey GetKey() => Console.ReadKey(intercept: true).Key;

        public static Vector2Int GetCommand()
        {
            var key = GetKey();
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    return Vector2Int.Left;
                case ConsoleKey.RightArrow:
                    return Vector2Int.Right;
                case ConsoleKey.UpArrow:
                    return Vector2Int.Up;
                case ConsoleKey.DownArrow:
                    return Vector2Int.Down;
                default:
                    return Vector2Int.Zero;

                //case ConsoleKey.P:
                //    PickUp();
                //    break;
            }
        }
    }
}

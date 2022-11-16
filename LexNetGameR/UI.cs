using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR
{
    internal class UI
    {
        public const ConsoleColor White = ConsoleColor.White;
        public const ConsoleColor Black = ConsoleColor.Black;
        public const ConsoleColor Red = ConsoleColor.Red;
        public const ConsoleColor Blue = ConsoleColor.Blue;
        public const ConsoleColor Green = ConsoleColor.Green;
        public const ConsoleColor DarkGray = ConsoleColor.DarkGray;
        public const ConsoleColor Yellow = ConsoleColor.Yellow;
        public const ConsoleColor DarkRed = ConsoleColor.DarkRed;

        public static void InitUI()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = Black;
        }

        public static void NewLine()
        {
            Console.WriteLine();
        }
        public static void OutputSymbol(ConsoleColor color, string symbol, Vector2Int pos)
        {
            //output the symbol at the new pos
            Console.ForegroundColor = color;
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(symbol);
        }
    }
}

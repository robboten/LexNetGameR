using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR
{
    public class UI
    {
        public const string MapColor = "White";

        public static void InitUI()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void NewLine()
        {
            Console.WriteLine();
        }
        //public static void OutputSymbol(ConsoleColor color, string symbol, Vector2Int pos)
        //{
        //    //output the symbol at the new pos
        //    Console.ForegroundColor = color;
        //    Console.SetCursorPosition(pos.X, pos.Y);
        //    Console.Write(symbol);
        //}

        public static void OutputSymbol(string color, string symbol, Vector2Int pos)
        {
            //output the symbol at the new pos
            Console.ForegroundColor = GetColor(color);
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(symbol);
        }

        public static ConsoleColor GetColor(string color)
        {
            return  (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
        }
    }
}

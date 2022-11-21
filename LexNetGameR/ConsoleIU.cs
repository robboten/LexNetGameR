using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace LexNetGameR
{
    public class ConsoleUI : IUI
    {
        public void InitUI()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //public static void NewLine()
        //{
        //    Console.WriteLine();
        //}
        //public static void OutputSymbol(ConsoleColor color, string symbol, Vector2Int pos)
        //{
        //    //output the symbol at the new pos
        //    Console.ForegroundColor = color;
        //    Console.SetCursorPosition(pos.X, pos.Y);
        //    Console.Write(symbol);
        //}

        /// <summary>
        /// show points 2 rows below the map
        /// </summary>
        public void ShowPoints(Vector2Int position, int Score)
        {
            var offset=new Vector2Int(0, position.Y+2);
            string str = $"Score: {Score} ";
            OutputSymbol("White", str, offset);
        }

        public static void OutputSymbol(string color, string symbol, Vector2Int pos)
        {
            //output the symbol at the new pos
            Console.ForegroundColor = GetColor(color);
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(symbol);
        }

        private static ConsoleColor GetColor(string color)
        {
            return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
        }
    }
}

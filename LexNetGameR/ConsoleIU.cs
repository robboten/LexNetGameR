using LexNetGameR.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// show points 2 rows below the map
        /// </summary>
        public void ShowPoints(Vector2Int position, int Score)
        {
            var offset=new Vector2Int(0, position.Y+2);
            string str = $"Score: {Score} ";
            OutputSymbol("White", str, offset);
        }

        public void OutputSymbol(string color, string symbol, Vector2Int pos)
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

        public void RenderEntity(string color, string symbol, Vector2Int position, Vector2Int acceleration) //take apart? Should move and render be in same?
        {
            //var oldpos = position - acceleration;
            OutputSymbol(color, symbol, position);

            //Debug.WriteLine($"{symbol} - p: {position.V2ToString()} - a: {oldpos.V2ToString()}");
        }

    }
}

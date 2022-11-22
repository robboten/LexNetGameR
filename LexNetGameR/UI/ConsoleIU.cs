using LexNetGameR.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static LexNetGameR.ConsoleUIHelpers;

namespace LexNetGameR
{
    public class ConsoleUI : IUI
    {
        readonly string BgColor;
        readonly string FgColor;
        public ConsoleUI(IConfiguration config)
        {
            BgColor = config.GetColor("backgroundcolor");
            FgColor = config.GetColor("foregroundcolor");
        }
        
        public void InitUI()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = GetConsoleColorFromString(BgColor);
        }

        /// <summary>
        /// show points 2 rows below the map
        /// </summary>
        public void ShowPoints(Vector2Int position, int Score)
        {
            var offset=new Vector2Int(0, position.Y+2);
            string str = $"Score: {Score} ";
            OutputSymbol(FgColor, str, offset);
        }

        public void OutputSymbol(string color, string symbol, Vector2Int pos)
        {
            //output the symbol at the new pos
            Console.ForegroundColor = GetConsoleColorFromString(color);
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(symbol);
        }

        private static ConsoleColor GetConsoleColorFromString(string color)
        {
            return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
        }
    }
}

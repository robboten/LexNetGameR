using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR
{
    internal class Entity
    {
        public string Symbol { get; set; }
        public string Name { get; set; }    
        public ConsoleColor Color { get; set; } =ConsoleColor.Green;
        public Vector2Int Position {get;set;}
        public bool IsActive {get;set;}
        public bool IsStatic {get;set;}
        public Entity(string symbol, string name, ConsoleColor color)
        {
            Symbol = symbol;
            Name = name;
            Color = color;
        }

    }
}

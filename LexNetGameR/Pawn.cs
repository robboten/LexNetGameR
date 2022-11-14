using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR
{
    internal class Pawn
    {
        private Cell cell;
        public string Symbol { get; set; }
        public string Name { get; set; }    
        public ConsoleColor Color { get; set; } =ConsoleColor.Green;

        public Cell Cell
        {
            get => cell; 
            set => cell = value;
        }

        public Pawn(Cell cell, string symbol, string name, ConsoleColor color)
        {
            this.cell = cell;
            Symbol = symbol;
            Name = name;
            Color = color;
        }

    }
}

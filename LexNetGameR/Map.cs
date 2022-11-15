using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LexNetGameR
{

    internal class Cell
    {
        public char Symbol;
        public Vector2Int Position;  
        public ConsoleColor Color;
        public Cell(Vector2Int pos)
        {
            Position = pos;
            Symbol = '.';
            Color = ConsoleColor.White;
        }
        public char GetCellSymbol()
        {
            return Symbol;
        }
        public void SetCellSymbol(Entity entity)
        {
            Symbol=entity.Symbol;
        }
    }

    internal class Map
    {
        public Vector2Int Size { get; set; } 
        public string Name { get; set; }

        //remove cells and use pos only?
        public Cell[,] cells;

        public Map(string name, Vector2Int size)
        {
            Name = name;
            Size = size;
            Vector2Int position = new();

            cells = new Cell[Size.X, Size.Y];
            for (int y=0; y<Size.Y; y++)
            {
                for (int x=0; x<Size.X; x++)
                {
                    cells[x, y] = new Cell(position);
                }
            }
        }
        public Cell GetCell(Vector2Int pos)
        {
            return cells[pos.X,pos.Y];
        }
    }

}

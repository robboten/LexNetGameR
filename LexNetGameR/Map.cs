using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR
{

    internal class Cell
    {
        public string Symbol => ". ";
        public Vector2Int Position;    
        public Cell(Vector2Int pos)
        {
            Position = pos;
        }
        public string GetCellSymbol()
        {
            return Symbol;
        }
    }

    internal class Map
    {
        public Vector2Int Size { get; set; } 
        public string Name { get; set; }

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
    internal struct Vector2Int{
        public int X {get;set;}
        public int Y {get;set;}
        public Vector2Int(int x, int y){
            X=x;
            Y=y;
        }
    }

}

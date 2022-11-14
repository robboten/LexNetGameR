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
        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }

        public Cell[,] cells;

        public Map(string name, int height,int width)
        {
            Name = name;
            Height = height;
            Width = width;
            Vector2 position = new();

            cells = new Cell[Width, Height];
            for (int y=0; y<Height; y++)
            {
                for (int x=0; x<Width; x++)
                {
                    cells[x, y] = new Cell(position);
                }
            }
        }
        public Cell GetCell(int x, int y)
        {
            return cells[x,y];
        }
    }
    internal struct Vector2Int{
        int X;
        int Y;
        public Vector2Int(int x, int y){
            X=x;
            Y=y;
        }
    }

}

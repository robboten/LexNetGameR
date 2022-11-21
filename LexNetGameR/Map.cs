using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LexNetGameR.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace LexNetGameR
{
    public class Map
    {
        private readonly char[,] maze =
        {
            { '╔','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','╗'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ','╔','═','═','═','╗',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ','╚','═','═','═','╝',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','╔','═','═','═','╗',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','╚','═','═','═','╝',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ','╔','═','═','═','╗',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ','╚','═','═','═','╝',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '║',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','║'},
            { '╚','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','╝'}

        };
        Vector2Int Size;
        readonly int MaxX;
        readonly int MaxY;
        public Map()
        {
            Size.Set(maze.GetLength(1), maze.GetLength(0));
            MaxX = maze.GetLength(1);
            MaxY = maze.GetLength(0);
        }

        public char GetChar(int x, int y)
        {
            return maze[y, x];
        }

        public Tuple<int,int> GetSizeInt()
        {
            return Tuple.Create(Size.X,Size.Y);
        }

        public Vector2Int GetSizeV2()
        {
            return  new Vector2Int (Size.X, Size.Y);
        }
        public void DrawMap()
        {
            for (int y = 0; y < MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {
                    ConsoleUI.OutputSymbol("White", GetChar(x, y).ToString(), new Vector2Int(x, y));
                }
            }
        }

        bool IsWall(int x, int y) => GetChar(x,y) is not ' ';

        public bool CanMove(Vector2Int pos, Vector2Int acc)
        {
            pos += acc;
            return TestPos(pos);
        }
        private bool TestPos(Vector2Int pos)
        {
            if (pos.X > 0 && pos.X < MaxX && pos.Y > 0 && pos.Y < MaxY)
                if (!IsWall(pos.X, pos.Y))
                    return true;
            return false;
        }

    }

}

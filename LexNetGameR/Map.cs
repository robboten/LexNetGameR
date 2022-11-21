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
        List<Vector2Int> ValidPositionList;
        public Map()
        {
            Size.Set(maze.GetLength(1), maze.GetLength(0));
            MaxX = maze.GetLength(1);
            MaxY = maze.GetLength(0);
            ValidPositionList=ValidPositions();
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
            if (ValidPositionList.Contains(pos))
                    return true;
            return false;
        }

        /// <summary>
        /// special random position generator that checks if the position is valid
        /// </summary>
        /// <returns>a valid Vector2Int position</returns>
        public Vector2Int RandomPosWithCheck()
        {
            List<Vector2Int> list = ValidPositionList;
            Vector2Int randPos;
            while (true) //ugly check.. work on this
            {
                randPos = Vector2Int.GetRandom(GetSizeInt());
                if (list.Contains(randPos))
                    return randPos;
            }
        }

        /// <summary>
        /// make a list with all valid positions once and for all
        /// </summary>
        public List<Vector2Int> ValidPositions()
        {
            List<Vector2Int> validPosList = new List<Vector2Int>();

            for (int y = 0; y < MaxY; y++)
            {
                for (int x = 0; x < MaxX; x++)
                {
                    Vector2Int pos = new(x, y);
                    if (pos.X > 0 && pos.X < MaxX && pos.Y > 0 && pos.Y < MaxY)
                        if (!IsWall(pos.X, pos.Y))
                        {
                            //Console.WriteLine((pos.V2ToString()));
                            validPosList.Add(pos);
                        }

                }
            }
            return validPosList;
        }

    }

}

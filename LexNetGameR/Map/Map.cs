﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LexNetGameR.Entities;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace LexNetGameR
{
    public class Map : IMap
    {
        //move this out
        private readonly char[,] mazeold =
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


        char[,] maze;
        readonly List<Vector2Int> ValidPositionList;
        readonly string MapColor;
        public Map(IConfiguration config)
        {
            MapColor = "White"; //don't like this - should be foregroundcolor for ui

            //yuk
            var stringmaze=config.GetSection("game:maze").Get<List<string[]>>()!;
            char[][] jaggedarr = stringmaze.Select(x => string.Concat(x).ToCharArray()).ToArray();
            maze= ImperativeConvert(jaggedarr);

            Size.Set(maze.GetLength(1), maze.GetLength(0));
            ValidPositionList = MakeValidPosList();
        }

        static char[,] ImperativeConvert(char[][] source)
        {
            char[,] result = new char[source.Length, source[0].Length];

            for (int i = 0; i < source.Length; i++)
            {
                for (int k = 0; k < source[0].Length; k++)
                {
                    result[i, k] = source[i][k];
                }
            }

            return result;
        }

        public char GetMapChar(int x, int y)
        {
            return maze[y, x];
        }

        public Tuple<int, int> GetSizeInt()
        {
            return Tuple.Create(Size.X, Size.Y);
        }

        public Vector2Int GetSizeV2()
        {
            return new Vector2Int(Size.X, Size.Y);
        }
        public void DrawMap(IUI UI)
        {
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    UI.OutputSymbol(MapColor, GetMapChar(x, y).ToString(), new Vector2Int(x, y));
                }
            }
        }

        public List<Vector2Int> GetMapPosList()
        {
            return ValidPositionList;
        }

        ////does not work ...
        //public void RenderAll(IUI UI, List<Entity> el)
        //{
        //    char s;
        //    string c;
        //    Vector2Int p;

        //    var maplist = ValidPositionList;

        //    //filter out all positions with ent from mapposlist
        //    maplist.RemoveAll(x => !el.Any(y => y.Position == x));
        //    foreach (var map in maplist)
        //    {
        //        s = GetMapChar(map.X, map.Y);
        //        p = map;
        //        c = MapColor;
        //        UI.OutputSymbol(c, s.ToString(), p);
        //    }
        //    foreach (var e in el)
        //    {
        //        s = e.Symbol;
        //        p = e.Position;
        //        c = e.Color;
        //        UI.OutputSymbol(c, s.ToString(), p);
        //    }
        //}

        //not good in here.. but won't bother for now
        public void RenderAll(IUI UI, List<Entity> el, List<Vector2Int> maplist)
        {
            char s;
            string c;
            Vector2Int p;

            List<Vector2Int> epl = el.Select(o => o.Position).ToList(); //get all ent pos into a list

            foreach (var pos in maplist)
            {
                if (epl.Contains(pos))
                {
                    var entityAtPos = el.Where(e => e.Position == pos && e.IsActive).ToList();
                    var eap = entityAtPos.First();
                    s = eap.Symbol;
                    p = eap.Position;
                    c = eap.Color;
                }
                else
                {
                    s = GetMapChar(pos.X, pos.Y);
                    p = pos;
                    c = MapColor;
                }

                UI.OutputSymbol(c, s.ToString(), p);
            }
        }

        bool IsWall(int x, int y) => GetMapChar(x, y) is not ' ';

        public bool CanMove(Vector2Int pos, Vector2Int acc)
        {
            var lpos = pos; //work with instance
            lpos += acc;
            if (ValidPositionList.Contains(lpos))
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
        public List<Vector2Int> MakeValidPosList()
        {
            List<Vector2Int> validPosList = new List<Vector2Int>();

            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    Vector2Int pos = new(x, y);
                    if (pos.X > 0 && pos.X < Size.X && pos.Y > 0 && pos.Y < Size.Y)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR
{
    internal struct Vector2Int
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Set(int x, int y)
        {
            X = x;
            Y = y;
        }

        //borrowed from Unity @ https://gist.github.com/twobob/c6b5bfa1101164c3a0bc0881189eeceb
        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2Int operator -(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.X - b.X, a.Y - b.Y);
        }
        public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }
        public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
        {
            return !(lhs == rhs);
        }
        public bool Equals(Vector2Int other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public string V2ToString()
        {
            return $"{X}, {Y}";
        }

        public void Clamp(Vector2Int min, Vector2Int max)
        {
            X = Math.Max(min.X, X);
            X = Math.Min(max.X, X);
            Y = Math.Max(min.Y, Y);
            Y = Math.Min(max.Y, Y);
        }

        public void Clamp(int min, int max)
        {
            X = Math.Max(min, X);
            X = Math.Min(max, X);
            Y = Math.Max(min, Y);
            Y = Math.Min(max, Y);
        }
        public static Vector2Int Zero { get { return s_Zero; } }
        public static Vector2Int One { get { return s_One; } }
        public static Vector2Int Up { get { return s_Up; } }
        public static Vector2Int Down { get { return s_Down; } }
        public static Vector2Int Left { get { return s_Left; } }
        public static Vector2Int Right { get { return s_Right; } }

        private static readonly Vector2Int s_Zero = new(0, 0);
        private static readonly Vector2Int s_One = new(1, 1);
        private static readonly Vector2Int s_Up = new(0, -1); //swapped -1 1 up/down because of array pos
        private static readonly Vector2Int s_Down = new(0, 1);
        private static readonly Vector2Int s_Left = new(-1, 0);
        private static readonly Vector2Int s_Right = new(1, 0);
    }
}

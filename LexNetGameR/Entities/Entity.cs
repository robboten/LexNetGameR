using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LexNetGameR.Entities
{
    public class Entity
    {
        //would love to make these into components, but not sure it's worth it
        public string Name { get; set; }
        public char Symbol { get; set; }
        public string Color { get; set; } 
        public Vector2Int Position { get; set; } = Vector2Int.One;
        public Vector2Int Acceleration { get; set; } = Vector2Int.Zero;
        public bool IsActive { get; set; } = true; //not sure this is needed since if it's not in list it won't be active... but just in case..
        public bool IsStatic { get; set; } = false;
        public bool IsPlayer { get; set; } = false;
        public int Points { get; set; }

        [JsonConstructor]
        public Entity(string Name, char Symbol, Vector2Int Position, string Color="Blue", bool IsActive=true, bool IsStatic=false, bool IsPlayer=false, int Points=0)
        {
            this.Name = Name;
            this.Symbol = Symbol;
            this.Position = Position;
            this.Color = Color;
            this.IsActive = IsActive;
            this.IsStatic = IsStatic;
            this.IsPlayer = IsPlayer;
            this.Points = Points;
        }
        //public Entity(string name, char symbol)
        //{
        //    Name = name;
        //    Position = new(1, 1);
        //    Symbol = symbol;
        //}

        public void MoveEntity() //take apart? Should move and render be in same?
        {
            Position += Acceleration;
        }

    }
}

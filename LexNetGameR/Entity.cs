using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexNetGameR
{
    internal class Entity
    {
        //make id to identify without name?
        public char Symbol { get; set; }
        public string Name { get; set; }    
        public ConsoleColor Color { get; set; } = ConsoleColor.Green;
        public Vector2Int Position {get;set;}
        public Vector2Int Acceleration { get; set; }
        public bool IsActive {get;set;}
        public bool IsStatic {get;set;}
        public bool IsPlayer {get;set;}

        public Entity(string name,char symbol)
        {
            Random random = new();
            Position = new(random.Next(0, 10), random.Next(0, 10)); //change to map size!
            Symbol = symbol;
            Name = name;
            IsActive = true;
        }
        public Vector2Int GetPos()
        {
            return Position;
        }
        public void SetPos(Vector2Int Acc)
        {
            Position += Acc;
            Vector2Int pos = Position;
            pos.Clamp(0, 9); //why doesn't this work directly on Position?
            Position=pos;
            //Console.WriteLine(Position.V2ToString());
            //Console.WriteLine(pos.V2ToString());
        }

    }
}
